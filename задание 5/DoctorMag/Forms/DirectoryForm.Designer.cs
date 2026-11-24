namespace DoctorMag.Forms;

partial class DirectoryForm
{
    private FlowLayoutPanel filters = null!;
    private FlowLayoutPanel cards = null!;
    private Label identity = null!;
    private Label resultCount = null!;
    private Button doctorsButton = null!;
    private Button visitsButton = null!;
    private Button newVisitButton = null!;

    private void InitializeComponent()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 5, Padding = new Padding(16) };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        var header = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3 };
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        var logo = Theme.Picture(Theme.Asset("logo.png"));
        logo.Dock = DockStyle.Fill;
        identity = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
        var exit = new Button { Text = "Выйти", Anchor = AnchorStyles.Right, AutoSize = true };
        Theme.StyleButton(exit, isAccent: false);
        exit.Click += (_, _) => Close();
        header.Controls.Add(logo, 0, 0);
        header.Controls.Add(identity, 1, 0);
        header.Controls.Add(exit, 2, 0);
        var navigation = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        doctorsButton = new Button { Text = "Сотрудники", AutoSize = true };
        Theme.StyleButton(doctorsButton, isAccent: false);
        visitsButton = new Button { Text = "Посещения", AutoSize = true };
        Theme.StyleButton(visitsButton, isAccent: false);
        newVisitButton = new Button { Text = "Новая запись", AutoSize = true };
        Theme.StyleButton(newVisitButton, isAccent: true);
        var reload = new Button { Text = "Обновить", AutoSize = true };
        Theme.StyleButton(reload, isAccent: false);
        doctorsButton.Click += (_, _) => ShowDirectory(false);
        visitsButton.Click += (_, _) => ShowDirectory(true);
        newVisitButton.Click += (_, _) => ShowDirectory(false);
        reload.Click += (_, _) => Reload();
        navigation.Controls.AddRange(new Control[] { doctorsButton, visitsButton, newVisitButton, reload });
        filters = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
        cards = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        cards.SizeChanged += (_, _) => FitCards();
        resultCount = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
        layout.Controls.Add(header, 0, 0);
        layout.Controls.Add(navigation, 0, 1);
        layout.Controls.Add(filters, 0, 2);
        layout.Controls.Add(cards, 0, 3);
        layout.Controls.Add(resultCount, 0, 4);
        Controls.Add(layout);
        AutoScaleMode = AutoScaleMode.Font;
        Theme.Apply(this);
        ClientSize = new Size(1150, 730);
        MinimumSize = new Size(850, 570);
        Icon = new Icon(Theme.Asset("icon.ico"));
        StartPosition = FormStartPosition.CenterParent;
        Text = "ДокторМаг";
    }
}
