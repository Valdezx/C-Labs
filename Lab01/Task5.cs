using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01
{
    internal class Task5
    {
        public static void Run()
        {
            string[] days = { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
            Console.WriteLine("======== Графік Роботи ========");
            Console.WriteLine("Введіть номер дня тижня (1-7): ");
            int dayNumber = int.Parse(Console.ReadLine());
            switch (dayNumber)
            {
                case 1:
                    
                        Console.Write("Ви вибрали: " + days[0]);
                        Console.WriteLine("Графік роботи: 08:00 - 18:00");
                        break;
                    
                case 2:
                   
                    
                        Console.Write("Ви вибрали: " + days[1]);
                        Console.WriteLine("Графік роботи: 08:00 - 18:00");
                        break;
                    
                case 3:
                    
                    
                        Console.Write("Ви вибрали: " + days[2]);
                        Console.WriteLine("Графік роботи: 09:00 - 17:00");
                        break;
                    
                case 4:
                    
                    
                        Console.Write("Ви вибрали: " + days[3]);
                        Console.WriteLine("Графік роботи: 08:00 - 18:00");
                        break;
                     case 5:
                   
                        Console.Write("Ви вибрали: " + days[4]);
                        Console.WriteLine("Графік роботи: 08:00 - 16:00");
                        break;
                     case 6:
                   
                        Console.Write("Ви вибрали: " + days[5]);
                        Console.WriteLine("Графік роботи: 08:00 - 14:00");
                        break;
                    case 7: 
                    
                        Console.Write("Ви вибрали: " + days[6]);
                        Console.WriteLine("Вихідний"); break;
                    
                default: Console.WriteLine("Невірний номер дня тижня."); return;

            }
        }
    }
}
