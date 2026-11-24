using SchoolLearn.Data;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn.Forms;

public partial class UpcomingServicesForm : Form
{
    private readonly SchoolRepository? repository;
    private bool loading;
    private bool closed;

    public UpcomingServicesForm()
    {
        InitializeComponent();
    }

    public UpcomingServicesForm(SchoolRepository repository) : this()
    {
        this.repository = repository;
    }

    private async void UpcomingServicesForm_Load(object? sender, EventArgs e)
    {
        if (repository is null)
            return;
        ImageAssets.ApplyBrand(this, logoPictureBox);
        await RefreshAsync(true);
        if (!closed)
            refreshTimer.Start();
    }

    private async void RefreshTimer_Tick(object? sender, EventArgs e) => await RefreshAsync(false);
    private async void RefreshButton_Click(object? sender, EventArgs e) => await RefreshAsync(true);

    private async Task RefreshAsync(bool showError)
    {
        if (loading || closed || repository is null)
            return;
        loading = true;
        try
        {
            var now = DateTime.Now;
            var appointments = await repository.GetUpcomingAsync(now);
            if (closed)
                return;
            appointmentsDataGridView.DataSource = appointments;
            foreach (DataGridViewRow row in appointmentsDataGridView.Rows)
                if (row.DataBoundItem is Appointment item && item.Start - now < TimeSpan.FromHours(1))
                    row.DefaultCellStyle.ForeColor = Color.Firebrick;
            emptyLabel.Visible = appointments.Count == 0;
            if (emptyLabel.Visible)
                emptyLabel.BringToFront();
            statusLabel.Text = $"Записей: {appointments.Count}. Обновлено в {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            if (!closed)
            {
                statusLabel.Text = "Нет связи с базой. Следующая попытка через 30 секунд.";
                if (showError)
                    UserMessages.Error(this, ex);
            }
        }
        finally { loading = false; }
    }

    private void UpcomingServicesForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        closed = true;
        refreshTimer.Stop();
    }
}
