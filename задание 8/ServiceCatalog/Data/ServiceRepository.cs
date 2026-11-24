using MySqlConnector;
using ServiceCatalog.Models;

namespace ServiceCatalog.Data;

public sealed class ServiceRepository
{
    private const string ConnectionString =
        "Server=85.236.189.123;Port=13306;Database=isp-44_ilchenko_dm_services8;" +
        "User ID=isp-44_ilchenko_dm;Password=isp-44_ilchenko_dm;Character Set=utf8mb4;SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=10";

    public async Task<List<Service>> LoadAsync()
    {
        await using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand("SELECT ID,Title,Description,Cost,Discount,MainImage FROM Service ORDER BY ID", connection);
        await using var reader = await command.ExecuteReaderAsync();
        var services = new List<Service>();
        while (await reader.ReadAsync())
        {
            services.Add(new Service(
                reader.GetInt32("ID"), reader.GetString("Title"),
                reader["Description"] is DBNull ? "" : reader.GetString("Description"),
                reader.GetDecimal("Cost"), Convert.ToDecimal(reader["Discount"]),
                reader["MainImage"] is byte[] photo ? photo : null));
        }
        return services;
    }
}
