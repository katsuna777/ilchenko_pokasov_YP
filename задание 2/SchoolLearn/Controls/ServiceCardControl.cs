using System.ComponentModel;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn.Controls;

public partial class ServiceCardControl : UserControl
{
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Service? Service { get; private set; }
    public event EventHandler? EditRequested;
    public event EventHandler? DeleteRequested;
    public event EventHandler? BookRequested;

    public ServiceCardControl()
    {
        InitializeComponent();
    }

    public void Bind(Service service, bool isAdmin)
    {
        Service = service;
        titleLabel.Text = service.Title;
        priceLabel.Text = $"{service.FinalCost:N2} ₽ за {service.DurationMinutes:0.##} мин";
        oldPriceLabel.Text = $"{service.Cost:N2} ₽";
        oldPriceLabel.Visible = service.Discount > 0;
        discountLabel.Text = $"Скидка {service.Discount:0.##}%";
        descriptionLabel.Text = service.Description;
        servicePictureBox.Image = ImageAssets.GetImage(service.MainImagePath);
        BackColor = service.Discount > 0 ? Color.FromArgb(231, 250, 191) : Color.White;
        editButton.Visible = isAdmin;
        deleteButton.Visible = isAdmin;
        bookButton.Visible = isAdmin;
        var bottomPadding = (int)Math.Ceiling(14 * DeviceDpi / 96d);
        Height = (isAdmin ? bookButton.Bottom : descriptionLabel.Bottom) + bottomPadding;
    }

    private void EditButton_Click(object? sender, EventArgs e) => EditRequested?.Invoke(this, EventArgs.Empty);
    private void DeleteButton_Click(object? sender, EventArgs e) => DeleteRequested?.Invoke(this, EventArgs.Empty);
    private void BookButton_Click(object? sender, EventArgs e) => BookRequested?.Invoke(this, EventArgs.Empty);
}
