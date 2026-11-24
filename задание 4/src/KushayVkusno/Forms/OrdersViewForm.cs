using KushayVkusno.Data;

namespace KushayVkusno.Forms;

public class OrdersViewForm : Form
{
    private readonly Repository repo;
    private readonly User currentUser;
    private readonly DataGridView ordersGrid;
    private readonly ListBox itemsListBox;
    private readonly Label totalCountLabel;
    private List<Order> loadedOrders = new();

    public OrdersViewForm(Repository repo, User user)
    {
        this.repo = repo;
        this.currentUser = user;

        Theme.Apply(this);
        Text = "КушайВкусно — Список заказов (Администратор)";
        ClientSize = new Size(950, 600);
        StartPosition = FormStartPosition.CenterParent;

        var header = new Label
        {
            Location = new Point(20, 15),
            Size = new Size(500, 30),
            Font = Theme.TitleFont,
            ForeColor = Theme.AccentAction,
            Text = "Журнал заказов"
        };

        var refreshBtn = new Button
        {
            Location = new Point(810, 12),
            Size = new Size(115, 34),
            Text = "Обновить",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        refreshBtn.FlatAppearance.BorderSize = 0;
        refreshBtn.Click += async (s, e) => await LoadOrdersAsync();

        totalCountLabel = new Label
        {
            Location = new Point(20, 50),
            Size = new Size(400, 24),
            Font = Theme.BaseFont,
            Text = "Загрузка заказов..."
        };

        ordersGrid = new DataGridView
        {
            Location = new Point(20, 80),
            Size = new Size(580, 480),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White
        };

        ordersGrid.Columns.Add("id", "№");
        ordersGrid.Columns.Add("date", "Дата");
        ordersGrid.Columns.Add("waiter", "Официант");
        ordersGrid.Columns.Add("table", "Стол");
        ordersGrid.Columns.Add("status", "Статус");
        ordersGrid.Columns.Add("total", "Сумма");

        ordersGrid.Columns["id"]!.FillWeight = 30;
        ordersGrid.Columns["date"]!.FillWeight = 80;
        ordersGrid.Columns["waiter"]!.FillWeight = 110;
        ordersGrid.Columns["table"]!.FillWeight = 40;
        ordersGrid.Columns["status"]!.FillWeight = 60;
        ordersGrid.Columns["total"]!.FillWeight = 60;

        ordersGrid.SelectionChanged += OrdersGrid_SelectionChanged;

        var detailsHeader = new Label
        {
            Location = new Point(620, 80),
            Size = new Size(300, 24),
            Font = Theme.HeaderFont,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Text = "Состав выбранного заказа:"
        };

        itemsListBox = new ListBox
        {
            Location = new Point(620, 110),
            Size = new Size(305, 450),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right,
            Font = Theme.BaseFont
        };

        Controls.Add(header);
        Controls.Add(refreshBtn);
        Controls.Add(totalCountLabel);
        Controls.Add(ordersGrid);
        Controls.Add(detailsHeader);
        Controls.Add(itemsListBox);

        Shown += async (s, e) => await LoadOrdersAsync();
    }

    private async Task LoadOrdersAsync()
    {
        try
        {
            loadedOrders = await repo.GetOrdersAsync();
            ordersGrid.Rows.Clear();
            foreach (var o in loadedOrders)
            {
                int r = ordersGrid.Rows.Add(o.Id, o.OrderDate.ToString("dd.MM.yyyy HH:mm"), o.WaiterName, o.TableNumber, o.StatusName, $"{o.TotalPrice:N2} ₽");
                ordersGrid.Rows[r].Tag = o;
            }
            totalCountLabel.Text = $"Всего заказов в базе: {loadedOrders.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OrdersGrid_SelectionChanged(object? sender, EventArgs e)
    {
        itemsListBox.Items.Clear();
        if (ordersGrid.CurrentRow?.Tag is Order o)
        {
            itemsListBox.Items.Add($"Заказ №{o.Id}");
            itemsListBox.Items.Add($"Стол: {o.TableNumber}");
            itemsListBox.Items.Add($"Официант: {o.WaiterName}");
            itemsListBox.Items.Add($"Статус: {o.StatusName}");
            if (!string.IsNullOrWhiteSpace(o.Details))
            {
                itemsListBox.Items.Add($"Пожелания: {o.Details}");
            }
            itemsListBox.Items.Add("----------------------------------------");
            itemsListBox.Items.Add("Позиции:");
            foreach (var item in o.Items)
            {
                itemsListBox.Items.Add($"• {item.DishName}");
                itemsListBox.Items.Add($"  {item.Quantity} шт. × {item.Price:N2} ₽ = {item.Total:N2} ₽");
            }
            itemsListBox.Items.Add("----------------------------------------");
            itemsListBox.Items.Add($"Всего блюд: {o.ItemsCount} шт.");
            itemsListBox.Items.Add($"ИТОГО: {o.TotalPrice:N2} ₽");
        }
    }
}
