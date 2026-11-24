namespace RelationsDemo.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int TicketCount { get; set; }
    public bool IsActual { get; set; }
    public byte[]? ImagePreview { get; set; }
    public string CountryCode { get; set; } = "";
    public Country? Country { get; set; }
    public ICollection<TourType> TypeOfTours { get; set; } = new List<TourType>();

    public string StatusText => IsActual ? "Актуален" : "Не актуален";
    public string TypeNames => string.Join(", ", TypeOfTours.Select(link => link.Type.Name).OrderBy(name => name));
}
