using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    internal class Task1
    {

        public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
    System.Globalization.CultureInfo.InvariantCulture;
            double weight;
            double height;
            double IMT;
            Console.Write("Введіть вашу вагу (кг): ");
            weight = double.Parse(Console.ReadLine());
            Console.Write("Введіть ваш зріст (м): ");
            height = double.Parse(Console.ReadLine());
            IMT = weight / (height * height);
            Console.WriteLine($"Ваш індекс маси тіла: {IMT:F2}");
        }
    }
}
