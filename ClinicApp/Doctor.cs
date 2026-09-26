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

        public Specialty Specialty { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string LicenseNumber { get; set; }

        public string Phone { get; set; }

        public int WorkStartHour { get; set; }

        public int WorkEndHour { get; set; }

        public int WorkingHoursPerDay => WorkEndHour - WorkStartHour;

        public string WorkSchedule => $"{WorkStartHour}:00 - {WorkEndHour}:00";

        public bool IsAvailable(int hour) => hour >= WorkStartHour && hour < WorkEndHour;

        public Doctor() : this("Невідомо", "Невідомо", "Невідомо") { }

        public Doctor(string firstName, string lastName, Specialty specialty) : this(firstName, lastName, Specialty.General, "Невідомо", 0, 0) { }

        public Doctor(string fullName, Specialty specialty, string licenseNumber, string phone, int workStartHour = 8, int workEndHour = 17)
        {
            FullName = fullName;
            Specialty = specialty;
            LicenseNumber = licenseNumber;
            Phone = phone;
            WorkStartHour = workStartHour;
            WorkEndHour = workEndHour;
        }
        public string CanAcceptAt (int hour)
        {
            if (IsAvailable(hour))
            {
                return $"Зараз доступний.";
            }
            else
            {
                return $"Наразі не доступний.";
            }
        }
        public override string ToString()
        {
            return $"Лікар  №: {Id}, Ім'я: {FullName}, Спеціалізація: {Specialty}, Ліцензія: {LicenseNumber}, Телефон: {Phone}, Робочий графік: {WorkSchedule}";
        }
    }
}
