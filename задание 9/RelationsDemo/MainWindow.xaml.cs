using System.Windows;
using Microsoft.EntityFrameworkCore;
using RelationsDemo.Data;

namespace RelationsDemo;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    public async Task LoadDataAsync()
    {
        await using var context = new TravelContext();

        var gridRows = await context.Tours.AsNoTracking().OrderBy(tour => tour.Id).ToListAsync();
        var listRows = await context.Tours.AsNoTracking().OrderBy(tour => tour.Id).ToListAsync();

        var gridCountries = await context.Tours.AsNoTracking()
            .Include(tour => tour.Country).OrderBy(tour => tour.Id).ToListAsync();
        var listCountries = await context.Tours.AsNoTracking()
            .Include(tour => tour.Country).OrderBy(tour => tour.Id).ToListAsync();

        var gridTypes = await context.Tours.AsNoTracking()
            .Include(tour => tour.TypeOfTours).ThenInclude(link => link.Type)
            .OrderBy(tour => tour.Id).ToListAsync();
        var listTypes = await context.Tours.AsNoTracking()
            .Include(tour => tour.TypeOfTours).ThenInclude(link => link.Type)
            .OrderBy(tour => tour.Id).ToListAsync();

        SingleGrid.ItemsSource = gridRows;
        SingleList.ItemsSource = listRows;
        IncludeGrid.ItemsSource = gridCountries;
        IncludeList.ItemsSource = listCountries;
        ThenIncludeGrid.ItemsSource = gridTypes;
        ThenIncludeList.ItemsSource = listTypes;
        StatusLabel.Text = $"Загружено {gridRows.Count} туров в каждый пример";
    }

    internal async Task ReloadAsync()
    {
        ReloadButton.IsEnabled = false;
        StatusLabel.Text = "Загрузка…";
        try { await LoadDataAsync(); }
        catch (Exception error)
        {
            StatusLabel.Text = "Ошибка подключения. Повторите обновление.";
            MessageBox.Show(this, error.Message, "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally { ReloadButton.IsEnabled = true; }
    }

    private async void ReloadClick(object sender, RoutedEventArgs e) => await ReloadAsync();
}
