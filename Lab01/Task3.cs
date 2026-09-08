using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    internal class Task3
    {
       public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            int birthYear;
            List<string> categories = new List<string> { "Дитина", "Дорослий", "Пенсіонер" };
            string category;
            int age;
            int currentYear = 2026;
            Console.Write("Введіть рік вашого народження: ");
            birthYear = int.Parse(Console.ReadLine());
            age = currentYear - birthYear;
            if (age >= 0 && age < 17)
            {
                Console.Write("Ви належите до категорії: " + categories[0]);
            }
            else if (age >= 18 && age < 59)
            {
                Console.Write("Ви належите до категорії: " + categories[1]);
            }
            else if(age >= 60)
            {
                Console.Write ("Ви належите до категорії: " + categories[2]);
            }
        }

    }

}
