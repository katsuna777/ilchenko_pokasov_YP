using System.Globalization;
using TravelAgency.Models;

namespace TravelAgency.Forms;

internal sealed class TourCard : UserControl
{
    private readonly PictureBox picture;
    private readonly ToolTip description = new();
    internal static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru-RU");

    public TourCard(Tour tour)
    {
        Font = new Font("Comic Sans MS", 10);
        Height = 360;
        Margin = new Padding(7);
        Padding = new Padding(8);
        BorderStyle = BorderStyle.FixedSingle;
        BackColor = Color.White;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        var name = new Label { Text = tour.Name, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, AutoEllipsis = true, Font = new Font(Font, FontStyle.Bold) };
        description.SetToolTip(name, tour.Description.Length == 0 ? tour.Name : tour.Description);
        picture = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, Image = LoadImage(tour.Image) };
        var price = new Label { Text = $"{tour.Price.ToString("N2", Russian)} РУБ", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 14, FontStyle.Bold) };
        var footer = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        footer.Controls.Add(new Label { Text = tour.IsActual ? "Актуален" : "Не актуален", ForeColor = tour.IsActual ? Color.DarkGreen : Color.FromArgb(227, 30, 36), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
        footer.Controls.Add(new Label { Text = $"Билетов: {tour.TicketCount}", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 0);
        layout.Controls.Add(name, 0, 0);
        layout.Controls.Add(picture, 0, 1);
        layout.Controls.Add(price, 0, 2);
        layout.Controls.Add(footer, 0, 3);
        Controls.Add(layout);
    }

    private static Image LoadImage(byte[]? bytes)
    {
        if (bytes is { Length: > 0 })
        {
            try
            {
                using var stream = new MemoryStream(bytes);
                using var source = Image.FromStream(stream);
                return new Bitmap(source);
            }
            catch (ArgumentException) { }
            catch (OutOfMemoryException) { }
        }
        using var placeholder = Image.FromFile(Path.Combine(AppContext.BaseDirectory, "Resources", "picture.png"));
        return new Bitmap(placeholder);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            picture.Image?.Dispose();
            description.Dispose();
        }
        base.Dispose(disposing);
    }
}
