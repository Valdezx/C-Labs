using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    internal class Task4
    {
        public static void Run()
        {
            int DiastolicPressure = 0;
            int SystolicPressure = 0;
            string[] categories = { "Норма", "Підвищений", "Гіпертонія 1 ступеня", "Гіпертонія 2 ступеня" };
            Console.WriteLine("Введіть діастолічний тиск: ");
            DiastolicPressure = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть систолічний тиск:  ");
            SystolicPressure = int.Parse(Console.ReadLine());
            if (SystolicPressure <120 &&  DiastolicPressure<= 80)
            {
                Console.WriteLine("Ви належите до категорії: " + categories[0]);
            }
            else if (SystolicPressure >= 120 && SystolicPressure < 130 && DiastolicPressure >= 80 && DiastolicPressure < 85)
            {
                Console.WriteLine("Ви належите до категорії: " + categories[1]);
            }
            else if (SystolicPressure >= 130 && SystolicPressure < 140 && DiastolicPressure >= 85 && DiastolicPressure < 90)
            {
                Console.WriteLine("Ви належите до категорії: " + categories[2]);
            }
            else
            {
                Console.WriteLine("Ви належите до категорії: " + categories[3]);
            }
        }
    }
}
