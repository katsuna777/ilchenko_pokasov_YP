namespace DoctorMag.Forms;

partial class BookingForm
{
    private Label registrar = null!;
    private Label created = null!;
    private Label specialist = null!;
    private Label visitor = null!;
    private TextBox policy = null!;
    private Button lookup = null!;
    private ComboBox visitType = null!;
    private DateTimePicker visitDate = null!;
    private DateTimePicker visitTime = null!;

    private void InitializeComponent()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 2 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        var logo = Theme.Picture(Theme.Asset("logo.png"));
        logo.Size = new Size(156, 60);
        layout.Controls.Add(logo, 0, 0);
        layout.SetColumnSpan(logo, 2);
        registrar = AddValue(layout, 1, "Оформляет");
        created = AddValue(layout, 2, "Дата и время записи");
        specialist = AddValue(layout, 3, "Врач и направление");
        var policyLayout = new FlowLayoutPanel { AutoSize = true, WrapContents = false };
        policy = new TextBox { Width = 190, MaxLength = 40 };
        lookup = new Button { Text = "Найти", AutoSize = true };
        Theme.StyleButton(lookup, isAccent: false);
        policyLayout.Controls.AddRange(new Control[] { policy, lookup });
        AddField(layout, 4, "Номер полиса ОМС", policyLayout);
        visitor = AddValue(layout, 5, "Пациент");
        visitType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        visitType.Items.AddRange(new[] { "Первичное", "Повторное", "Срочное" });
        AddField(layout, 6, "Тип посещения", visitType);
        visitDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy", Width = 160, Value = DateTime.Today.AddDays(1) };
        AddField(layout, 7, "Дата посещения", visitDate);
        visitTime = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true, Width = 115 };
        AddField(layout, 8, "Время посещения", visitTime);
        var actions = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
        var cancel = new Button { Text = "Назад", AutoSize = true, DialogResult = DialogResult.Cancel };
        Theme.StyleButton(cancel, isAccent: false);
        var save = new Button { Text = "Сохранить", AutoSize = true };
        Theme.StyleButton(save, isAccent: true);
        actions.Controls.AddRange(new Control[] { save, cancel });
        layout.Controls.Add(actions, 0, 9);
        layout.SetColumnSpan(actions, 2);
        lookup.Click += FindPatient;
        policy.TextChanged += PolicyChanged;
        save.Click += Save;
        AcceptButton = save;
        CancelButton = cancel;
        Controls.Add(layout);
        AutoScaleMode = AutoScaleMode.Font;
        Theme.Apply(this);
        ClientSize = new Size(615, 490);
        MinimumSize = new Size(615, 490);
        Icon = new Icon(Theme.Asset("icon.ico"));
        StartPosition = FormStartPosition.CenterParent;
        Text = "ДокторМаг — запись на приём";
    }

    private static Label AddValue(TableLayoutPanel layout, int row, string title)
    {
        var value = new Label { AutoSize = true, Padding = new Padding(0, 5, 0, 5) };
        AddField(layout, row, title, value);
        return value;
    }

    private static void AddField(TableLayoutPanel layout, int row, string title, Control field)
    {
        layout.Controls.Add(new Label { Text = title, AutoSize = true, Padding = new Padding(0, 5, 0, 5) }, 0, row);
        layout.Controls.Add(field, 1, row);
    }
}
