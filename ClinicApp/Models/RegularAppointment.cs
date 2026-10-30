using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Managers;

namespace ClinicApp.Models
{
    public class RegularAppointment : Appointment
    {
        public int RecurrenceIntervalDays { get; private set; }
        public RegularAppointment(int patientId, int doctorId, DateTime scheduledAt, int recurrenceIntervalDays, int durationMinutes = 30)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            RecurrenceIntervalDays = recurrenceIntervalDays;
        }
        public override string GetDescription()
        {
            return $"Звичайний прийом у лікаря (ID: {DoctorId}) для пацієнта (ID: {PatientId}) на {DurationMinutes} хвилин, запланований на {ScheduledAt:yyyy-MM-dd HH:mm}, з інтервалом повторення {RecurrenceIntervalDays} днів.";
        }
    }
}
