using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Models
{
    public sealed class SpecialistAppointment : Appointment
    {
        public string ConsulationTopic { get; }

        public SpecialistAppointment(int patientId, int doctorId, DateTime scheduledAt, string consulationTopic, int durationMinutes = 30)
            : base(patientId, doctorId, scheduledAt, durationMinutes)
        {
            ConsulationTopic = consulationTopic;
        }
        public override decimal GetCost()
        {
            return base.GetCost() * 1.3m;
        }
        public override string GetDescription()
        {
            return $"Прийом у спеціаліста (ID: {DoctorId}) для пацієнта (ID: {PatientId}) на {DurationMinutes} хвилин, запланований на {ScheduledAt:yyyy-MM-dd HH:mm}, тема консультації: {ConsulationTopic}.";
        }
    }
}
