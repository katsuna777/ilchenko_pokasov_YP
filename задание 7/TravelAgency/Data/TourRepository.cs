using MySqlConnector;
using TravelAgency.Models;

namespace TravelAgency.Data;

public sealed class TourRepository
{
    private const string ConnectionString =
        "Server=85.236.189.123;Port=13306;Database=isp-44_ilchenko_dm_tours7;" +
        "User ID=isp-44_ilchenko_dm;Password=isp-44_ilchenko_dm;Character Set=utf8mb4;SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=10";

    public async Task<Catalog> LoadAsync()
    {
        await using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);
        var types = new List<TourType>();
        await using (var command = new MySqlCommand("SELECT Id,Name FROM `Type` ORDER BY Name", connection, transaction))
        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
                types.Add(new TourType(reader.GetInt32("Id"), reader.GetString("Name")));
        }

        var links = new Dictionary<int, List<int>>();
        await using (var command = new MySqlCommand("SELECT TourId,TypeId FROM TypeOfTour", connection, transaction))
        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var tourId = reader.GetInt32("TourId");
                if (!links.TryGetValue(tourId, out var ids))
                    links[tourId] = ids = new List<int>();
                ids.Add(reader.GetInt32("TypeId"));
            }
        }

        var tours = new List<Tour>();
        await using (var command = new MySqlCommand("SELECT Id,Name,Description,Price,TicketCount,IsActual,ImagePreview FROM Tour ORDER BY Id", connection, transaction))
        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var id = reader.GetInt32("Id");
                tours.Add(new Tour(
                    id, reader.GetString("Name"),
                    reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                    reader.GetDecimal("Price"), reader.GetInt32("TicketCount"), reader.GetBoolean("IsActual"),
                    reader["ImagePreview"] is byte[] image ? image : null,
                    links.TryGetValue(id, out var ids) ? ids : Array.Empty<int>()));
            }
        }
        await transaction.CommitAsync();
        return new Catalog(tours, types);
    }
}
