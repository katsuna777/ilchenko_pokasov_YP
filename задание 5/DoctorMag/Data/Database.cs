using DoctorMag.Models;
using MySqlConnector;

namespace DoctorMag.Data;

public static class Database
{
    private const string ConnectionString = "Server=85.236.189.123;Port=13306;Database=isp-44_ilchenko_dm_doctormag5;User ID=isp-44_ilchenko_dm;Password=isp-44_ilchenko_dm;Character Set=utf8mb4;SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=10";

    internal static MySqlConnection Open()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    private static string Text(MySqlDataReader reader, string name) => reader[name] is DBNull ? "" : Convert.ToString(reader[name]) ?? "";
    private static Doctor ReadDoctor(MySqlDataReader r) => new(Convert.ToInt32(r["doctor_id"]), Text(r, "doctor_name"), Text(r, "specialty"), Text(r, "schedule"), Text(r, "doctor_visit_type"), Text(r, "doctor_status"), Text(r, "photo"));

    public static Session? Login(string login, string password)
    {
        using var connection = Open();
        using var command = new MySqlCommand("SELECT id, full_name, role FROM users WHERE login=@login AND BINARY password=BINARY @password", connection);
        command.Parameters.AddWithValue("@login", login);
        command.Parameters.AddWithValue("@password", password);
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read()) return new Session(reader.GetInt32("id"), Text(reader, "full_name"), Text(reader, "role"), false);
        }
        using var patientCommand = new MySqlCommand("SELECT id, full_name FROM patients WHERE SUBSTRING_INDEX(full_name,' ',1)=@login AND policy=@password", connection);
        patientCommand.Parameters.AddWithValue("@login", login);
        patientCommand.Parameters.AddWithValue("@password", password);
        using var patient = patientCommand.ExecuteReader();
        return patient.Read() ? new Session(patient.GetInt32("id"), Text(patient, "full_name"), "Клиент", true) : null;
    }

    public static List<Doctor> Doctors()
    {
        using var connection = Open();
        using var command = new MySqlCommand("SELECT d.id doctor_id,d.full_name doctor_name,s.name specialty,d.schedule,m.name doctor_visit_type,d.status doctor_status,d.photo FROM doctors d JOIN specialties s ON s.id=d.specialty_id LEFT JOIN service_modes m ON m.id=d.service_mode_id", connection);
        using var reader = command.ExecuteReader();
        var list = new List<Doctor>();
        while (reader.Read()) list.Add(ReadDoctor(reader));
        return list;
    }

    public static List<Visit> Visits(Session session)
    {
        using var connection = Open();
        var sql = "SELECT v.id,v.created_at,v.visit_at,CONCAT_WS(', ',k.name,vm.name) visit_type,v.status,u.full_name registrar,p.id patient_id,p.full_name patient_name,p.policy,d.id doctor_id,d.full_name doctor_name,s.name specialty,d.schedule,m.name doctor_visit_type,d.status doctor_status,d.photo FROM visits v JOIN patients p ON p.id=v.patient_id JOIN doctors d ON d.id=v.doctor_id JOIN specialties s ON s.id=d.specialty_id LEFT JOIN service_modes m ON m.id=d.service_mode_id JOIN visit_kinds k ON k.id=v.kind_id LEFT JOIN service_modes vm ON vm.id=v.service_mode_id LEFT JOIN users u ON u.id=v.created_by";
        if (session.IsPatient) sql += " WHERE p.id=@patient";
        using var command = new MySqlCommand(sql, connection);
        if (session.IsPatient) command.Parameters.AddWithValue("@patient", session.Id);
        using var reader = command.ExecuteReader();
        var list = new List<Visit>();
        while (reader.Read())
            list.Add(new Visit(reader.GetInt32("id"), new Patient(reader.GetInt32("patient_id"), Text(reader, "patient_name"), Text(reader, "policy")), ReadDoctor(reader), reader["created_at"] is DBNull ? null : reader.GetDateTime("created_at"), reader.GetDateTime("visit_at"), Text(reader, "visit_type"), Text(reader, "status"), Text(reader, "registrar")));
        return list;
    }

    public static Patient? FindPatient(string policy)
    {
        using var connection = Open();
        using var command = new MySqlCommand("SELECT id,full_name,policy FROM patients WHERE policy=@policy", connection);
        command.Parameters.AddWithValue("@policy", policy);
        using var reader = command.ExecuteReader();
        return reader.Read() ? new Patient(reader.GetInt32("id"), Text(reader, "full_name"), Text(reader, "policy")) : null;
    }

    public static long AddVisit(Session session, Patient patient, Doctor doctor, DateTime date, string type)
    {
        if (session.IsGuest || session.IsPatient) throw new InvalidOperationException("Запись может оформить только сотрудник.");
        if (type is not ("Первичное" or "Повторное" or "Срочное"))
            throw new InvalidOperationException("Выберите тип посещения.");
        if (patient.Id <= 0) throw new InvalidOperationException("Найдите пациента по номеру полиса ОМС.");
        using var connection = Open();
        using var transaction = connection.BeginTransaction();
        using var check = new MySqlCommand("SELECT status,schedule FROM doctors WHERE id=@id FOR UPDATE", connection, transaction);
        check.Parameters.AddWithValue("@id", doctor.Id);
        using (var reader = check.ExecuteReader())
        {
            if (!reader.Read()) throw new InvalidOperationException("Врач не найден.");
            BookingRules.Validate(Text(reader, "status"), Text(reader, "schedule"), date);
        }
        using var insert = new MySqlCommand("INSERT INTO visits(patient_id,doctor_id,created_at,visit_at,kind_id,service_mode_id,created_by) SELECT @patient,d.id,NOW(),@date,k.id,d.service_mode_id,@user FROM doctors d JOIN visit_kinds k ON k.name=@type WHERE d.id=@doctor", connection, transaction);
        insert.Parameters.AddWithValue("@patient", patient.Id);
        insert.Parameters.AddWithValue("@doctor", doctor.Id);
        insert.Parameters.AddWithValue("@date", date);
        insert.Parameters.AddWithValue("@type", type);
        insert.Parameters.AddWithValue("@user", session.Id);
        if (insert.ExecuteNonQuery() != 1) throw new InvalidOperationException("Не удалось определить вид приёма.");
        transaction.Commit();
        return insert.LastInsertedId;
    }
}
