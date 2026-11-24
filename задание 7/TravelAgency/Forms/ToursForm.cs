using TravelAgency.Data;
using TravelAgency.Models;

namespace TravelAgency.Forms;

public partial class ToursForm : Form
{
    private Catalog catalog = new(Array.Empty<Tour>(), Array.Empty<TourType>());
    private bool updating;
    private List<Tour> displayed = new();

    public ToursForm()
    {
        InitializeComponent();
        SetCatalog(catalog);
    }

    private async Task ReloadAsync()
    {
        filters.Enabled = false;
        UseWaitCursor = true;
        countLabel.Text = "Загрузка туров…";
        try
        {
            var loaded = await new TourRepository().LoadAsync();
            if (!IsDisposed) SetCatalog(loaded);
        }
        catch (Exception error)
        {
            if (!IsDisposed)
            {
                countLabel.Text = "Не удалось обновить список. Нажмите «Обновить», чтобы повторить.";
                MessageBox.Show(this, $"Не удалось загрузить туры. Проверьте доступ к серверу MySQL и повторите обновление.\n\n{error.Message}", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        finally
        {
            if (!IsDisposed)
            {
                filters.Enabled = true;
                UseWaitCursor = false;
            }
        }
    }

    internal void SetCatalog(Catalog loaded)
    {
        var selectedId = (typeBox.SelectedItem as TourType)?.Id ?? 0;
        updating = true;
        try
        {
            catalog = loaded;
            typeBox.Items.Clear();
            typeBox.Items.Add(new TourType(0, "Все типы"));
            foreach (var type in loaded.Types) typeBox.Items.Add(type);
            var index = loaded.Types.ToList().FindIndex(type => type.Id == selectedId);
            typeBox.SelectedIndex = index < 0 ? 0 : index + 1;
        }
        finally { updating = false; }
        Render();
    }

    private void Render()
    {
        if (updating || IsDisposed) return;
        var typeId = (typeBox.SelectedItem as TourType)?.Id ?? 0;
        displayed = TourFilter.Apply(catalog.Tours, searchBox.Text, typeId == 0 ? null : typeId, actualOnlyBox.Checked, sortBox.SelectedIndex == 1);
        tiles.SuspendLayout();
        while (tiles.Controls.Count > 0) tiles.Controls[0].Dispose();
        foreach (var tour in displayed) tiles.Controls.Add(new TourCard(tour));
        ResizeTiles();
        tiles.ResumeLayout();
        totalLabel.Text = $"Общая стоимость туров\n{TourFilter.Total(displayed).ToString("N2", TourCard.Russian)} РУБ";
        countLabel.Text = displayed.Count == 0 ? $"Ничего не найдено · 0 из {catalog.Tours.Count}" : $"Туров: {displayed.Count} из {catalog.Tours.Count}";
    }

    private void ResizeTiles()
    {
        var available = Math.Max(300, tiles.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2);
        var columns = Math.Max(1, available / 310);
        var width = available / columns - 14;
        foreach (Control card in tiles.Controls) card.Width = width;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) logo?.Image?.Dispose();
        base.Dispose(disposing);
    }
}
