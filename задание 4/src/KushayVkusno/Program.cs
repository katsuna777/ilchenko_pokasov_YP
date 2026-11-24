using KushayVkusno.Data;
using KushayVkusno.Forms;

namespace KushayVkusno;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var repo = new Repository(DatabaseConfig.ConnectionString);

        while (true)
        {
            using var loginForm = new LoginForm(repo);
            if (loginForm.ShowDialog() != DialogResult.OK || loginForm.AuthenticatedUser == null)
            {
                break;
            }

            using var mainForm = new MainForm(repo, loginForm.AuthenticatedUser);
            var result = mainForm.ShowDialog();
            if (result != DialogResult.Retry)
            {
                break;
            }
        }
    }
}
