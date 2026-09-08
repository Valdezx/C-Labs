using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    internal class Task2
    {

        public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            double price;
            int quantity;
            int sale;
            double totalPrice;
            Console.Write("Введіть ціну візиту (грн): ");
            price = double.Parse(Console.ReadLine());
            Console.Write("Введіть кількість візитів: ");
            quantity = int.Parse(Console.ReadLine());
            Console.Write("Введіть знижку (у відсотках): ");
            sale = int.Parse(Console.ReadLine());
            totalPrice = price * quantity * (1 - sale / 100.0);
            Console.WriteLine($"Загальна вартість знижки: {totalPrice:F2} грн");
        }
    }
}