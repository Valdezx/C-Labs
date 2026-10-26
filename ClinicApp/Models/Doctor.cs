using ClinicApp.Enums;
using ClinicApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Interfaces;

namespace ClinicApp.Models
{
    public class Doctor : ISchedulable
    {
        private static int _nextId = 1;

        public int Id { get; }
        private string _firstName = "";
        private string _lastName = "";
        public Speciality Speciality { get; set; } = Speciality.General;
        private string _licenseNumber = "";
        private string _phone = "";

        public string FirstName
        {
            get => _firstName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(FirstName));
                _firstName = value.Trim();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(LastName));
                _lastName = value.Trim();
            }
        }

        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Номер ліцензії не може бути порожнім.");
                }
                else
                {
                    _licenseNumber = value.Trim();
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                ClinicValidator.ValidatePhone(value);
                _phone = value.Trim();
            }
        }

        public WorkSchedule Schedule { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public bool IsAvailable(int hour) => Schedule.Contains(hour);

        public Doctor()
            : this("Невідомо", "Невідомо", Speciality.General, "LIC-000", "", new WorkSchedule(8, 17)) { }

        public Doctor(string firstName, string lastName, Speciality speciality)
            : this(firstName, lastName, speciality, "LIC-000", "", new WorkSchedule(8, 17)) { }

        public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, int workStartHour, int workEndHour)
            : this(firstName, lastName, speciality, licenseNumber, phone, new WorkSchedule(workStartHour, workEndHour)) { }

        public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            LicenseNumber = licenseNumber;
            Phone = phone;
            Schedule = schedule;
        }

        public string CanAcceptAt(int hour)
        {
            return IsAvailable(hour) ? "Зараз доступний." : "Наразі не доступний.";
        }

        public override string ToString()
        {
            return $"Лікар №: {Id}, Ім'я: {FullName}, Спеціалізація: {ClinicFormatter.FormatSpeciality(Speciality)}, Ліцензія: {LicenseNumber}, Телефон: {ClinicFormatter.FormatPhone(Phone)}, Графік: {Schedule}";
        }

        public bool CanSchedule(DateTime at)
        {
            return Schedule.Contains(at.Hour);
        }

        public DateTime[] GetAvailableSlots(DateTime date, int slotCount)
        {
            ClinicValidator.ValidatePositive(slotCount, nameof(slotCount));
            var slots = new List<DateTime>();
            for (int h = 0; h < 24 && slots.Count < slotCount; h++)
            {
                if (Schedule.Contains(h))
                {
                    slots.Add(date.Date.AddHours(h));
                }
            }
            return slots.ToArray();
        }
    }
}