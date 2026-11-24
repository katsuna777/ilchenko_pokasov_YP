using System.Text.RegularExpressions;
using DoctorMag.Data;
using DoctorMag.Models;

namespace DoctorMag.Forms;

public partial class DirectoryForm : Form
{
    private const string Reset = "Сбросить фильтрацию";
    private Session? session;
    private bool showingVisits;
    private bool updatingFilters;
    private TextBox search = null!;
    private ComboBox sort = null!;
    private readonly List<(ComboBox Control, int Field)> activeFilters = new();
    private List<Row> rows = new();
    private record Row(Doctor Doctor, Visit? Visit, string[] Fields, string SearchText);

    public DirectoryForm() => InitializeComponent();

    public DirectoryForm(Session session) : this()
    {
        this.session = session;
        identity.Text = session.IsGuest ? "Гость" : $"{session.Role} · {session.Name}";
        doctorsButton.Visible = !session.IsPatient;
        visitsButton.Visible = !session.IsGuest;
        ShowDirectory(session.IsPatient);
    }

    private static string Shift(Doctor doctor)
    {
        var match = Regex.Match(doctor.Schedule, @"(\d+)\s*смена", RegexOptions.IgnoreCase);
        if (match.Success) return $"Смена {match.Groups[1].Value}";
        return doctor.Schedule.TrimStart() switch
        {
            var value when value.StartsWith("8:00") || value.StartsWith("08:00") => "Смена 1",
            var value when value.StartsWith("12:00") => "Смена 2",
            var value when value.StartsWith("16:00") => "Смена 3",
            _ => "Не указана"
        };
    }

    private static Row DoctorRow(Doctor doctor) => new(doctor, null,
        new[] { doctor.Status, doctor.VisitType, doctor.Specialty, Shift(doctor) }, $"{doctor} {Shift(doctor)}");

    private static Row VisitRow(Visit visit)
    {
        var doctor = visit.Doctor;
        return new Row(doctor, visit,
            new[] { visit.Status, visit.VisitType, doctor.Specialty }, $"{visit} {visit.VisitAt:g} {visit.CreatedAt:g} {Shift(doctor)}");
    }

    private List<Row> LoadRows(bool visits) => visits
        ? Database.Visits(session!).Select(VisitRow).ToList()
        : Database.Doctors().Select(DoctorRow).ToList();

    private void Reload()
    {
        try
        {
            rows = LoadRows(showingVisits);
            updatingFilters = true;
            try
            {
                foreach (var (control, field) in activeFilters)
                {
                    var selected = control.Text;
                    Populate(control, field);
                    control.SelectedIndex = Math.Max(0, control.Items.IndexOf(selected));
                }
            }
            finally { updatingFilters = false; }
            Render();
        }
        catch (Exception error) { Theme.Error(this, error); }
    }

