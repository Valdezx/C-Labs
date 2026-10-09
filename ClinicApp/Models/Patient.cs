using ClinicApp.Enums;
using ClinicApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Utils;


namespace ClinicApp.Models { 
    public class Patient
    {
        private static int _nextId = 1;

        public int Id { get; }
        private string _firstName = "";
        private string _lastName = "";
        private DateTime _dateOfBirth = DateTime.MinValue;
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        private string _phone = "";
        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FirstName
        {
            get => _firstName;

            set
            {
                ClinicValidator.ValidateName(value, nameof(FirstName));
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(LastName));
                _lastName = value;
            }
        }
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                ClinicValidator.ValidateDateOfBirth(value);
                _dateOfBirth = value;
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
            ClinicValidator.ValidatePhone(value);
                _phone = value;

            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public bool IsAdult => Age >= 18;

        // Конструктор за замовчуванням -> викликає скорочений
        public Patient() : this("Невідомо", "Невідомо") { }

        // Скорочений конструктор -> викликає повний
        public Patient(string firstName, string lastName)
            : this(firstName, lastName, DateTime.Today, BloodType.Unknown, "", "") { }

        // Повний конструктор -> присвоює поля та генерує Id
        public Patient(string firstName, string lastName, DateTime dob, BloodType bloodType, string phone, string email = "")
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            BloodType = bloodType;
            Phone = phone;
            Email = email;
        }

        public string GetAgeCategory()
        {
            if (Age < 18) return "дитина";
            if (Age < 65) return "дорослий";
            return "літній";
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {FullName}, Вік: {ClinicFormatter.FormatAge(Age)} ({GetAgeCategory()}), Група крові: {ClinicFormatter.FormatBloodType(BloodType)}, Тел: {ClinicFormatter.FormatPhone(Phone)}, Email: {Email}";
        }
    }
}
