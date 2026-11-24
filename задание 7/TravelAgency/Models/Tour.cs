namespace TravelAgency.Models;

public sealed record Tour(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int TicketCount,
    bool IsActual,
    byte[]? Image,
    IReadOnlyList<int> TypeIds);

public sealed record TourType(int Id, string Name)
{
    public override string ToString() => Name;
}

public sealed record Catalog(IReadOnlyList<Tour> Tours, IReadOnlyList<TourType> Types);