    private void ShowDirectory(bool visits)
    {
        if (session is null || (visits && session.IsGuest) || (!visits && session.IsPatient)) return;
        try { rows = LoadRows(visits); }
        catch (Exception error) { Theme.Error(this, error); return; }

        showingVisits = visits;
        Text = visits ? "ДокторМаг — посещения" : "ДокторМаг — сотрудники";
        newVisitButton.Visible = visits && !session.IsPatient;
        ClearControls(filters);
        activeFilters.Clear();
        search = new TextBox { Width = 176, PlaceholderText = "Поиск" };
        AddFilterControl("Поиск", search);
        sort = new ComboBox { Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
        sort.Items.AddRange(visits ? new[] { "Дата ↑", "Дата ↓" } : new[] { "ФИО ↑", "ФИО ↓", "Направление ↑", "Направление ↓" });
        sort.SelectedIndex = 0;
        AddFilterControl("Сортировка", sort);
        var labels = visits ? new[] { "Статус", "Тип", "Направление" } : new[] { "Статус", "Тип", "Направление", "Смена" };
        for (var index = 0; index < labels.Length; index++)
        {
            var control = new ComboBox { Width = 164, DropDownStyle = ComboBoxStyle.DropDownList };
            Populate(control, index);
            control.SelectedIndex = 0;
            activeFilters.Add((control, index));
            AddFilterControl(labels[index], control);
            control.SelectedIndexChanged += (_, _) => { if (control.SelectedIndex >= 0) Render(); };
        }
        search.TextChanged += (_, _) => Render();
        sort.SelectedIndexChanged += (_, _) => Render();
        Render();
    }

    private void Populate(ComboBox control, int field)
    {
        control.Items.Clear();
        control.Items.Add(Reset);
        var values = rows.Select(row => row.Fields[field]).Where(value => value.Length > 0)
            .Select(value => showingVisits && field == 1 ? value.Split(',')[0] : value)
            .Distinct().OrderBy(value => value).ToArray();
        control.Items.AddRange(values);
    }

    private void AddFilterControl(string label, Control control)
    {
        var box = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        box.Controls.Add(new Label { Text = label, AutoSize = true });
        box.Controls.Add(control);
        filters.Controls.Add(box);
    }

    private static void ClearControls(Control parent)
    {
        while (parent.Controls.Count != 0) parent.Controls[0].Dispose();
    }

    private void Render()
    {
        if (updatingFilters) return;
        var filtered = rows.Where(row => row.SearchText.Contains(search.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)
            && activeFilters.All(filter => filter.Control.SelectedIndex <= 0 || filter.Control.Text ==
                (showingVisits && filter.Field == 1 ? row.Fields[filter.Field].Split(',')[0] : row.Fields[filter.Field])));
        var descending = sort.SelectedIndex % 2 == 1;
        var ordered = showingVisits ? filtered.OrderBy(row => row.Visit!.VisitAt)
            : filtered.OrderBy(row => sort.SelectedIndex >= 2 ? row.Doctor.Specialty : row.Doctor.Name, StringComparer.CurrentCultureIgnoreCase);
        var displayed = (descending ? ordered.Reverse() : ordered).ToList();
        cards.SuspendLayout();
        ClearControls(cards);
        foreach (var row in displayed) cards.Controls.Add(CreateCard(row));
        FitCards();
        cards.ResumeLayout();
        resultCount.Text = displayed.Count == 0 ? $"0 из {rows.Count} · ничего не найдено" : $"{displayed.Count} из {rows.Count}";
    }

    private Control CreateCard(Row row)
    {
        var doctor = row.Doctor;
        var visit = row.Visit;
        var present = doctor.Status == "Присутствует";
        var card = new TableLayoutPanel
        {
            ColumnCount = 2, RowCount = 2, Height = visit is null ? 190 : 242,
            Margin = new Padding(0, 4, 0, 4), Padding = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = visit is null && !present ? Color.LightGray : SystemColors.Window
        };
        card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
        card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
        card.RowStyles.Add(new RowStyle(SizeType.Absolute, 94));
        card.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var relativePhoto = Path.Combine("Doctors", (Path.GetFileName(doctor.Photo) ?? "").Normalize());
        var file = Theme.Asset(relativePhoto);
        var picture = Theme.Picture(doctor.Photo.Length > 0 && File.Exists(file) ? file : Theme.Asset("picture.png"));
        picture.Dock = DockStyle.Fill;
        picture.BorderStyle = BorderStyle.FixedSingle;
        card.Controls.Add(picture, 0, 0);

        var shift = Shift(doctor).Replace("Смена ", "");
        var hours = Regex.Match(doctor.Schedule, @"\d{1,2}:\d{2}\s*[-–]\s*\d{1,2}:\d{2}");
        var summary = new Label
        {
            Text = $"{doctor.Name}\nСмена: {shift}\nРежим работы: {(hours.Success ? hours.Value : "Не указан")}",
            Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(8), TextAlign = ContentAlignment.MiddleLeft
        };
        card.Controls.Add(summary, 1, 0);

        var details = new TableLayoutPanel
        {
            ColumnCount = 2, RowCount = 1, Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(5)
        };
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        details.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        card.Controls.Add(details, 0, 1);
        card.SetColumnSpan(details, 2);
        var fields = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown, WrapContents = false, Dock = DockStyle.Fill
        };
        details.Controls.Add(fields, 0, 0);
        AddCardField(fields, $"Направление: {doctor.Specialty}");
        if (visit is null)
        {
            AddCardField(fields, $"Тип посещения: {doctor.VisitType}");
            var status = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = Padding.Empty };
            AddCardField(status, "Статус:");
            var value = AddCardField(status, doctor.Status);
            value.ForeColor = present ? Color.DarkGreen : Color.DarkRed;
            fields.Controls.Add(status);
        }
        else
        {
            AddCardField(fields, $"Посетитель: {visit.Patient.Name}");
            AddCardField(fields, $"Тип посещения: {visit.VisitType}");
            AddCardField(fields, $"Дата и время посещения: {visit.VisitAt:dd.MM.yyyy HH:mm}");
            var created = visit.CreatedAt?.ToString("dd.MM.yyyy HH:mm") ?? "Не указаны";
            AddCardField(fields, $"Дата и время записи: {created}");
            AddCardField(fields, $"Статус: {visit.Status}");
            if (visit.Registrar.Length > 0) AddCardField(fields, $"Оформил: {visit.Registrar}");
        }

        if (session is { IsGuest: false, IsPatient: false } && present)
        {
            var action = new Button
            {
                Text = visit is null ? "Записать" : "Повторный приём",
                AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            Theme.StyleButton(action, isAccent: true);
            action.Click += (_, _) => Book(row);
            details.Controls.Add(action, 1, 0);
        }
        return card;
    }

    private static Label AddCardField(Control parent, string text)
    {
        var label = new Label { Text = text, AutoSize = true, Margin = new Padding(3, 1, 3, 1) };
        parent.Controls.Add(label);
        return label;
    }

    private void FitCards()
    {
        foreach (Control card in cards.Controls)
            card.Width = Math.Max(100, cards.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);
    }

    private void Book(Row row)
    {
        try
        {
            BookingRules.GetHours(row.Doctor.Status, row.Doctor.Schedule);
            using var booking = new BookingForm(session!, row.Doctor, row.Visit?.Patient);
            if (booking.ShowDialog(this) == DialogResult.OK)
            {
                if (showingVisits) Reload();
                else ShowDirectory(true);
            }
        }
        catch (Exception error) { Theme.Error(this, error); }
    }
}
