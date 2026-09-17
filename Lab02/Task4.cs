using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task4
    {
        public static void Run()

        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
            Console.Write("Введіть кількість лікарів (N): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Помилка: введіть коректне додатне число N.");
                return;
            }

            Console.Write("Введіть кількість днів (M): ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m <= 0)
            {
                Console.WriteLine("Помилка: введіть коректне додатне число M.");
                return;
            }

            int[,] matrix = new int[n, m];

            Console.WriteLine($"\nВведіть {n} рядків матриці (кожен рядок містить {m} чисел через пробіл):");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Лікар #{i + 1}: ");
                string[] rowElements = Console.ReadLine()!
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                while (rowElements.Length < m)
                {
                    Console.WriteLine($"Потрібно ввести рівно {m} чисел. Спробуйте ще раз для лікаря #{i + 1}:");
                    rowElements = Console.ReadLine()!
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }

                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = int.Parse(rowElements[j]);
                }
            }

            // 1. Сума прийомів по лікарях (рядки)
            int[] doctorSums = CalculateDoctorSums(matrix, n, m);

            // 2. Сума прийомів по днях (стовпці)
            int[] daySums = CalculateDaySums(matrix, n, m);

            // 3. Пошук максимуму та його координат
            FindMaxReception(matrix, n, m, out int maxValue, out int maxDoctor, out int maxDay);

            // Виведення результатів
            Console.WriteLine("\n=== Результати аналізу ===");
            Console.WriteLine("Сума по кожному лікарю:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"  • Лікар #{i + 1}: {doctorSums[i]} прийомів");
            }

            Console.WriteLine("\nСума по кожному дню:");
            for (int j = 0; j < m; j++)
            {
                Console.WriteLine($"  • День #{j + 1}: {daySums[j]} прийомів");
            }

            Console.WriteLine($"\nМаксимальна кількість прийомів: {maxValue}");
            Console.WriteLine($"Позиція: Лікар #{maxDoctor}, День #{maxDay}");
        }

        public static int[] CalculateDoctorSums(int[,] matrix, int n, int m)
        {
            int[] sums = new int[n];
            for (int i = 0; i < n; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < m; j++)
                {
                    rowSum += matrix[i, j];
                }
                sums[i] = rowSum;
            }
            return sums;
        }

        public static int[] CalculateDaySums(int[,] matrix, int n, int m)
        {
            int[] sums = new int[m];
            for (int j = 0; j < m; j++)
            {
                int colSum = 0;
                for (int i = 0; i < n; i++)
                {
                    colSum += matrix[i, j];
                }
                sums[j] = colSum;
            }
            return sums;
        }

        public static void FindMaxReception(int[,] matrix, int n, int m, out int maxVal, out int doctorIndex, out int dayIndex)
        {
            maxVal = matrix[0, 0];
            doctorIndex = 1;
            dayIndex = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] > maxVal)
                    {
                        maxVal = matrix[i, j];
                        doctorIndex = i + 1;
                        dayIndex = j + 1;
                    }
                }
            }
        }
    }
}
 
