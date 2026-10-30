using System;
using System.Collections.Generic;
using System.Text;

using System;
using ClinicApp.Models;
using ClinicApp.Interfaces;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private readonly RegularAppointment[] _appointments = new RegularAppointment[MaxAppointments];

    private readonly  UrgentAppointment[] _urgentAppointments = new UrgentAppointment[MaxAppointments];

    private readonly SpecialistAppointment[] _specialistAppointments = new SpecialistAppointment[MaxAppointments];
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

        var appointment = new RegularAppointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count++] = appointment;
        Console.WriteLine($"Запис #{appointment.Id} успішно заброньовано.");
        return true;
    }
    public bool BookUrgent(int patientId, int doctorId, DateTime scheduledAt,string urgencyNote, int durationMinutes = 30)
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
        var appointment = new UrgentAppointment(patientId, doctorId, scheduledAt, urgencyNote, durationMinutes= 30);
        _urgentAppointments[_count++] = appointment;
        Console.WriteLine($"Терміновий запис #{appointment.Id} успішно заброньовано.");
        return true;
    }
    public bool BookSpecialist(int patientId, int doctorId, DateTime scheduledAt, string consulationTopic, int durationMinutes = 30)
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
        var appointment = new SpecialistAppointment(patientId, doctorId, scheduledAt, consulationTopic, durationMinutes);
        _specialistAppointments[_count++] = appointment;
        Console.WriteLine($"Запис до спеціаліста #{appointment.Id} успішно заброньовано.");
        return true;
    }
    public RegularAppointment[] GetRegular()
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] != null)
            {
                matchCount++;
            }
        }
        var result = new RegularAppointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] != null)
            {
                result[idx++] = _appointments[i];
            }
        }
        return result;
    }
    public UrgentAppointment[] GetUrgent()
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_urgentAppointments[i] != null)
            {
                matchCount++;
            }
        }
        var result = new UrgentAppointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_urgentAppointments[i] != null)
            {
                result[idx++] = _urgentAppointments[i];
            }
        }
        return result;

    }
    public SpecialistAppointment[] GetSpecialist()
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_specialistAppointments[i] != null)
            {
                matchCount++;
            }
        }
        var result = new SpecialistAppointment[matchCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_specialistAppointments[i] != null)
            {
                result[idx++] = _specialistAppointments[i];
            }
        }
        return result;
    }


    public bool Cancel(int id, string reason)
    {
        var app = FindById(id);
        if (app != null)
        {
            return app.Cancel(reason);
        }
        return false;
    }

    
    public bool Complete(int id)
    {
        var app = FindById(id);
        if (app != null)
        {
            return app.Complete();
        }
        return false;
    }

    
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

        string line = $"[{appointment.Id}] {appointment.GetDescription()} | Пацієнт: {patientName} -> Лікар: {doctorName} | Час: {appointment.ScheduledAt:yyyy-MM-dd HH:mm}-{appointment.EndsAt:HH:mm} | Статус: {appointment.Status}" +
            $" | Вартість: {appointment.GetCost():F2} грн";

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
    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item != null && item.Cancel(reason))
            {
                count++;
            }
        }
        return count;
    }
    
    public Appointment this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException($"Індекс {index} виходить за межі діапазону (0..{_count - 1}).");

            return _appointments[index];
        }
    }
    
    public Appointment[] GetByDate(int year, int month, int day)
    {
        DateTime date = new DateTime(year, month, day);
        return GetByDate(date);
    }
    
 
}