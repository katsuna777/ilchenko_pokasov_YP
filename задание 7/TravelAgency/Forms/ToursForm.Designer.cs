namespace TravelAgency.Forms;

partial class ToursForm
{
    private TextBox searchBox = null!;
    private ComboBox typeBox = null!;
    private ComboBox sortBox = null!;
    private CheckBox actualOnlyBox = null!;
    private Label totalLabel = null!;
    private Label countLabel = null!;
    private FlowLayoutPanel tiles = null!;
    private FlowLayoutPanel filters = null!;
    private Button reloadButton = null!;
    private PictureBox logo = null!;

    private void InitializeComponent()
    {
        SuspendLayout();
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(12) };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 85));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        var header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        logo = new PictureBox
        {
            Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, TabStop = false,
            Image = Image.FromFile(Path.Combine(AppContext.BaseDirectory, "Resources", "logo_full_color.png"))
        };
        totalLabel = new Label { Text = "Общая стоимость туров", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, ForeColor = Color.FromArgb(68, 92, 147) };
        header.Controls.Add(logo, 0, 0);
        header.Controls.Add(totalLabel, 1, 0);

        filters = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 8) };
        searchBox = new TextBox { Width = 270, PlaceholderText = "Название или описание", MaxLength = 1000 };
        typeBox = new ComboBox { Width = 270, DropDownStyle = ComboBoxStyle.DropDownList, DropDownWidth = 500 };
        sortBox = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
        sortBox.Items.AddRange(new[] { "Цена по возрастанию", "Цена по убыванию" });
        sortBox.SelectedIndex = 0;
        AddFilter("Поиск", searchBox);
        AddFilter("Тип тура", typeBox);
        AddFilter("Сортировка", sortBox);
        actualOnlyBox = new CheckBox { Text = "Только актуальные туры", AutoSize = true, Margin = new Padding(8, 27, 8, 0) };
        reloadButton = new Button { Text = "Обновить", AutoSize = true, Margin = new Padding(8, 23, 8, 0) };
        filters.Controls.Add(actualOnlyBox);
        filters.Controls.Add(reloadButton);

        tiles = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true, FlowDirection = FlowDirection.LeftToRight, BackColor = Color.White };
        countLabel = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        layout.Controls.Add(header, 0, 0);
        layout.Controls.Add(filters, 0, 1);
        layout.Controls.Add(tiles, 0, 2);
        layout.Controls.Add(countLabel, 0, 3);
        Controls.Add(layout);
        searchBox.TextChanged += (_, _) => Render();
        typeBox.SelectedIndexChanged += (_, _) => Render();
        sortBox.SelectedIndexChanged += (_, _) => Render();
        actualOnlyBox.CheckedChanged += (_, _) => Render();
        reloadButton.Click += async (_, _) => await ReloadAsync();
        tiles.ClientSizeChanged += (_, _) => ResizeTiles();
        Shown += async (_, _) => await ReloadAsync();
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Comic Sans MS", 10);
        BackColor = Color.White;
        ForeColor = Color.Black;
        Text = "Путешествуй по России — туры";
        ClientSize = new Size(1200, 820);
        MinimumSize = new Size(760, 570);
        StartPosition = FormStartPosition.CenterScreen;
        ResumeLayout(false);
    }

    private void AddFilter(string title, Control control)
    {
        var group = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false, Margin = new Padding(0, 0, 12, 0) };
        group.Controls.Add(new Label { Text = title, AutoSize = true });
        group.Controls.Add(control);
        filters.Controls.Add(group);
    }
}
