using KushayVkusno.Data;

namespace KushayVkusno.Forms;

public class LoginForm : Form
{
    private readonly Repository repo;
    private readonly TextBox loginBox;
    private readonly TextBox passwordBox;
    private readonly Button enterButton;

    public User? AuthenticatedUser { get; private set; }

    public LoginForm(Repository repo)
    {
        this.repo = repo;
        Theme.Apply(this);

        Text = "КушайВкусно — Вход в систему";
        ClientSize = new Size(450, 380);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        var logoBox = new PictureBox
        {
            Location = new Point(185, 20),
            Size = new Size(80, 80),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Theme.LoadImage("logo.png")
        };

        var titleLabel = new Label
        {
            Location = new Point(25, 110),
            Size = new Size(400, 32),
            Font = Theme.TitleFont,
            ForeColor = Theme.AccentAction,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = "ООО «КушайВкусно»"
        };

        var loginLabel = new Label
        {
            Location = new Point(50, 160),
            Size = new Size(350, 20),
            Font = Theme.BaseFont,
            Text = "Логин:"
        };

        loginBox = new TextBox
        {
            Location = new Point(50, 185),
            Size = new Size(350, 28),
            Font = Theme.BaseFont
        };

        var passLabel = new Label
        {
            Location = new Point(50, 225),
            Size = new Size(350, 20),
            Font = Theme.BaseFont,
            Text = "Пароль:"
        };

        passwordBox = new TextBox
        {
            Location = new Point(50, 250),
            Size = new Size(350, 28),
            Font = Theme.BaseFont,
            UseSystemPasswordChar = true
        };

        enterButton = new Button
        {
            Location = new Point(50, 305),
            Size = new Size(350, 42),
            Font = Theme.HeaderFont,
            BackColor = Theme.AccentAction,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Text = "Войти",
            Cursor = Cursors.Hand
        };
        enterButton.FlatAppearance.BorderSize = 0;
        enterButton.Click += async (s, e) => await LoginAsync();

        AcceptButton = enterButton;

        Controls.Add(logoBox);
        Controls.Add(titleLabel);
        Controls.Add(loginLabel);
        Controls.Add(loginBox);
        Controls.Add(passLabel);
        Controls.Add(passwordBox);
        Controls.Add(enterButton);
    }

    private async Task LoginAsync()
    {
        string login = loginBox.Text.Trim();
        string pass = passwordBox.Text.Trim();

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
        {
            MessageBox.Show("Пожалуйста, заполните логин и пароль.", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        enterButton.Enabled = false;
        try
        {
            var user = await repo.AuthenticateAsync(login, pass);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль. Попробуйте снова.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordBox.Clear();
                passwordBox.Focus();
                return;
            }

            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            enterButton.Enabled = true;
        }
    }
}
