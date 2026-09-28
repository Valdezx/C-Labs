using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Speciality Speciality { get; set; } = Speciality.General;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Заміна окремих годин на структуру WorkSchedule
    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}".Trim();

    public bool IsAvailable(int hour) => Schedule.Contains(hour);

    // Конструктор за замовчуванням
    public Doctor()
        : this("Невідомо", "Невідомо", Speciality.General, "LIC-000", "", new WorkSchedule(8, 17)) { }

    // Скорочений конструктор
    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "LIC-000", "", new WorkSchedule(8, 17)) { }

    // Конструктор, який приймає години окремо (для зручності сумісності)
    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, int workStartHour, int workEndHour)
        : this(firstName, lastName, speciality, licenseNumber, phone, new WorkSchedule(workStartHour, workEndHour)) { }

    // Повний конструктор зі структурою WorkSchedule
    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public string CanAcceptAt(int hour)
    {
        return IsAvailable(hour) ? "Зараз доступний." : "Наразі не доступний.";
    }

    public override string ToString()
    {
        return $"Лікар №: {Id}, Ім'я: {FullName}, Спеціалізація: {ClinicFormatter.FormatSpeciality(Speciality)}, Ліцензія: {LicenseNumber}, Телефон: {ClinicFormatter.FormatPhone(Phone)}, Графік: {Schedule}";
    }
}