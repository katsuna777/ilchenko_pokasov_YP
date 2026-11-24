using MySqlConnector;

namespace KushayVkusno.Data;

public class Repository
{
    private readonly string connectionString;

    public Repository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    private MySqlConnection Open()
    {
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    public async Task<User?> AuthenticateAsync(string login, string password)
    {
        using var c = Open();
        using var cmd = new MySqlCommand("""
            SELECT u.id, u.full_name, u.role_id, r.name, u.login
            FROM users u
            JOIN roles r ON r.id = u.role_id
            WHERE u.login = @login AND u.password = @password
            """, c);
        cmd.Parameters.AddWithValue("@login", login.Trim());
        cmd.Parameters.AddWithValue("@password", password.Trim());
        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            return new User(r.GetInt32(0), r.GetString(1), r.GetInt32(2), r.GetString(3), r.GetString(4));
        }
        return null;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        using var c = Open();
        using var cmd = new MySqlCommand("SELECT id, name FROM categories ORDER BY name", c);
        using var r = await cmd.ExecuteReaderAsync();
        var list = new List<Category>();
        while (await r.ReadAsync())
        {
            list.Add(new Category(r.GetInt32(0), r.GetString(1)));
        }
        return list;
    }

    public async Task<List<Dish>> GetDishesAsync()
    {
        using var c = Open();
        using var cmd = new MySqlCommand("""
            SELECT d.id, d.name, d.price, d.category_id, c.name, d.description, d.image_path
            FROM dishes d
            JOIN categories c ON c.id = d.category_id
            ORDER BY d.id
            """, c);
        using var r = await cmd.ExecuteReaderAsync();
        var list = new List<Dish>();
        while (await r.ReadAsync())
        {
            list.Add(new Dish(
                r.GetInt32(0),
                r.GetString(1),
                r.GetDecimal(2),
                r.GetInt32(3),
                r.GetString(4),
                r.GetString(5),
                r.IsDBNull(6) ? null : r.GetString(6)
            ));
        }
        return list;
    }

    public async Task SaveDishAsync(Dish dish)
    {
        using var c = Open();
        if (dish.Id == 0)
        {
            using var cmd = new MySqlCommand("""
                INSERT INTO dishes (name, price, category_id, description, image_path)
                VALUES (@name, @price, @cat, @desc, @img)
                """, c);
            cmd.Parameters.AddWithValue("@name", dish.Name.Trim());
            cmd.Parameters.AddWithValue("@price", dish.Price);
            cmd.Parameters.AddWithValue("@cat", dish.CategoryId);
            cmd.Parameters.AddWithValue("@desc", dish.Description.Trim());
            cmd.Parameters.AddWithValue("@img", string.IsNullOrWhiteSpace(dish.ImagePath) ? (object)DBNull.Value : dish.ImagePath);
            await cmd.ExecuteNonQueryAsync();
        }
        else
        {
            using var cmd = new MySqlCommand("""
                UPDATE dishes
                SET name = @name, price = @price, category_id = @cat, description = @desc, image_path = @img
                WHERE id = @id
                """, c);
            cmd.Parameters.AddWithValue("@id", dish.Id);
            cmd.Parameters.AddWithValue("@name", dish.Name.Trim());
            cmd.Parameters.AddWithValue("@price", dish.Price);
            cmd.Parameters.AddWithValue("@cat", dish.CategoryId);
            cmd.Parameters.AddWithValue("@desc", dish.Description.Trim());
            cmd.Parameters.AddWithValue("@img", string.IsNullOrWhiteSpace(dish.ImagePath) ? (object)DBNull.Value : dish.ImagePath);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteDishAsync(int dishId)
    {
        using var c = Open();

        using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM order_items WHERE dish_id = @id", c))
        {
            checkCmd.Parameters.AddWithValue("@id", dishId);
            long count = Convert.ToInt64(await checkCmd.ExecuteScalarAsync());
            if (count > 0)
            {
                throw new InvalidOperationException("Товар, который присутствует в заказе, удалить нельзя.");
            }
        }

        using (var delCmd = new MySqlCommand("DELETE FROM dishes WHERE id = @id", c))
        {
            delCmd.Parameters.AddWithValue("@id", dishId);
            await delCmd.ExecuteNonQueryAsync();
        }
    }

    public async Task<int> GetNextOrderIdAsync()
    {
        using var c = Open();
        using var cmd = new MySqlCommand("SELECT COALESCE(MAX(id), 0) + 1 FROM orders", c);
        var result = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task<int> CreateOrderAsync(int userId, DateTime orderDate, int tableNumber, string? details, List<(int DishId, int Quantity, decimal Price)> items)
    {
        if (items.Count == 0)
            throw new ArgumentException("В заказе должно быть хотя бы одно блюдо.");

        using var c = Open();
        using var tx = await c.BeginTransactionAsync();

        using var cmd = new MySqlCommand("""
            INSERT INTO orders (user_id, order_date, table_number, status_id, details)
            VALUES (@user, @date, @table, 1, @details)
            """, c, tx);
        cmd.Parameters.AddWithValue("@user", userId);
        cmd.Parameters.AddWithValue("@date", orderDate);
        cmd.Parameters.AddWithValue("@table", tableNumber);
        cmd.Parameters.AddWithValue("@details", string.IsNullOrWhiteSpace(details) ? (object)DBNull.Value : details.Trim());
        await cmd.ExecuteNonQueryAsync();

        int orderId = checked((int)cmd.LastInsertedId);

        foreach (var item in items)
        {
            using var itemCmd = new MySqlCommand("""
                INSERT INTO order_items (order_id, dish_id, quantity, price)
                VALUES (@order, @dish, @qty, @price)
                """, c, tx);
            itemCmd.Parameters.AddWithValue("@order", orderId);
            itemCmd.Parameters.AddWithValue("@dish", item.DishId);
            itemCmd.Parameters.AddWithValue("@qty", item.Quantity);
            itemCmd.Parameters.AddWithValue("@price", item.Price);
            await itemCmd.ExecuteNonQueryAsync();
        }

        await tx.CommitAsync();
        return orderId;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        using var c = Open();
        using var cmd = new MySqlCommand("""
            SELECT o.id, o.user_id, u.full_name, o.order_date, o.table_number, o.status_id, s.name, o.details,
                   oi.dish_id, d.name, oi.quantity, oi.price
            FROM orders o
            JOIN users u ON u.id = o.user_id
            JOIN order_statuses s ON s.id = o.status_id
            JOIN order_items oi ON oi.order_id = o.id
            JOIN dishes d ON d.id = oi.dish_id
            ORDER BY o.order_date DESC, o.id DESC
            """, c);

        using var r = await cmd.ExecuteReaderAsync();
        var dict = new Dictionary<int, Order>();
        while (await r.ReadAsync())
        {
            int oid = r.GetInt32(0);
            if (!dict.TryGetValue(oid, out var order))
            {
                order = new Order(
                    oid,
                    r.GetInt32(1),
                    r.GetString(2),
                    r.GetDateTime(3),
                    r.GetInt32(4),
                    r.GetInt32(5),
                    r.GetString(6),
                    r.IsDBNull(7) ? null : r.GetString(7),
                    new List<OrderItem>()
                );
                dict[oid] = order;
            }
            order.Items.Add(new OrderItem(
                r.GetInt32(8),
                r.GetString(9),
                r.GetInt32(10),
                r.GetDecimal(11)
            ));
        }
        return dict.Values.ToList();
    }
}
