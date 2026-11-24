namespace DoctorMag.Forms;

partial class LoginForm
{
    private TextBox loginBox = null!;
    private TextBox passwordBox = null!;

    private void InitializeComponent()
    {
        SuspendLayout();
        var logo = new PictureBox { ImageLocation = Theme.Asset("logo.png"), SizeMode = PictureBoxSizeMode.Zoom, Bounds = new Rectangle(24, 20, 164, 64) };
        var title = new Label { Text = "Вход в систему", Font = Theme.HeaderFont, AutoSize = true, Location = new Point(24, 106) };
        var loginLabel = new Label { Text = "Логин сотрудника или фамилия клиента", AutoSize = true, Location = new Point(24, 160) };
        loginBox = new TextBox { Bounds = new Rectangle(24, 188, 410, 31), TabIndex = 0 };
        var passwordLabel = new Label { Text = "Пароль или номер полиса ОМС", AutoSize = true, Location = new Point(24, 233) };
        passwordBox = new TextBox { Bounds = new Rectangle(24, 261, 410, 31), UseSystemPasswordChar = true, TabIndex = 1 };
        var signIn = new Button { Text = "Войти", Bounds = new Rectangle(24, 318, 115, 40), TabIndex = 2 };
        Theme.StyleButton(signIn, isAccent: true);
        var guest = new Button { Text = "Просмотр сотрудников как гость", Bounds = new Rectangle(150, 318, 284, 40), TabIndex = 3 };
        Theme.StyleButton(guest, isAccent: false);
        signIn.Click += SignIn;
        guest.Click += EnterAsGuest;
        Controls.AddRange(new Control[] { logo, title, loginLabel, loginBox, passwordLabel, passwordBox, signIn, guest });
        AcceptButton = signIn;
        AutoScaleMode = AutoScaleMode.Font;
        Theme.Apply(this);
        ClientSize = new Size(458, 386);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = new Icon(Theme.Asset("icon.ico"));
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ДокторМаг — вход";
        ResumeLayout(false);
    }
}
