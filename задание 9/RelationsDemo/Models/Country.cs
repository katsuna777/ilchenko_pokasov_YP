namespace RelationsDemo.Models;

public class Country
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
