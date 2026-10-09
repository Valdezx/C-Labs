using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Utils
{
    public static class ClinicValidator
    {
        public static void ValidateName(string name, string fieldName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || name.Length > 50 || name.Length < 2)
                {
                    throw new ArgumentException($"{fieldName} не може бути порожнім і не повинно перевищувати 50 символів.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації {fieldName}: {ex.Message}");
                throw;
            }
        }
        public static void ValidatePhone(string phone)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phone) || phone.Length > 10)
                {
                    throw new ArgumentException("Телефон не може бути порожнім і не повинно перевищувати 10 символів.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації телефону: {ex.Message}");
                throw;
            }
        }
        public static void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            try
            {
                if (dateOfBirth > DateTime.Today || dateOfBirth.Year < 1900)
                {
                    throw new ArgumentException("Дата народження не може бути в майбутньому або більш старою, ніж 1900 рік.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації дати народження: {ex.Message}");
                throw;
            }
        }
        public static void ValidatePositive(int value, string fieldName)
        {
            try
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{fieldName} повинно бути додатнім числом.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка валідації {fieldName}: {ex.Message}");
                throw;
            }
        }
    }
}
