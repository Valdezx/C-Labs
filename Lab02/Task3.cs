using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task3
    {
        int calculateTotalPatients(int[] countOfPatients)
        {
            int total = 0;
            foreach (int count in countOfPatients)
            {
                total += count;
            }
            return total;
        }
        int calculateMinPatients(int[] countOfPatients)
        {
            int min = countOfPatients[0];
            foreach (int count in countOfPatients)
            {
                if (count < min)
                {
                    min = count;
                }
            }
            return min;
        }
        int calculateMaxPatients(int[] countOfPatients)
        {
            int max = countOfPatients[0];
            foreach (int count in countOfPatients)
            {
                if (count > max)
                {
                    max = count;
                }
            }
            return max;
        }

        public  static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
            string[] days = { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
            int[] countOfPatients = new int[7];
            for(int i = 0; i < 7; i++)
            {
                Console.Write($"Введіть кількість пацієнтів у {days[i]}: ");
                countOfPatients[i] = int.Parse(Console.ReadLine());
            }
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"У {days[i]} було {countOfPatients[i]} пацієнтів.");
            }
            Console.WriteLine($"Загальна кількість пацієнтів за тиждень: {new Task3().calculateTotalPatients(countOfPatients)}");
            Console.WriteLine($"Мінімальна кількість пацієнтів була у {days[Array.IndexOf(countOfPatients, new Task3().calculateMinPatients(countOfPatients))]}: {new Task3().calculateMinPatients(countOfPatients)}");
            Console.WriteLine($"Максимальна кількість пацієнтів була у {days[Array.IndexOf(countOfPatients, new Task3().calculateMaxPatients(countOfPatients))]}: {new Task3().calculateMaxPatients(countOfPatients)}");

        }
    }
}
