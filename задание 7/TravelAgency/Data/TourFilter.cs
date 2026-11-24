using TravelAgency.Models;

namespace TravelAgency.Data;

internal static class TourFilter
{
    public static List<Tour> Apply(IEnumerable<Tour> tours, string search, int? typeId, bool actualOnly, bool descending)
    {
        var text = search.Trim();
        var result = tours.Where(tour =>
            (tour.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase)
             || tour.Description.Contains(text, StringComparison.CurrentCultureIgnoreCase))
            && (!typeId.HasValue || tour.TypeIds.Contains(typeId.Value))
            && (!actualOnly || tour.IsActual));
        return descending
            ? result.OrderByDescending(tour => tour.Price).ThenBy(tour => tour.Name).ToList()
            : result.OrderBy(tour => tour.Price).ThenBy(tour => tour.Name).ToList();
    }

    public static decimal Total(IEnumerable<Tour> tours) => tours.Sum(tour => tour.Price * tour.TicketCount);
}
