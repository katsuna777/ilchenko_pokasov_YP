using System.Globalization;
using SchoolLearn.Models;

namespace SchoolLearn.Services;

public static class ServiceRules
{
    public static readonly (decimal Min, decimal Max)[] DiscountRanges =
        [(0, 5), (5, 15), (15, 30), (30, 70), (70, 100)];

    public static List<Service> Select(IEnumerable<Service> source, string query, int range, int sort)
    {
        query = query.Trim();
        var result = source.Where(s =>
            (query.Length == 0 || s.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                || s.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            && (range == 0 || (s.Discount >= DiscountRanges[range - 1].Min
                && s.Discount < DiscountRanges[range - 1].Max))).ToList();

        if (sort != 0)
            result.Sort((a, b) =>
            {
                var priceOrder = a.Cost.CompareTo(b.Cost);
                if (priceOrder == 0)
                    return a.Id.CompareTo(b.Id);
                return sort == 1 ? priceOrder : -priceOrder;
            });
        return result;
    }

    public static void Validate(Service service)
    {
        if (string.IsNullOrWhiteSpace(service.Title) || service.Title.Trim().Length > 100)
            throw new ArgumentException("Введите название услуги длиной от 1 до 100 символов.");
        if (service.Cost < 0 || service.Cost > 999999999m || decimal.Round(service.Cost, 2) != service.Cost)
            throw new ArgumentException("Стоимость: от 0 до 999 999 999 рублей, не более двух знаков после запятой.");
        if (service.DurationInSeconds < 0 || service.DurationInSeconds > 14400)
            throw new ArgumentException("Длительность должна быть от 0 до 240 минут (4 часов).");
        if (service.Discount < 0 || service.Discount > 100 || decimal.Round(service.Discount, 2) != service.Discount)
            throw new ArgumentException("Скидка должна быть от 0 до 100%, не более двух знаков после запятой.");
    }

    public static decimal ReadNumber(string? value, string field)
    {
        if (!decimal.TryParse(value?.Trim().Replace(',', '.'), NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var number))
            throw new ArgumentException($"Поле «{field}» должно содержать число.");
        return number;
    }

    public static DateTime AppointmentStart(DateTime? date, string? time)
    {
        if (date is null)
            throw new ArgumentException("Выберите дату занятия в календаре.");
        if (!TimeOnly.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var clock))
            throw new ArgumentException("Введите действительное время в формате ЧЧ:ММ, например 09:30.");
        var start = date.Value.Date + clock.ToTimeSpan();
        if (start < DateTime.Now)
            throw new ArgumentException("Начало занятия должно быть в будущем.");
        return start;
    }
}
