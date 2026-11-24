using MySqlConnector;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn.Data;

public sealed class SchoolRepository(DatabaseSettings settings, AdminSession session)
{
    private async Task<MySqlConnection> OpenAsync()
    {
        var connection = new MySqlConnection(settings.ConnectionString);
        try
        {
            await connection.OpenAsync();
            return connection;
        }
        catch
        {
            await connection.DisposeAsync();
            throw;
        }
    }

    public async Task<List<Service>> GetServicesAsync()
    {
        await using var c = await OpenAsync();
        await using var cmd = new MySqlCommand("""
            SELECT ID,Title,Cost,DurationInSeconds,COALESCE(Description,''),
                   COALESCE(Discount,0),COALESCE(MainImagePath,'') FROM Service ORDER BY ID
            """, c);
        await using var reader = await cmd.ExecuteReaderAsync();
        var services = new List<Service>();
        while (await reader.ReadAsync())
            services.Add(new Service(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2),
                reader.GetInt32(3), reader.GetString(4), reader.GetDecimal(5), reader.GetString(6)));
        return services;
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        session.RequireAdmin();
        await using var c = await OpenAsync();
        await using var cmd = new MySqlCommand("""
            SELECT ID,FirstName,LastName,COALESCE(Patronymic,'')
            FROM Client ORDER BY LastName,FirstName,Patronymic
            """, c);
        await using var reader = await cmd.ExecuteReaderAsync();
        var clients = new List<Client>();
        while (await reader.ReadAsync())
            clients.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
        return clients;
    }

    public async Task<int> SaveServiceAsync(Service service)
    {
        session.RequireAdmin();
        ServiceRules.Validate(service);
        await using var c = await OpenAsync();
        var sql = service.Id == 0
            ? """
              INSERT INTO Service(Title,Cost,DurationInSeconds,Description,Discount,MainImagePath)
              VALUES(@title,@cost,@duration,@description,@discount,@image)
              """
            : """
              UPDATE Service SET Title=@title,Cost=@cost,DurationInSeconds=@duration,
                Description=@description,Discount=@discount,MainImagePath=@image WHERE ID=@id
              """;
        await using var cmd = new MySqlCommand(sql, c);
        cmd.Parameters.AddWithValue("@title", service.Title.Trim());
        cmd.Parameters.AddWithValue("@cost", service.Cost);
        cmd.Parameters.AddWithValue("@duration", service.DurationInSeconds);
        cmd.Parameters.AddWithValue("@description", service.Description);
        cmd.Parameters.AddWithValue("@discount", service.Discount);
        cmd.Parameters.AddWithValue("@image", service.MainImagePath);
        cmd.Parameters.AddWithValue("@id", service.Id);
        try
        {
            var affected = await cmd.ExecuteNonQueryAsync();
            if (service.Id != 0 && affected == 0)
                throw new InvalidOperationException("Услуга уже удалена. Обновите список.");
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            throw new ArgumentException("Услуга с таким названием уже существует. Введите другое название.");
        }
        return service.Id == 0 ? checked((int)cmd.LastInsertedId) : service.Id;
    }

    public async Task DeleteServiceAsync(int id)
    {
        session.RequireAdmin();
        await using var c = await OpenAsync();
        await using var transaction = await c.BeginTransactionAsync();
        await using var check = new MySqlCommand("SELECT COUNT(*) FROM ClientService WHERE ServiceID=@id", c, transaction);
        check.Parameters.AddWithValue("@id", id);
        if (Convert.ToInt32(await check.ExecuteScalarAsync()) != 0)
            throw new InvalidOperationException("Нельзя удалить услугу, на которую уже записаны клиенты. История записей сохраняется.");
        await using var photos = new MySqlCommand("DELETE FROM ServicePhoto WHERE ServiceID=@id", c, transaction);
        photos.Parameters.AddWithValue("@id", id);
        await photos.ExecuteNonQueryAsync();
        await using var cmd = new MySqlCommand("DELETE FROM Service WHERE ID=@id", c, transaction);
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync();
        await transaction.CommitAsync();
    }

    public async Task<int> AddAppointmentAsync(int clientId, int serviceId, DateTime start)
    {
        session.RequireAdmin();
        if (start < DateTime.Now)
            throw new ArgumentException("Нельзя записать клиента на прошедшее время.");
        await using var c = await OpenAsync();
        await using var transaction = await c.BeginTransactionAsync();

        await using var client = new MySqlCommand("SELECT ID FROM Client WHERE ID=@id FOR UPDATE", c, transaction);
        client.Parameters.AddWithValue("@id", clientId);
        if (await client.ExecuteScalarAsync() is null)
            throw new ArgumentException("Клиент не найден. Откройте форму записи заново.");
        await using var duration = new MySqlCommand("SELECT DurationInSeconds FROM Service WHERE ID=@id FOR SHARE", c, transaction);
        duration.Parameters.AddWithValue("@id", serviceId);
        var seconds = await duration.ExecuteScalarAsync();
        if (seconds is null)
            throw new ArgumentException("Услуга была удалена. Обновите список.");
        var end = start.AddSeconds(Convert.ToInt32(seconds));
        await using var overlaps = new MySqlCommand("""
            SELECT COUNT(*) FROM ClientService cs JOIN Service s ON s.ID=cs.ServiceID
            WHERE cs.ClientID=@client AND cs.StartTime < @end
              AND DATE_ADD(cs.StartTime, INTERVAL s.DurationInSeconds SECOND) > @start
            """, c, transaction);
        overlaps.Parameters.AddWithValue("@client", clientId);
        overlaps.Parameters.AddWithValue("@start", start);
        overlaps.Parameters.AddWithValue("@end", end);
        if (Convert.ToInt32(await overlaps.ExecuteScalarAsync()) > 0)
            throw new ArgumentException("У клиента уже есть занятие в это время. Выберите другое время.");
        await using var cmd = new MySqlCommand("""
            INSERT INTO ClientService(ClientID,ServiceID,StartTime) VALUES(@client,@service,@start)
            """, c, transaction);
        cmd.Parameters.AddWithValue("@client", clientId);
        cmd.Parameters.AddWithValue("@service", serviceId);
        cmd.Parameters.AddWithValue("@start", start);
        await cmd.ExecuteNonQueryAsync();
        var id = checked((int)cmd.LastInsertedId);
        await transaction.CommitAsync();
        return id;
    }

    public async Task<List<Appointment>> GetUpcomingAsync(DateTime now)
    {
        session.RequireAdmin();
        await using var c = await OpenAsync();
        await using var cmd = new MySqlCommand("""
            SELECT cs.ID,CONCAT_WS(' ',c.LastName,c.FirstName,c.Patronymic),c.Phone,s.Title,
                cs.StartTime,DATE_ADD(cs.StartTime, INTERVAL s.DurationInSeconds SECOND)
            FROM ClientService cs JOIN Client c ON c.ID=cs.ClientID JOIN Service s ON s.ID=cs.ServiceID
            WHERE cs.StartTime >= @now AND cs.StartTime < @until ORDER BY cs.StartTime,cs.ID
            """, c);
        cmd.Parameters.AddWithValue("@now", now);
        cmd.Parameters.AddWithValue("@until", now.AddDays(2));
        await using var reader = await cmd.ExecuteReaderAsync();
        var items = new List<Appointment>();
        while (await reader.ReadAsync())
            items.Add(new Appointment(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                reader.GetString(3), reader.GetDateTime(4), reader.GetDateTime(5)));
        return items;
    }
}
