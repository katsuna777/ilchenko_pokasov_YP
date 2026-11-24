using ServiceCatalog.Models;

namespace ServiceCatalog.Data;

internal static class ServiceSelection
{
    public static List<Service> Apply(IEnumerable<Service> services, string search, int sort)
    {
        var text = search.Trim();
        var selected = services.Where(service =>
            service.Title.Contains(text, StringComparison.CurrentCultureIgnoreCase)
            || service.Description.Contains(text, StringComparison.CurrentCultureIgnoreCase));
        return sort switch
        {
            1 => selected.OrderByDescending(service => service.Title, StringComparer.CurrentCultureIgnoreCase).ThenBy(service => service.Id).ToList(),
            2 => selected.OrderBy(service => service.Cost).ThenBy(service => service.Title, StringComparer.CurrentCultureIgnoreCase).ToList(),
            3 => selected.OrderByDescending(service => service.Cost).ThenBy(service => service.Title, StringComparer.CurrentCultureIgnoreCase).ToList(),
            _ => selected.OrderBy(service => service.Title, StringComparer.CurrentCultureIgnoreCase).ThenBy(service => service.Id).ToList()
        };
    }
}
