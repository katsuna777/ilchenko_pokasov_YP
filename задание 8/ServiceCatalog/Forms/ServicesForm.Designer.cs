namespace ServiceCatalog.Forms;

partial class ServicesForm
{
    private TextBox searchBox = null!;
    private ComboBox sortBox = null!;
    private FlowLayoutPanel rowsPanel = null!;
    private Label countLabel = null!;
    private FlowLayoutPanel controlsPanel = null!;

    private void InitializeComponent()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        controlsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 0, 0, 8) };
        searchBox = new TextBox { Width = 300, PlaceholderText = "Название или описание услуги" };
        sortBox = new ComboBox { Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
        sortBox.Items.AddRange(new[] { "Наименование по возрастанию", "Наименование по убыванию", "Стоимость по возрастанию", "Стоимость по убыванию" });
        sortBox.SelectedIndex = 0;
        var searchGroup = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        searchGroup.Controls.Add(new Label { Text = "Поиск услуг", AutoSize = true });
        searchGroup.Controls.Add(searchBox);
        var sortGroup = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        sortGroup.Controls.Add(new Label { Text = "Сортировка", AutoSize = true });
        sortGroup.Controls.Add(sortBox);
        var refresh = new Button { Text = "Обновить", AutoSize = true, Margin = new Padding(8, 22, 0, 0) };
        controlsPanel.Controls.AddRange(new Control[] { searchGroup, sortGroup, refresh });
        rowsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = false, FlowDirection = FlowDirection.TopDown };
        countLabel = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        layout.Controls.Add(controlsPanel, 0, 0);
        layout.Controls.Add(rowsPanel, 0, 1);
        layout.Controls.Add(countLabel, 0, 2);
        Controls.Add(layout);
        searchBox.TextChanged += (_, _) => Render();
        sortBox.SelectedIndexChanged += (_, _) => Render();
        rowsPanel.ClientSizeChanged += (_, _) => ResizeRows();
        refresh.Click += async (_, _) => await ReloadAsync();
        Shown += async (_, _) => await ReloadAsync();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(850, 730);
        MinimumSize = new Size(650, 450);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Список услуг";
    }
}
