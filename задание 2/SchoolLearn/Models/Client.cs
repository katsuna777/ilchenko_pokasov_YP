namespace SchoolLearn.Models;

public sealed record Client(int Id, string FirstName, string LastName, string Patronymic)
{
    public string FullName => $"{LastName} {FirstName} {Patronymic}".Trim();
    public override string ToString() => FullName;
}
