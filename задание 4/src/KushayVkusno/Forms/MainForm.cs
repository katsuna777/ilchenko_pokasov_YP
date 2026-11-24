using KushayVkusno.Controls;
using KushayVkusno.Data;

namespace KushayVkusno.Forms;

public class MainForm : Form
{
    private readonly Repository repo;
    private readonly User currentUser;

    private List<Dish> allDishes = new();
    private List<Category> allCategories = new();
    private readonly List<DishCardControl> dishCards = new();

    private readonly FlowLayoutPanel listPanel;
    private readonly TextBox searchBox;
    private readonly ComboBox categoryFilter;
    private readonly ComboBox sortBox;
    private readonly Label countLabel;
    private readonly Button createOrderBtn;
    private readonly Button addDishBtn;
    private readonly Button editDishBtn;
    private readonly Button deleteDishBtn;
    private readonly Button viewOrdersBtn;

    private OrderCreateForm? openOrderForm;

    public MainForm(Repository repo, User user)
    {
        this.repo = repo;
        this.currentUser = user;

        Theme.Apply(this);
        Text = $"КушайВкусно — Каталог блюд ({user.RoleName})";
        ClientSize = new Size(1050, 720);
        MinimumSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;

        var topPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 70,
            BackColor = Theme.SecondaryBackground
        };

        var logoBox = new PictureBox
        {
            Location = new Point(12, 10),
            Size = new Size(50, 50),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Theme.LoadImage("logo.png")
        };

        var brandLabel = new Label
        {
            Location = new Point(70, 18),
            Size = new Size(240, 32),
            Font = Theme.TitleFont,
            ForeColor = Theme.AccentAction,
            Text = "КушайВкусно"
        };

        var userLabel = new Label
        {
            Location = new Point(450, 15),
            Size = new Size(450, 40),
            Font = Theme.BaseFont,
            ForeColor = Theme.TextDark,
            TextAlign = ContentAlignment.MiddleRight,
            Text = $"{currentUser.FullName}\n({currentUser.RoleName})"
        };

        var logoutBtn = new Button
        {
            Location = new Point(910, 16),
            Size = new Size(115, 36),
            Text = "Выйти",
            Font = Theme.BaseFont,
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        logoutBtn.FlatAppearance.BorderSize = 0;
        logoutBtn.Click += (s, e) => { DialogResult = DialogResult.Retry; Close(); };

        topPanel.Controls.Add(logoBox);
        topPanel.Controls.Add(brandLabel);
        topPanel.Controls.Add(userLabel);
        topPanel.Controls.Add(logoutBtn);

        var filterPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 75,
            BackColor = Theme.MainBackground
        };

        countLabel = new Label
        {
            Location = new Point(15, 8),
            Size = new Size(400, 22),
            Font = Theme.HeaderFont,
            ForeColor = Theme.AccentAction,
            Text = "Загрузка..."
        };

        var searchLbl = new Label { Location = new Point(15, 38), Size = new Size(50, 24), Text = "Поиск:" };
        searchBox = new TextBox { Location = new Point(70, 35), Size = new Size(240, 28) };
        searchBox.TextChanged += (s, e) => ApplyFilters();

        var catLbl = new Label { Location = new Point(330, 38), Size = new Size(80, 24), Text = "Категория:" };
        categoryFilter = new ComboBox { Location = new Point(415, 35), Size = new Size(180, 28), DropDownStyle = ComboBoxStyle.DropDownList };
        categoryFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

        var sortLbl = new Label { Location = new Point(615, 38), Size = new Size(90, 24), Text = "Сортировка:" };
        sortBox = new ComboBox { Location = new Point(710, 35), Size = new Size(200, 28), DropDownStyle = ComboBoxStyle.DropDownList };
        sortBox.Items.AddRange(new object[] { "Без сортировки", "Стоимость: по возрастанию", "Стоимость: по убыванию" });
        sortBox.SelectedIndex = 0;
        sortBox.SelectedIndexChanged += (s, e) => ApplyFilters();

