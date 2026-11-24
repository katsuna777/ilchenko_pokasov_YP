namespace SchoolLearn.Services;

public sealed class AdminSession
{
    public bool IsAdmin { get; private set; }

    public bool Login(string code)
    {
        IsAdmin = code == "0000";
        return IsAdmin;
    }

    public void Logout() => IsAdmin = false;

    public void RequireAdmin()
    {
        if (!IsAdmin)
            throw new InvalidOperationException("Это действие доступно только администратору.");
    }
}
