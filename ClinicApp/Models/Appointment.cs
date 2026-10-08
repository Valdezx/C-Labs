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
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public string Notes { get; private set; } = string.Empty;

    // Обчислювана властивість: час завершення прийому
    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

    // Обчислювана властивість: чи прийом є майбутнім і активним
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

    // Конструктор: призначає Id, Status = AppointmentStatus.Scheduled, durationMinutes за замовчуванням 30
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

    // Скасування: якщо Status == AppointmentStatus.Scheduled -> AppointmentStatus.Cancelled, зберігає причину в Notes
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

    // Завершення: якщо Status == AppointmentStatus.Scheduled -> AppointmentStatus.Completed
    public bool Complete()
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Completed;
            return true;
        }

        return false;
    }

    // Перевизначений ToString: виводить рядок з ID, а не іменами
    public override string ToString()
    {
        string baseInfo = $"[Запис #{Id}] Пацієнт ID: {PatientId} | Лікар ID: {DoctorId} | Час: {ScheduledAt:yyyy-MM-dd HH:mm} - {EndsAt:HH:mm} ({DurationMinutes} хв) | Статус: {Status}";

        if (!string.IsNullOrWhiteSpace(Notes))
        {
            baseInfo += $" | Примітка: {Notes}";
        }

        return baseInfo;
    }
}