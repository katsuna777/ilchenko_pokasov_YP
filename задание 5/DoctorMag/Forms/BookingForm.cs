using DoctorMag.Data;
using DoctorMag.Models;

namespace DoctorMag.Forms;

public partial class BookingForm : Form
{
    private Session? session;
    private Doctor? doctor;
    private Patient? patient;

    public BookingForm() => InitializeComponent();

    public BookingForm(Session session, Doctor doctor, Patient? patient) : this()
    {
        this.session = session;
        this.doctor = doctor;
        this.patient = patient;
        Text = patient is null ? "ДокторМаг — первичный приём" : "ДокторМаг — повторный приём";
        registrar.Text = session.Name;
        created.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
        specialist.Text = $"{doctor.Name}\n{doctor.Specialty} · {doctor.Schedule}";
        visitTime.Value = DateTime.Today.AddDays(1).Add(BookingRules.GetHours(doctor.Status, doctor.Schedule).Start);
        visitType.SelectedIndex = patient is null ? 0 : 1;
        if (patient is not null)
        {
            policy.Text = patient.Policy;
            this.patient = patient;
            policy.ReadOnly = true;
            lookup.Enabled = false;
            visitor.Text = patient.Name;
        }
    }

    private void FindPatient(object? sender, EventArgs e)
    {
        try
        {
            patient = Database.FindPatient(policy.Text.Trim());
            visitor.Text = patient?.Name ?? "Пациент не найден";
        }
        catch (Exception error) { Theme.Error(this, error); }
    }

    private void PolicyChanged(object? sender, EventArgs e)
    {
        patient = null;
        visitor.Text = "Введите полис и нажмите «Найти»";
    }

    private void Save(object? sender, EventArgs e)
    {
        if (session is null || doctor is null) return;
        try
        {
            if (patient is null) throw new InvalidOperationException("Найдите пациента по номеру полиса ОМС.");
            var date = visitDate.Value.Date.AddHours(visitTime.Value.Hour).AddMinutes(visitTime.Value.Minute);
            Database.AddVisit(session, patient, doctor, date, visitType.Text);
            MessageBox.Show(this, "Запись добавлена.", "ДокторМаг", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception error) { Theme.Error(this, error); }
    }
}
