using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task8
    {
        public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
            Console.Write("Введіть кількість відділень (D): ");
            int d = ReadInt();

            Console.Write("Введіть кількість тижнів (W): ");
            int w = ReadInt();

            const int SHIFTS = 2; // 0 - ранок, 1 - вечір
            int[,,] stats = new int[d, w, SHIFTS];

            Console.WriteLine($"\nВведіть дані про пацієнтів (всього {d * w * SHIFTS} значень):");

            for (int dept = 0; dept < d; dept++)
            {
                Console.WriteLine($"\n--- Відділення #{dept + 1} ---");
                for (int week = 0; week < w; week++)
                {
                    Console.WriteLine($"Тиждень #{week + 1}:");

                    Console.Write("  Ранкова зміна: ");
                    stats[dept, week, 0] = ReadInt();

                    Console.Write("  Вечірня зміна: ");
                    stats[dept, week, 1] = ReadInt();
                }
            }

            int maxPatients = -1;
            int busiestDepartment = 0;

            Console.WriteLine("\n=== Результати аналітики ===");
            for (int dept = 0; dept < d; dept++)
            {
                int morningSum = 0;
                int eveningSum = 0;

                for (int week = 0; week < w; week++)
                {
                    morningSum += stats[dept, week, 0];
                    eveningSum += stats[dept, week, 1];
                }

                int departmentTotal = morningSum + eveningSum;
                double avgPerWeek = w > 0 ? (double)departmentTotal / w : 0.0;

                Console.WriteLine($"Відділення #{dept + 1}: всього = {departmentTotal}, ранок = {morningSum}, вечір = {eveningSum}, сер./тижд. = {avgPerWeek:F2}");

                if (departmentTotal > maxPatients)
                {
                    maxPatients = departmentTotal;
                    busiestDepartment = dept + 1;
                }
            }

            Console.WriteLine($"\nНайзавантаженіше відділення: #{busiestDepartment} ({maxPatients} пацієнтів)");
        }

        private static int ReadInt()
        {
            string? line = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(line))
            {
                line = Console.ReadLine();
            }
            return int.Parse(line.Trim());
        }

    }
}