using System.Globalization;
using SchoolLearn.Data;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn.Forms;

public partial class ClientServiceForm : Form
{
    private readonly SchoolRepository? repository;
    private readonly Service? service;
    private bool saving;

    public ClientServiceForm()
    {
        InitializeComponent();
    }

    public ClientServiceForm(SchoolRepository repository, Service service) : this()
    {
        this.repository = repository;
        this.service = service;
        serviceTitleLabel.Text = service.Title;
        durationLabel.Text = $"Длительность: {service.DurationMinutes:0.##} мин";
        dateDateTimePicker.MinDate = DateTime.Today;
        dateDateTimePicker.Value = DateTime.Today.AddDays(1);
        timeMaskedTextBox.Text = "09:00";
        UpdateEndTime();
    }

    private async void ClientServiceForm_Load(object? sender, EventArgs e)
    {
        if (repository is null)
            return;
        ImageAssets.ApplyBrand(this, logoPictureBox);
        try
        {
            var clients = await repository.GetClientsAsync();
            if (IsDisposed)
                return;
            clientComboBox.DataSource = clients;
            clientComboBox.SelectedIndex = -1;
            clientStatusLabel.Text = clients.Count == 0 ? "В базе пока нет клиентов." : $"Клиентов в базе: {clients.Count}. Выберите фамилию из списка.";
            saveButton.Enabled = clients.Count > 0;
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                clientStatusLabel.Text = "Не удалось загрузить клиентов. Откройте форму заново.";
                UserMessages.Error(this, ex);
            }
        }
    }

    private void Time_Changed(object? sender, EventArgs e) => UpdateEndTime();

    private void UpdateEndTime()
    {
        if (service is null)
            return;
        if (TimeOnly.TryParseExact(timeMaskedTextBox.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var clock))
            endLabel.Text = (dateDateTimePicker.Value.Date + clock.ToTimeSpan()).AddSeconds(service.DurationInSeconds).ToString("dd.MM.yyyy HH:mm");
        else
            endLabel.Text = "Введите время в формате ЧЧ:ММ";
    }

    private async void SaveButton_Click(object? sender, EventArgs e)
    {
        if (saving || repository is null || service is null)
            return;
        saving = true;
        saveButton.Enabled = false;
        cancelButton.Enabled = false;
        try
        {
            if (clientComboBox.SelectedItem is not Client client)
                throw new ArgumentException("Выберите клиента из списка.");
            var start = ServiceRules.AppointmentStart(dateDateTimePicker.Value.Date, timeMaskedTextBox.Text);
            var id = await repository.AddAppointmentAsync(client.Id, service.Id, start);
            MessageBox.Show(this, $"Клиент: {client.FullName}\nУслуга: {service.Title}\n" +
                $"Начало: {start:dd.MM.yyyy HH:mm}\nОкончание: {start.AddSeconds(service.DurationInSeconds):dd.MM.yyyy HH:mm}\nНомер записи: {id}",
                "Леарн — запись сохранена", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void ClientServiceForm_FormClosing(object? sender, FormClosingEventArgs e) => e.Cancel = saving;
}
