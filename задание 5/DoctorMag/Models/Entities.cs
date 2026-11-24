namespace DoctorMag.Models;

public record Session(int Id, string Name, string Role, bool IsPatient, bool IsGuest = false);
public record Doctor(int Id, string Name, string Specialty, string Schedule, string VisitType, string Status, string Photo);
public record Patient(int Id, string Name, string Policy);
public record Visit(int Id, Patient Patient, Doctor Doctor, DateTime? CreatedAt, DateTime VisitAt, string VisitType, string Status, string Registrar);
