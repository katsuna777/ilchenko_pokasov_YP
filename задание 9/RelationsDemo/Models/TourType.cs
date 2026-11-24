namespace RelationsDemo.Models;

public class TourType
{
    public int TourId { get; set; }
    public int TypeId { get; set; }
    public Tour Tour { get; set; } = null!;
    public TravelType Type { get; set; } = null!;
}
