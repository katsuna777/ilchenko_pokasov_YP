namespace RelationsDemo.Models;

public class TravelType
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<TourType> TypeOfTours { get; set; } = new List<TourType>();
}
