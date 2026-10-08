using System;
using System.Collections.Generic;
using System.Text;

using System;
using ClinicApp.Managers;

namespace ClinicApp;

internal class Clinic
{
    public string Name { get; set; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"\n=== Розклад на {date:dd.MM.yyyy} ===");
        var dailyAppointments = Appointments.GetByDate(date);

        if (dailyAppointments.Length == 0)
        {
            Console.WriteLine("На цю дату записів немає.");
            return;
        }

        for (int i = 0; i < dailyAppointments.Length; i++)
        {
            var app = dailyAppointments[i];
            var patient = Patients.FindById(app.PatientId);
            var doctor = Doctors.FindById(app.DoctorId);

            string pName = patient != null ? patient.FullName : $"ID:{app.PatientId}";
            string dName = doctor != null ? doctor.FullName : $"ID:{app.DoctorId}";

            Console.WriteLine($"[{app.Id}] {pName} -> {dName} | {app.ScheduledAt:dd.MM.yyyy HH:mm}-{app.EndsAt:HH:mm} | {app.Status}");
        }
    }

 
    public void GenerateReport()
    {
        Console.WriteLine($"\n========================================");
        Console.WriteLine($"             ЗВІТ КЛІНІКИ               ");
        Console.WriteLine($"Назва: {Name}");
        Console.WriteLine($"========================================");
        Console.WriteLine($"Всього зареєстровано пацієнтів: {Patients.Count}");
        Console.WriteLine($"Всього працює лікарів:          {Doctors.Count}");
        Console.WriteLine($"Майбутніх активних записів:     {Appointments.GetUpcoming().Length}");
        Console.WriteLine($"----------------------------------------");
        Console.WriteLine("Навантаження на лікарів (кількість записів):");

        var allDoctors = Doctors.GetAllDoctors();
        if (allDoctors.Length == 0)
        {
            Console.WriteLine("  Лікарів немає в базі.");
        }
        else
        {
            foreach (var doc in allDoctors)
            {
                int docAppointmentsCount = Appointments.GetByDoctor(doc.Id).Length;
                Console.WriteLine($"  - {doc.FullName} ({doc.Speciality}): {docAppointmentsCount} запис(ів)");
            }
        }
        
    }
}