using System.ComponentModel;
using SchoolLearn.Data;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn.Forms;

public partial class ServiceEditForm : Form
{
    private readonly SchoolRepository? repository;
    private readonly int serviceId;
    private string? uploadedImage;
    private bool saving;

    public ServiceEditForm()
    {
        InitializeComponent();
    }

    public ServiceEditForm(SchoolRepository repository, Service? service) : this()
    {
        this.repository = repository;
        serviceId = service?.Id ?? 0;
        Text = service is null ? "Леарн — добавление услуги" : "Леарн — редактирование услуги";
        headingLabel.Text = service is null ? "Добавление услуги" : "Редактирование услуги";
        idPanel.Visible = service is not null;
        idTextBox.Text = serviceId.ToString();
        titleTextBox.Text = service?.Title ?? "";
        costNumericUpDown.Value = service?.Cost ?? 0;
        durationNumericUpDown.Value = service?.DurationMinutes ?? 30;
        discountNumericUpDown.Value = service?.Discount ?? 0;
        descriptionTextBox.Text = service?.Description ?? "";
        var files = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Assets"))
            .Where(p => new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(p).ToLowerInvariant()))
            .Select(Path.GetFileName).OrderBy(n => n).ToList();
        imageComboBox.Items.Add("Без изображения");
        foreach (var name in files)
            imageComboBox.Items.Add(name!);
        var image = Path.GetFileName((service?.MainImagePath ?? "").Replace('\\', '/'));
        imageComboBox.SelectedItem = files.Contains(image) ? image : "Без изображения";
    }

    private void ServiceEditForm_Load(object? sender, EventArgs e)
    {
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        ImageAssets.ApplyBrand(this, logoPictureBox);
    }

    private void ImageComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ReleaseUploadPreview();
        uploadedImage = null;
        var name = imageComboBox.SelectedItem as string;
        previewPictureBox.Image = name is null or "Без изображения" ? null : ImageAssets.GetImage(name);
    }

    private void ChooseImageButton_Click(object? sender, EventArgs e)
    {
        using var picker = new OpenFileDialog
        {
            Title = "Главное изображение услуги", Filter = "Изображения PNG и JPEG|*.png;*.jpg;*.jpeg", CheckFileExists = true
        };
        if (picker.ShowDialog(this) != DialogResult.OK)
            return;
        try
        {
            if (new FileInfo(picker.FileName).Length > 10 * 1024 * 1024)
                throw new ArgumentException("Изображение должно быть не больше 10 МБ.");
            using var stream = File.OpenRead(picker.FileName);
            using var image = Image.FromStream(stream);
            var preview = new Bitmap(image);
            imageComboBox.SelectedIndex = 0;
            uploadedImage = picker.FileName;
            ReleaseUploadPreview();
            previewPictureBox.Image = preview;
            previewPictureBox.Tag = preview;
        }
        catch (Exception ex) { UserMessages.Error(this, ex); }
    }

    private async void SaveButton_Click(object? sender, EventArgs e)
    {
        if (saving || repository is null)
            return;
        saving = true;
        saveButton.Enabled = false;
        cancelButton.Enabled = false;
        try
        {
            var minutes = durationNumericUpDown.Value;
            if (minutes * 60 != decimal.Truncate(minutes * 60))
                throw new ArgumentException("Укажите длительность с точностью до целой секунды.");
            var imageName = imageComboBox.SelectedItem as string;
            var path = imageName is null or "Без изображения" ? "" : "Assets/" + imageName;
            var service = new Service(serviceId, titleTextBox.Text.Trim(), costNumericUpDown.Value,
                checked((int)(minutes * 60)), descriptionTextBox.Text.Trim(), discountNumericUpDown.Value, path);
            ServiceRules.Validate(service);
            if (uploadedImage is not null)
            {
                var name = Guid.NewGuid().ToString("N") + Path.GetExtension(uploadedImage).ToLowerInvariant();
                File.Copy(uploadedImage, ImageAssets.GetPath(name));
                service = service with { MainImagePath = "Assets/" + name };
            }
            await repository.SaveServiceAsync(service);
            saving = false;
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex) { UserMessages.Error(this, ex); }
        finally
        {
            saving = false;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;
        }
    }

    private void ServiceEditForm_FormClosing(object? sender, FormClosingEventArgs e) => e.Cancel = saving;

    private void ReleaseUploadPreview()
    {
        if (previewPictureBox.Tag is Image image)
        {
            if (ReferenceEquals(previewPictureBox.Image, image))
                previewPictureBox.Image = null;
            previewPictureBox.Tag = null;
            image.Dispose();
        }
    }

    private void ServiceEditForm_FormClosed(object? sender, FormClosedEventArgs e) => ReleaseUploadPreview();
}
