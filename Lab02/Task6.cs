using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task6
    {
        public static void Run()
        {

            int n = int.Parse(Console.ReadLine()!);

            // Ініціалізація рваного (зубчастого) масиву
            int[][] doctors = new int[n][];

            for (int i = 0; i < n; i++)
            {
                // Кількість прийомів K для поточного лікаря
                int k = int.Parse(Console.ReadLine()!);
                doctors[i] = new int[k];

                // Зчитування K значень вартостей прийомів (по одному на рядок)
                for (int j = 0; j < k; j++)
                {
                    doctors[i][j] = int.Parse(Console.ReadLine()!);
                }
            }

            int maxIncome = -1;
            int bestDoctorIndex = 0;

            Console.WriteLine("\n=== Статистика по лікарях ===");
            for (int i = 0; i < n; i++)
            {
                int count = doctors[i].Length;
                int totalIncome = 0;

                for (int j = 0; j < count; j++)
                {
                    totalIncome += doctors[i][j];
                }

                double averageIncome = count > 0 ? (double)totalIncome / count : 0.0;

                Console.WriteLine($"Лікар #{i + 1}: прийомів = {count}, сума = {totalIncome} грн, середній чек = {averageIncome:F2} грн");

                if (totalIncome > maxIncome)
                {
                    maxIncome = totalIncome;
                    bestDoctorIndex = i + 1;
                }
            }

            Console.WriteLine($"\nЛікар з найбільшим доходом: #{bestDoctorIndex} (дохід: {maxIncome} грн)");
        }
    }
}
