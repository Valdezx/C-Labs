using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lab02
{
    internal class Task2
    {
        

        int CalculateMax(int []VisitPrice)
        {
            int max = VisitPrice[0];
            for (int i = 1; i < VisitPrice.Length; i++)
            {
                if (VisitPrice[i] > max)
                {
                    max = VisitPrice[i];
                }
            }
            return max;
        }
        int CalculateMin(int[] VisitPrice)
        {
            int min = VisitPrice[0];
            for (int i = 1; i < VisitPrice.Length; i++)
            {
                if (VisitPrice[i] < min)
                {
                    min = VisitPrice[i];
                }
            }
            return min;
        }


        public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
            int countOfVisits = 0;
            Console.Write("Введіть кількість відвідувань: ");
                    countOfVisits = int.Parse(Console.ReadLine());
            int[] VisitPrice = new int[countOfVisits];
            for (int i = 0; i < countOfVisits; i++)
            {
                Console.WriteLine($"Введіть вартість відвідування {i + 1}: ");
                VisitPrice[i] = int.Parse(Console.ReadLine());
                
            }
            for (int i = 0; i < countOfVisits; i++)
            {
                Array.Sort(VisitPrice);
                
            }
            Console.WriteLine("Відсортовані ціни відвідувань: " + string.Join(", ", VisitPrice));
            Console.WriteLine("Максимальна ціна відвідування: " + new Task2().CalculateMax(VisitPrice));
            Console.WriteLine("Мінімальна ціна відвідування: " + new Task2().CalculateMin(VisitPrice));

        }
    }
}
