using System.ComponentModel;
using SchoolLearn.Controls;
using SchoolLearn.Data;
using SchoolLearn.Forms;
using SchoolLearn.Models;
using SchoolLearn.Services;

namespace SchoolLearn;

public partial class Form1 : Form
{
    private readonly AdminSession session = new();
    private SchoolRepository? repository;
    private List<Service> services = [];
    private bool loading;
    private ServiceEditForm? editForm;

    public Form1()
    {
        InitializeComponent();
    }

    private async void Form1_Load(object? sender, EventArgs e)
    {
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        ImageAssets.ApplyBrand(this, logoPictureBox);
        discountComboBox.SelectedIndex = 0;
        sortComboBox.SelectedIndex = 0;
        await RunAsync(LoadServicesAsync);
    }

    private async Task LoadServicesAsync()
    {
        if (loading)
            return;
        loading = true;
        refreshButton.Enabled = false;
        countLabel.Text = "Загрузка услуг…";
        try
        {
            repository ??= new SchoolRepository(DatabaseSettings.Load(), session);
            var loaded = await repository.GetServicesAsync();
            if (IsDisposed || Disposing)
                return;
            services = loaded;
            RenderServices();
        }
        catch
        {
            if (!IsDisposed)
                countLabel.Text = "Нет связи с базой. Проверьте сеть и нажмите «Обновить».";
            throw;
        }
        finally
        {
            loading = false;
            if (!IsDisposed)
                refreshButton.Enabled = true;
        }
    }

    private void Filters_Changed(object? sender, EventArgs e)
    {
        if (!IsHandleCreated || DesignMode)
            return;
        RenderServices();
    }

    private void RenderServices()
    {
        var selected = ServiceRules.Select(services, searchTextBox.Text,
            Math.Max(0, discountComboBox.SelectedIndex), Math.Max(0, sortComboBox.SelectedIndex));
        servicesFlowLayoutPanel.SuspendLayout();
        try
        {
            foreach (var old in servicesFlowLayoutPanel.Controls.Cast<Control>().ToArray())
                old.Dispose();
            servicesFlowLayoutPanel.Controls.Clear();
            foreach (var service in selected)
            {
                var card = new ServiceCardControl();
                card.Bind(service, session.IsAdmin);
                card.EditRequested += ServiceCard_EditRequested;
                card.DeleteRequested += ServiceCard_DeleteRequested;
                card.BookRequested += ServiceCard_BookRequested;
                servicesFlowLayoutPanel.Controls.Add(card);
            }
            ResizeCards();
        }
        finally { servicesFlowLayoutPanel.ResumeLayout(true); }
        emptyLabel.Visible = selected.Count == 0;
        if (emptyLabel.Visible)
            emptyLabel.BringToFront();
        countLabel.Text = $"{selected.Count} из {services.Count} услуг";
    }

    private void ResizeCards()
    {
        var width = Math.Max(700, servicesFlowLayoutPanel.ClientSize.Width
            - servicesFlowLayoutPanel.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 4);
        foreach (Control card in servicesFlowLayoutPanel.Controls)
            card.Width = width;
    }

    private void ServicesFlowLayoutPanel_SizeChanged(object? sender, EventArgs e) => ResizeCards();

    private void ResetButton_Click(object? sender, EventArgs e)
    {
        searchTextBox.Clear();
        discountComboBox.SelectedIndex = 0;
        sortComboBox.SelectedIndex = 0;
    }

    private void AdminButton_Click(object? sender, EventArgs e)
    {
        if (session.IsAdmin)
            session.Logout();
        else
        {
            using var login = new AdminLoginForm();
            if (login.ShowDialog(this) != DialogResult.OK || !session.Login(login.Code))
                return;
        }
        adminPanel.Visible = session.IsAdmin;
        adminButton.Text = session.IsAdmin ? "Выйти из режима администратора" : "Вход администратора";
        RenderServices();
    }

    private async void RefreshButton_Click(object? sender, EventArgs e) => await RunAsync(LoadServicesAsync);
    private async void AddButton_Click(object? sender, EventArgs e) => await RunAsync(() => EditServiceAsync(null));

    private async void ServiceCard_EditRequested(object? sender, EventArgs e)
    {
        if (sender is ServiceCardControl { Service: { } service })
            await RunAsync(() => EditServiceAsync(service));
    }

    private async Task EditServiceAsync(Service? service)
    {
        session.RequireAdmin();
        if (repository is null)
            throw new InvalidOperationException("Сначала подключитесь к базе: нажмите «Обновить».");
        if (editForm is not null)
        {
            editForm.Activate();
            return;
        }
        try
        {
            using var form = new ServiceEditForm(repository, service);
            editForm = form;
            if (form.ShowDialog(this) == DialogResult.OK)
                await LoadServicesAsync();
        }
        finally { editForm = null; }
    }

    private async void ServiceCard_DeleteRequested(object? sender, EventArgs e)
    {
        if (sender is not ServiceCardControl { Service: { } service } || repository is null)
            return;
        await RunAsync(async () =>
        {
            session.RequireAdmin();
            if (MessageBox.Show(this, $"Удалить услугу «{service.Title}»?\nЭто действие нельзя отменить.",
                "Леарн — подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            await repository.DeleteServiceAsync(service.Id);
            await LoadServicesAsync();
        });
    }

    private async void ServiceCard_BookRequested(object? sender, EventArgs e)
    {
        if (sender is not ServiceCardControl { Service: { } service } || repository is null)
            return;
        await RunAsync(() =>
        {
            session.RequireAdmin();
            using var form = new ClientServiceForm(repository, service);
            form.ShowDialog(this);
            return Task.CompletedTask;
        });
    }

    private async void UpcomingButton_Click(object? sender, EventArgs e) => await RunAsync(() =>
    {
        session.RequireAdmin();
        if (repository is null)
            throw new InvalidOperationException("Сначала подключитесь к базе данных.");
        using var form = new UpcomingServicesForm(repository);
        form.ShowDialog(this);
        return Task.CompletedTask;
    });

    private async Task RunAsync(Func<Task> action)
    {
        try { await action(); }
        catch (Exception ex)
        {
            if (!IsDisposed)
                UserMessages.Error(this, ex);
        }
    }
}
