using ServiceCatalog.Data;
using ServiceCatalog.Models;

namespace ServiceCatalog.Forms;

public partial class ServicesForm : Form
{
    private List<Service> services = new();
    private List<Service> displayed = new();

    public ServicesForm()
    {
        InitializeComponent();
        Render();
    }

    private async Task ReloadAsync()
    {
        controlsPanel.Enabled = false;
        UseWaitCursor = true;
        countLabel.Text = "Загрузка услуг…";
        try
        {
            var loaded = await new ServiceRepository().LoadAsync();
            if (!IsDisposed) SetServices(loaded);
        }
        catch (Exception error)
        {
            if (!IsDisposed)
            {
                countLabel.Text = "Не удалось обновить услуги.";
                MessageBox.Show(this, $"Проверьте доступ к серверу MySQL и нажмите «Обновить».\n\n{error.Message}", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        finally
        {
            if (!IsDisposed)
            {
                controlsPanel.Enabled = true;
                UseWaitCursor = false;
            }
        }
    }

    internal void SetServices(List<Service> loaded)
    {
        services = loaded;
        Render();
    }

    private void Render()
    {
        if (IsDisposed) return;
        displayed = ServiceSelection.Apply(services, searchBox.Text, sortBox.SelectedIndex);
        rowsPanel.SuspendLayout();
        while (rowsPanel.Controls.Count > 0) rowsPanel.Controls[0].Dispose();
        foreach (var service in displayed) rowsPanel.Controls.Add(new ServiceRow(service));
        ResizeRows();
        rowsPanel.ResumeLayout(true);
        rowsPanel.Invalidate(true);
        countLabel.Text = $"{displayed.Count} из {services.Count}" + (displayed.Count == 0 ? " · Ничего не найдено" : "");
    }

    private void ResizeRows()
    {
        var width = Math.Max(200, rowsPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4);
        foreach (Control row in rowsPanel.Controls) row.Width = width;
    }
}
