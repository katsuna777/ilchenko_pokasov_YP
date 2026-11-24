using System.Globalization;
using ServiceCatalog.Models;

namespace ServiceCatalog.Forms;

internal sealed class ServiceRow : UserControl
{
    private readonly PictureBox thumbnail;
    private readonly ToolTip description = new();
    private static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru-RU");

    public ServiceRow(Service service)
    {
        Height = 112;
        Margin = new Padding(0, 4, 0, 4);
        Padding = new Padding(6);
        BackColor = Color.White;
        BorderStyle = BorderStyle.FixedSingle;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        thumbnail = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, Image = LoadPhoto(service.MainImage) };
        var details = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        details.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        var title = new Label { Text = service.Title, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true };
        description.SetToolTip(title, service.Description.Length > 0 ? service.Description : service.Title);
        var price = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        if (service.Discount > 0)
        {
            price.Controls.Add(new Label
            {
                Text = service.Cost.ToString("N2", Russian), AutoSize = true,
                Font = new Font(SystemFonts.MessageBoxFont!, FontStyle.Strikeout),
                ForeColor = Color.DimGray, Margin = new Padding(3, 2, 5, 0)
            });
        }
        price.Controls.Add(new Label
        {
            Text = $"{service.DiscountedCost.ToString("N2", Russian)} РУБ",
            AutoSize = true, Margin = new Padding(3, 2, 0, 0)
        });
        var discount = new Label
        {
            Text = service.Discount > 0 ? $"Скидка {service.Discount.ToString("0.##", Russian)}%" : "",
            Dock = DockStyle.Fill, ForeColor = Color.DimGray
        };
        details.Controls.Add(title, 0, 0);
        details.Controls.Add(price, 0, 1);
        details.Controls.Add(discount, 0, 2);
        layout.Controls.Add(thumbnail, 0, 0);
        layout.Controls.Add(details, 1, 0);
        Controls.Add(layout);
    }

    private static Image LoadPhoto(byte[]? photo)
    {
        if (photo is { Length: > 0 })
        {
            try
            {
                using var stream = new MemoryStream(photo);
                using var image = Image.FromStream(stream);
                return new Bitmap(image);
            }
            catch (ArgumentException) { }
            catch (OutOfMemoryException) { }
        }
        using var placeholder = Image.FromFile(Path.Combine(AppContext.BaseDirectory, "Resources", "Picture.jpg"));
        return new Bitmap(placeholder);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            thumbnail.Image?.Dispose();
            description.Dispose();
        }
        base.Dispose(disposing);
    }
}
