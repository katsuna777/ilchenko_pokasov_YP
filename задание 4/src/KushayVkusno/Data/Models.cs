namespace KushayVkusno.Data;

public record User(int Id, string FullName, int RoleId, string RoleName, string Login);

public record Category(int Id, string Name);

public record Dish(int Id, string Name, decimal Price, int CategoryId, string CategoryName, string Description, string? ImagePath);

public record OrderItem(int DishId, string DishName, int Quantity, decimal Price)
{
    public decimal Total => Quantity * Price;
}

public record Order(int Id, int UserId, string WaiterName, DateTime OrderDate, int TableNumber, int StatusId, string StatusName, string? Details, List<OrderItem> Items)
{
    public int ItemsCount => Items.Sum(i => i.Quantity);
    public decimal TotalPrice => Items.Sum(i => i.Total);
}
