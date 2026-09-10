using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01;

internal class Task8
{
    // 1. Метод розрахунку ІМТ
    public static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    // 2. Метод визначення категорії ІМТ
    public static string GetBMICategory(double bmi)
    {
        if (bmi < 18.5) return "недостатня вага";
        if (bmi < 25.0) return "норма";
        if (bmi < 30.0) return "надмірна вага";
        return "ожиріння";
    }

    // 3. Метод розрахунку вартості зі знижкою
    public static double CalculateCost(double price, int count, int discountPercent)
    {
        double total = price * count;
        return total - (total * discountPercent / 100.0);
    }

    // 4. Метод визначення вікової категорії
    public static string GetAgeCategory(int age)
    {
        if (age < 18) return "дитина";
        if (age <= 60) return "дорослий";
        return "пенсіонер";
    }

    // 5. Метод визначення статусу артеріального тиску
    public static string GetPressureStatus(int systolic, int diastolic)
    {
        if (systolic < 120 && diastolic < 80) return "нормальний";
        if (systolic <= 139 || diastolic <= 89) return "передгіпертонія";
        return "гіпертонія";
    }

    public static void Run()
    {
        // Вхідні дані для Задачі 1
        double weight = double.Parse(Console.ReadLine()!);
        double height = double.Parse(Console.ReadLine()!);
        double bmi = CalculateBMI(weight, height);
        string bmiCategory = GetBMICategory(bmi);

        Console.WriteLine($"ІМТ: {bmi:F2}");
        Console.WriteLine($"Категорія: {bmiCategory}");

        // Вхідні дані для Задачі 2
        double price = double.Parse(Console.ReadLine()!);
        int count = int.Parse(Console.ReadLine()!);
        int discount = int.Parse(Console.ReadLine()!);
        double cost = CalculateCost(price, count, discount);

        Console.WriteLine($"Вартість: {cost:F2}");

        // Вхідні дані для Задачі 3
        int age = int.Parse(Console.ReadLine()!);
        Console.WriteLine($"Вікова категорія: {GetAgeCategory(age)}");

        // Вхідні дані для Задачі 4
        int systolic = int.Parse(Console.ReadLine()!);
        int diastolic = int.Parse(Console.ReadLine()!);
        Console.WriteLine($"Статус тиску: {GetPressureStatus(systolic, diastolic)}");
    }
}