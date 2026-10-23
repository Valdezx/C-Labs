using ClinicApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;



namespace ClinicApp.Models;

public class Appointment
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }

    private bool _isPaid;
    public DateTime ScheduledAt { get; set; }
    private int _durationMinutes = 0;
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public string Notes { get; private set; } = string.Empty;


    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;
    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = string.Empty;
    }
    public bool IsPaid
    {
        get
        {
            if (Status == AppointmentStatus.Cancelled)
            {
                return false; 
            }
            return _isPaid;
        }
    }
    public decimal GetCost()
    {
       double minutes = DurationMinutes;
        return (decimal)minutes * 10;


    }
    public void MarkAsPaid()
    {
        
        if (Status == AppointmentStatus.Cancelled)
        {
            return;
        }
        _isPaid = true;
    }
    public string CancellationReason => Status == AppointmentStatus.Cancelled ? Notes : string.Empty;
    public bool Cancel(string reason)
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Cancelled;
            Notes = reason;
            return true;
        }

        return false;
    }
    public bool Complete()
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Completed;
            return true;
        }

        return false;
    }
    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Тривалість прийому повинна бути додатнім числом.");
            }
            _durationMinutes = value;
        }
    }

    public override string ToString()
    {
        string baseInfo = $"[Запис #{Id}] Пацієнт ID: {PatientId} | Лікар ID: {DoctorId} | Час: {ScheduledAt:yyyy-MM-dd HH:mm} - {EndsAt:HH:mm} ({DurationMinutes} хв) | Статус: {Status}";

        if (!string.IsNullOrWhiteSpace(Notes))
        {
            baseInfo += $" | Примітка: {Notes}";
        }

        return baseInfo;
    }
    public bool IsCancelled()
    {
        if (Status == AppointmentStatus.Cancelled)
        {
            return true;
        }
        return false;
    }
    
}