        var resetBtn = new Button
        {
            Location = new Point(925, 33),
            Size = new Size(100, 32),
            Text = "Сброс",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        resetBtn.FlatAppearance.BorderSize = 0;
        resetBtn.Click += (s, e) =>
        {
            searchBox.Clear();
            categoryFilter.SelectedIndex = 0;
            sortBox.SelectedIndex = 0;
        };

        filterPanel.Controls.Add(countLabel);
        filterPanel.Controls.Add(searchLbl);
        filterPanel.Controls.Add(searchBox);
        filterPanel.Controls.Add(catLbl);
        filterPanel.Controls.Add(categoryFilter);
        filterPanel.Controls.Add(sortLbl);
        filterPanel.Controls.Add(sortBox);
        filterPanel.Controls.Add(resetBtn);

        var bottomPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 65,
            BackColor = Theme.MainBackground
        };

        createOrderBtn = new Button
        {
            Location = new Point(15, 12),
            Size = new Size(230, 40),
            Text = "Оформить заказ",
            Font = Theme.HeaderFont,
            BackColor = Theme.AccentAction,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        createOrderBtn.FlatAppearance.BorderSize = 0;
        createOrderBtn.Click += CreateOrder_Click;

        bool isAdmin = currentUser.RoleId == 2;

        addDishBtn = new Button
        {
            Location = new Point(260, 12),
            Size = new Size(180, 40),
            Text = "Добавить блюдо",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Visible = isAdmin,
            Cursor = Cursors.Hand
        };
        addDishBtn.FlatAppearance.BorderSize = 0;
        addDishBtn.Click += AddDish_Click;

        editDishBtn = new Button
        {
            Location = new Point(450, 12),
            Size = new Size(160, 40),
            Text = "Редактировать",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Visible = isAdmin,
            Cursor = Cursors.Hand
        };
        editDishBtn.FlatAppearance.BorderSize = 0;
        editDishBtn.Click += EditDish_Click;

        deleteDishBtn = new Button
        {
            Location = new Point(620, 12),
            Size = new Size(150, 40),
            Text = "Удалить блюдо",
            Font = Theme.BaseFont,
            BackColor = Color.FromArgb(255, 230, 230),
            FlatStyle = FlatStyle.Flat,
            Visible = isAdmin,
            Cursor = Cursors.Hand
        };
        deleteDishBtn.FlatAppearance.BorderSize = 0;
        deleteDishBtn.Click += DeleteDish_Click;

        viewOrdersBtn = new Button
        {
            Location = new Point(810, 12),
            Size = new Size(215, 40),
            Text = "Просмотр заказов",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Visible = isAdmin,
            Cursor = Cursors.Hand
        };
        viewOrdersBtn.FlatAppearance.BorderSize = 0;
        viewOrdersBtn.Click += (s, e) =>
        {
            using var ordersForm = new OrdersViewForm(repo, currentUser);
            ordersForm.ShowDialog(this);
        };

        bottomPanel.Controls.Add(createOrderBtn);
        bottomPanel.Controls.Add(addDishBtn);
        bottomPanel.Controls.Add(editDishBtn);
        bottomPanel.Controls.Add(deleteDishBtn);
        bottomPanel.Controls.Add(viewOrdersBtn);

        listPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.FromArgb(248, 253, 253)
        };
        listPanel.SizeChanged += (s, e) => ResizeCards();

        Controls.Add(listPanel);
        Controls.Add(bottomPanel);
        Controls.Add(filterPanel);
        Controls.Add(topPanel);

