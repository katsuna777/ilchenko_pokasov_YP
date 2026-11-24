namespace DoctorMag.Forms;

internal static class Theme
{

    public static readonly Color MainBackground = Color.FromArgb(183, 211, 242);
    public static readonly Color SecondaryBackground = Color.FromArgb(175, 175, 220);
    public static readonly Color AccentAction = Color.FromArgb(138, 132, 226);

    public static readonly Font AppFont = new Font("Comic Sans MS", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font HeaderFont = new Font("Comic Sans MS", 10.5F, FontStyle.Bold, GraphicsUnit.Point);

    public static string Asset(string name) => Path.Combine(AppContext.BaseDirectory, "Resources", name);

    public static PictureBox Picture(string file) => new()
    {
        ImageLocation = file, SizeMode = PictureBoxSizeMode.Zoom, TabStop = false
    };

    public static void Apply(Form form)
    {
        form.Font = AppFont;
        form.BackColor = MainBackground;
    }

    public static void StyleButton(Button button, bool isAccent = false)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = HeaderFont;
        button.Cursor = Cursors.Hand;
        if (isAccent)
        {
            button.BackColor = AccentAction;
            button.ForeColor = Color.White;
        }
        else
        {
            button.BackColor = SecondaryBackground;
            button.ForeColor = Color.FromArgb(20, 20, 30);
        }
    }

    public static void Error(IWin32Window owner, Exception error) =>
        MessageBox.Show(owner, error.Message, "ДокторМаг", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
