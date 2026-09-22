using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace ClinicApp;

internal class AppointmentManager
{
    private const int MaxAppointments = 500;
    private readonly Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;

    private readonly PatientManager _patients;
    private readonly DoctorManager _doctors;

    public int Count => _count;

    public AppointmentManager(PatientManager patientManager, DoctorManager doctorManager)
    {
        _patients = patientManager;
        _doctors = doctorManager;
    }

    
    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Помилка: досягнуто ліміту записів (500).");
            return false;
        }

        if (_patients.FindById(patientId) == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID [{patientId}] не існує.");
            return false;
        }

        if (_doctors.FindById(doctorId) == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID [{doctorId}] не існує.");
            return false;
        }

        var appointment = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count++] = appointment;
        Console.WriteLine($"Запис #{appointment.Id} успішно заброньовано.");
        return true;
    }

    // Знаходить запис та делегує виклик appointment.Cancel()
    public bool Cancel(int id, string reason)
    {
        var app = FindById(id);
        if (app != null)
        {
            return app.Cancel(reason);
        }
        return false;
    }

    // Знаходить запис та делегує виклик appointment.Complete()
    public bool Complete(int id)
    {
        var app = FindById(id);
        if (app != null)
        {
            return app.Complete();
        }
        return false;
    }

    // Всі записи конкретного пацієнта
    public Appointment[] GetByPatient(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                matchCount++;
            }
        }

        var result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    // Всі записи лікаря
    public Appointment[] GetByDoctor(int doctorId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                matchCount++;
            }
        }

        var result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    // Всі записи на певну календарну дату
    public Appointment[] GetByDate(DateTime date)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                matchCount++;
            }
        }

        var result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    // Всі майбутні активні записи
    public Appointment[] GetUpcoming()
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                matchCount++;
            }
        }

        var result = new Appointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }

    public void DisplayAppointment(Appointment appointment)
    {
        if (appointment == null) return;

        var patient = _patients.FindById(appointment.PatientId);
        var doctor = _doctors.FindById(appointment.DoctorId);

        string patientName = patient != null ? patient.FullName : $"ID:{appointment.PatientId} (видалений)";
        string doctorName = doctor != null ? doctor.FullName : $"ID:{appointment.DoctorId} (видалений)";

        string line = $"[Запис #{appointment.Id}] Пацієнт: {patientName} | Лікар: {doctorName} | Час: {appointment.ScheduledAt:yyyy-MM-dd HH:mm}-{appointment.EndsAt:HH:mm} ({appointment.DurationMinutes} хв) | Статус: {appointment.Status}";

        if (!string.IsNullOrWhiteSpace(appointment.Notes))
        {
            line += $" | Примітка: {appointment.Notes}";
        }

        Console.WriteLine(line);
    }

    public void DisplayList(Appointment[] list)
    {
        if (list == null || list.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            DisplayAppointment(list[i]);
        }
    }

    public Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }
        return null;
    }

    public Appointment[] GetAll()
    {
        var result = new Appointment[_count];
        Array.Copy(_appointments, result, _count);
        return result;
    }
}