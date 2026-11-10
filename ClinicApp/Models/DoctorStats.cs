using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using System.Collections;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class DoctorStats : IComparable<DoctorStats>
    {
        public int DoctorId { get; }

        public string FullName { get; }

        public int AppointmentsCount { get; }

        public decimal TotalRevenue { get; }

        public readonly DateTime LastAppointmentDate = DateTime.MinValue;

        public DoctorStats(int doctorId, string fullName, int appointmentsCount, decimal totalRevenue, DateTime lastAppointmentDate)
        {
            DoctorId = doctorId;
            FullName = fullName;
            AppointmentsCount = appointmentsCount;
            TotalRevenue = totalRevenue;
            LastAppointmentDate = lastAppointmentDate;
        }
        public int CompareTo(DoctorStats other)
        {
            if (other == null) return 1;

            return other.AppointmentsCount.CompareTo(this.AppointmentsCount);

        }

        public override string ToString()
        {
            return $"DoctorId: {DoctorId}, FullName: {FullName}, AppointmentsCount: {AppointmentsCount}, TotalRevenue: {TotalRevenue}, LastAppointmentDate: {LastAppointmentDate}";
        }

    }
}
