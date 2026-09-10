using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01;

internal class Task6
{
    public static void Run()
    {
        Console.WriteLine("Введіть номер медичної картки (5-6 цифр):");
        int cardNumber = int.Parse(Console.ReadLine()!);

        // 1. Визначення відділення за останньою цифрою (cardNumber % 10)
        int lastDigit = cardNumber % 10;
        string department = lastDigit switch
        {
            0 or 1 => "загальна терапія",
            2 or 3 => "хірургія",
            4 or 5 => "кардіологія",
            6 or 7 => "неврологія",
            8 or 9 => "офтальмологія",
            _ => "невідомо"
        };

        // 2. Перевірка на пільгову картку (парність)
        string isDiscount = (cardNumber % 2 == 0) ? "так" : "ні";

        // 3. Перевірка на черговий огляд (кратність 3)
        string isCheckup = (cardNumber % 3 == 0) ? "так" : "ні";

        // Виведення результатів
        Console.WriteLine($"Відділення: {department}");
        Console.WriteLine($"Пільгова картка: {isDiscount}");
        Console.WriteLine($"Черговий огляд: {isCheckup}");
    }
}