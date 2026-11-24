namespace KushayVkusno;

public static class Theme
{

    public static readonly Font BaseFont = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
    public static readonly Font HeaderFont = new Font("Comic Sans MS", 12F, FontStyle.Bold, GraphicsUnit.Point);
    public static readonly Font TitleFont = new Font("Comic Sans MS", 16F, FontStyle.Bold, GraphicsUnit.Point);

    public static readonly Color MainBackground = Color.FromArgb(255, 255, 255);
    public static readonly Color SecondaryBackground = Color.FromArgb(169, 246, 244);
    public static readonly Color AccentAction = Color.FromArgb(43, 174, 229);
    public static readonly Color TextDark = Color.FromArgb(30, 30, 30);

    public static void Apply(Form form)
    {
        form.Font = BaseFont;
        form.BackColor = MainBackground;
        form.ForeColor = TextDark;
        form.Icon = LoadIcon("app.ico");
    }

    public static Icon? LoadIcon(string name)
    {
        var path = Path.Combine(AppContext.BaseDirectory, name);
        if (File.Exists(path))
        {
            try { return new Icon(path); } catch { }
        }
        return null;
    }

    public static Image? LoadImage(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            name = "picture.png";

        var fullPath = Path.Combine(AppContext.BaseDirectory, name);
        if (!File.Exists(fullPath))
        {
            fullPath = Path.Combine(AppContext.BaseDirectory, "Images", name);
        }

        if (!File.Exists(fullPath))
        {
            fullPath = Path.Combine(AppContext.BaseDirectory, "picture.png");
        }

        if (File.Exists(fullPath))
        {
            try
            {
                using var stream = File.OpenRead(fullPath);
                using var bmp = Image.FromStream(stream);
                return new Bitmap(bmp);
            }
            catch { }
        }
        return null;
    }
}
