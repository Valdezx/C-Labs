using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp
{
    public class Doctor
    {
        private static int _nextId = 1;

        public int Id { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Specialty { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string LicenseNumber { get; set; }

        public string Phone { get; set; }

        public int WorkStartHour { get; set; }

        public int WorkEndHour { get; set; }

        public int WorkingHoursPerDay => WorkEndHour - WorkStartHour;

        public string WorkSchedule => $"{WorkStartHour}:00 - {WorkEndHour}:00";

        public bool IsAvailable(int hour) => hour >= WorkStartHour && hour < WorkEndHour;

        public Doctor() : this("Невідомо", "Невідомо", "Невідомо") { }

        public Doctor(string firstName, string lastName, string specialty) : this(firstName, lastName, specialty, "Невідомо", 0, 0) { }

        public Doctor(string firstName, string lastName, string specialty, string licenseNumber, int workStartHour, int workEndHour)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            WorkStartHour = workStartHour;
            WorkEndHour = workEndHour;
        }
    }
}
