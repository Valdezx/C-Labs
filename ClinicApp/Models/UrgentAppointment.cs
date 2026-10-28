using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Models
{
    public class UrgentAppointment : Appointment
    {
        public string UrgencyNote { get; }
        public UrgentAppointment(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote, int durationMinutes = 30)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            UrgencyNote = urgencyNote;
        }
        public override decimal GetCost()
        {
            return base.GetCost() * 1.5m; 
        }
        public sealed override string GetDescription()
        {
            return $"Терміновий прийом у лікаря (ID: {DoctorId}) для пацієнта (ID: {PatientId}) на {DurationMinutes} хвилин, запланований на {ScheduledAt:yyyy-MM-dd HH:mm}.";
        }
        public new int GetPriority()
        {
            return 1; 
        }
        
    }
}
