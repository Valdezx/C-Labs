using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClinicApp;

public static class ClinicFormatter
{
    // Форматування групи крові у звичний медичний вигляд
    public static string FormatBloodType(BloodType bt) => bt switch
    {
        BloodType.OPositive => "O(I)+",
        BloodType.ONegative => "O(I)-",
        BloodType.APositive => "A(II)+",
        BloodType.ANegative => "A(II)-",
        BloodType.BPositive => "B(III)+",
        BloodType.BNegative => "B(III)-",
        BloodType.ABPositive => "AB(IV)+",
        BloodType.ABNegative => "AB(IV)-",
        _ => "Невідомо"
    };

    // Форматування спеціальності лікаря українською
    public static string FormatSpeciality(Speciality s) => s switch
    {
        Speciality.General => "Загальна практика",
        Speciality.Therapist => "Терапевт",
        Speciality.Cardiologist => "Кардіолог",
        Speciality.Neurologist => "Невролог",
        Speciality.Pediatrician => "Педіатр",
        Speciality.Dermatologist => "Дерматолог",
        Speciality.Surgeon => "Хірург",
        Speciality.Ophthalmologist => "Офтальмолог",
        _ => s.ToString()
    };

    // Правила відмінювання віку: рік / роки / років
    public static string FormatAge(int age)
    {
        int lastTwoDigits = age % 100;
        int lastDigit = age % 10;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 14)
            return $"{age} років";

        return lastDigit switch
        {
            1 => $"{age} рік",
            2 or 3 or 4 => $"{age} роки",
            _ => $"{age} років"
        };
    }

    // Форматування номера телефону (наприклад: 0501234567 -> (050) 123-4567)
    public static string FormatPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "Не вказано";

        string digits = Regex.Replace(phone, @"\D", "");

        // Якщо передано 10 цифр (наприклад, 0501234567)
        if (digits.Length == 10)
        {
            return $"({digits[..3]}) {digits.Substring(3, 3)}-{digits.Substring(6, 4)}";
        }
        // Якщо передано з кодом країни 12 цифр (наприклад, 380501234567)
        if (digits.Length == 12 && digits.StartsWith("38"))
        {
            return $"+38 ({digits.Substring(2, 3)}) {digits.Substring(5, 3)}-{digits.Substring(8, 4)}";
        }

        return phone;
    }
}