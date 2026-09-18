using System;
using System.Collections.Generic;
using System.Text;


    namespace ClinicApp { 
    public class Patient
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

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
            : this(firstName, lastName, DateTime.Today, "Невідомо", "", "") { }

        // Повний конструктор -> присвоює поля та генерує Id
        public Patient(string firstName, string lastName, DateTime dob, string bloodType, string phone, string email = "")
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
            return $"[ID: {Id}] {FullName}, Вік: {Age} ({GetAgeCategory()}), Група крові: {BloodType}, Тел: {Phone}, Email: {Email}";
        }
    }
}
