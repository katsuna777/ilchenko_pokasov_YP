namespace ServiceCatalog.Models;

public sealed record Service(int Id, string Title, string Description, decimal Cost, decimal Discount, byte[]? MainImage)
{
    public decimal DiscountedCost => decimal.Round(Cost * (100 - Discount) / 100, 2, MidpointRounding.AwayFromZero);
}
