namespace SchoolLearn.Models;

public sealed record Appointment(int Id, string ClientName, string Phone,
    string ServiceTitle, DateTime Start, DateTime End)
{
    public string Remaining
    {
        get
        {
            var minutes = Math.Max(0, (int)Math.Ceiling((Start - DateTime.Now).TotalMinutes));
            return minutes == 0 ? "Начинается сейчас" : $"До начала: {minutes / 60} ч {minutes % 60:00} мин";
        }
    }
}
