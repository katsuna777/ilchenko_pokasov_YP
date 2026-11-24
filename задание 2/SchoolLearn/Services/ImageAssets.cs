namespace SchoolLearn.Services;

internal static class ImageAssets
{
    private static readonly Dictionary<string, Image> Images = new(StringComparer.OrdinalIgnoreCase);
    private static Icon? icon;

    public static string GetPath(string file) => Path.Combine(AppContext.BaseDirectory,
        "Assets", Path.GetFileName(file.Replace('\\', '/')));

    public static Image? GetImage(string file)
    {
        var path = GetPath(file);
        if (!File.Exists(path))
            path = GetPath("logo.png");
        if (Images.TryGetValue(path, out var cached))
            return cached;
        if (!File.Exists(path))
            return null;
        try
        {
            using var stream = File.OpenRead(path);
            using var original = Image.FromStream(stream);
            var image = new Bitmap(original);
            Images.Add(path, image);
            return image;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    public static void ApplyBrand(Form form, PictureBox logo)
    {
        logo.Image = GetImage("logo.png");
        var path = GetPath("logo.ico");
        if (icon is null && File.Exists(path))
            icon = new Icon(path);
        if (icon is not null)
            form.Icon = icon;
    }

    public static void DisposeAll()
    {
        foreach (var image in Images.Values)
            image.Dispose();
        Images.Clear();
        icon?.Dispose();
        icon = null;
    }
}
