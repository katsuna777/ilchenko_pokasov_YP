namespace SchoolLearn.Models;

public sealed record Service(int Id, string Title, decimal Cost, int DurationInSeconds,
    string Description, decimal Discount, string MainImagePath)
{
    public decimal FinalCost => decimal.Round(Cost * (1 - Discount / 100), 2);
    public decimal DurationMinutes => DurationInSeconds / 60m;
}
