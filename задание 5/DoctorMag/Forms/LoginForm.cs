using DoctorMag.Data;
using DoctorMag.Models;

namespace DoctorMag.Forms;

public partial class LoginForm : Form
{
    public LoginForm() => InitializeComponent();

    private void SignIn(object? sender, EventArgs e)
    {
        try
        {
            var session = Database.Login(loginBox.Text.Trim(), passwordBox.Text);
            if (session is null)
            {
                MessageBox.Show(this, "Проверьте логин и пароль. Клиент вводит фамилию и номер полиса ОМС.", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenSession(session);
        }
        catch (Exception error) { Theme.Error(this, error); }
    }

    private void EnterAsGuest(object? sender, EventArgs e) =>
        OpenSession(new Session(0, "Гость", "Гость", false, true));

    private void OpenSession(Session session)
    {
        try
        {
            using var directory = new DirectoryForm(session);
            Hide();
            directory.ShowDialog();
        }
        catch (Exception error) { Theme.Error(this, error); }
        finally
        {
            passwordBox.Clear();
            Show();
            loginBox.Focus();
        }
    }
}
