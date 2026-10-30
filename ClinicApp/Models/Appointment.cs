using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;
using ClinicApp.Managers;

namespace ClinicApp.Models
{
    public class Appointment : IPayable, ICancellable
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

        public virtual decimal GetCost()
        {
            return (decimal)DurationMinutes * 10m;
        }
        public virtual string GetDescription()
        {
            return $"Прийом у лікаря (ID: {DoctorId}) для пацієнта (ID: {PatientId}) на {DurationMinutes} хвилин, запланований на {ScheduledAt:yyyy-MM-dd HH:mm}.";
        }
        public int GetPriority()
        {
            return 3;
        }
        public void MarkPaid()
        {
            if (Status == AppointmentStatus.Cancelled)
            {
                return;
            }
            _isPaid = true;
        }

        public bool IsCancelled => Status == AppointmentStatus.Cancelled;

        public string CancellationReason => Status == AppointmentStatus.Cancelled ? Notes : string.Empty;

        public bool Cancel(string reason = "")
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
            string baseInfo = GetDescription();

            if (!string.IsNullOrWhiteSpace(Notes))
            {
                baseInfo += $" | Примітка: {Notes}";
            }

            return baseInfo;
        }
    }
}