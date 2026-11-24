using System.Globalization;
using System.Text.RegularExpressions;

namespace DoctorMag.Data;

public static class BookingRules
{
    public static (TimeSpan Start, TimeSpan End) GetHours(string status, string schedule)
    {
        if (status != "Присутствует") throw new InvalidOperationException("Сотрудник временно отсутствует.");
        var match = Regex.Match(schedule, @"(\d{1,2}:\d{2})\s*[-–]\s*(\d{1,2}:\d{2})");
        if (!match.Success || !TimeSpan.TryParse(match.Groups[1].Value, CultureInfo.InvariantCulture, out var start) || !TimeSpan.TryParse(match.Groups[2].Value, CultureInfo.InvariantCulture, out var end))
            throw new InvalidOperationException("Для сотрудника не указан полный режим работы.");
        return (start, end);
    }

    public static void Validate(string status, string schedule, DateTime date)
    {
        var (start, end) = GetHours(status, schedule);
        if (date.TimeOfDay < start || date.TimeOfDay >= end) throw new InvalidOperationException("Время посещения находится вне рабочего графика врача.");
        if (date <= DateTime.Now) throw new InvalidOperationException("Выберите дату и время в будущем.");
    }
}