        Shown += async (s, e) => await ReloadAsync();
    }

    private async Task ReloadAsync()
    {
        try
        {
            allCategories = await repo.GetCategoriesAsync();
            categoryFilter.Items.Clear();
            categoryFilter.Items.Add("Все категории");
            foreach (var c in allCategories) categoryFilter.Items.Add(c.Name);
            categoryFilter.SelectedIndex = 0;

            allDishes = await repo.GetDishesAsync();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyFilters()
    {
        string query = searchBox.Text.Trim();
        string selectedCat = categoryFilter.SelectedItem?.ToString() ?? "Все категории";

        var filtered = allDishes.AsEnumerable();

        if (!string.IsNullOrEmpty(query))
        {
            filtered = filtered.Where(d =>
                d.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.CategoryName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.Price.ToString().Contains(query) ||
                d.Id.ToString().Contains(query));
        }

        if (selectedCat != "Все категории")
        {
            filtered = filtered.Where(d => d.CategoryName == selectedCat);
        }

        if (sortBox.SelectedIndex == 1)
        {
            filtered = filtered.OrderBy(d => d.Price);
        }
        else if (sortBox.SelectedIndex == 2)
        {
            filtered = filtered.OrderByDescending(d => d.Price);
        }

        var resultList = filtered.ToList();

        listPanel.SuspendLayout();
        foreach (var c in listPanel.Controls.Cast<Control>().ToList()) c.Dispose();
        listPanel.Controls.Clear();
        dishCards.Clear();

        foreach (var d in resultList)
        {
            var card = new DishCardControl(d, showSelection: true);
            card.SelectionChanged += (s, e) => UpdateOrderButtonState();
            dishCards.Add(card);
            listPanel.Controls.Add(card);
        }
        ResizeCards();
        listPanel.ResumeLayout(true);

        countLabel.Text = $"{resultList.Count} из {allDishes.Count}";
        UpdateOrderButtonState();
    }

    private void ResizeCards()
    {
        int w = Math.Max(700, listPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 15);
        foreach (Control c in listPanel.Controls) c.Width = w;
    }

    private void UpdateOrderButtonState()
    {
        int selectedCount = dishCards.Count(c => c.IsSelected);
        createOrderBtn.Text = selectedCount > 0 ? $"Оформить заказ ({selectedCount})" : "Оформить заказ";
        createOrderBtn.Enabled = selectedCount > 0;
    }

    private void CreateOrder_Click(object? sender, EventArgs e)
    {

        if (openOrderForm != null && !openOrderForm.IsDisposed)
        {
            openOrderForm.BringToFront();
            return;
        }

        var selectedDishes = dishCards.Where(c => c.IsSelected).Select(c => c.Dish).ToList();
        if (selectedDishes.Count == 0)
        {
            MessageBox.Show("Пожалуйста, выберите хотя бы одно блюдо для оформления заказа.", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        openOrderForm = new OrderCreateForm(repo, currentUser, selectedDishes);
        openOrderForm.FormClosed += (s, ev) => openOrderForm = null;

        if (openOrderForm.ShowDialog(this) == DialogResult.OK)
        {

            foreach (var card in dishCards) card.SetChecked(false);
            UpdateOrderButtonState();
        }
    }

    private async void AddDish_Click(object? sender, EventArgs e)
    {
        using var editForm = new DishEditForm(repo, null, allCategories);
        if (editForm.ShowDialog(this) == DialogResult.OK)
        {
            await ReloadAsync();
        }
    }

    private async void EditDish_Click(object? sender, EventArgs e)
    {
        var selected = dishCards.Where(c => c.IsSelected).Select(c => c.Dish).ToList();
        if (selected.Count != 1)
        {
            MessageBox.Show("Выберите ровно одно блюдо для редактирования (отметьте галочкой).", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var editForm = new DishEditForm(repo, selected[0], allCategories);
        if (editForm.ShowDialog(this) == DialogResult.OK)
        {
            await ReloadAsync();
        }
    }

    private async void DeleteDish_Click(object? sender, EventArgs e)
    {
        var selected = dishCards.Where(c => c.IsSelected).Select(c => c.Dish).ToList();
        if (selected.Count != 1)
        {
            MessageBox.Show("Выберите ровно одно блюдо для удаления (отметьте галочкой).", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var dish = selected[0];
        if (MessageBox.Show($"Вы действительно хотите удалить блюдо «{dish.Name}»?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await repo.DeleteDishAsync(dish.Id);
            MessageBox.Show("Блюдо успешно удалено.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await ReloadAsync();
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось удалить блюдо: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
