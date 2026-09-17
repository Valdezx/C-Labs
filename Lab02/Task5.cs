using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task5
    {
        public static void Run()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
            int n;
            Console.Write("Введіть розмір квадратної матриці N (N >= 2): ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 2)
            {
                Console.Write("Некоректне значення. Введіть ціле число >= 2: ");
            }

            int[,] matrix = new int[n, n];

            Console.WriteLine($"\nВведіть {n} рядків матриці (у кожному рядку рівно {n} чисел через пробіл):");
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.Write($"Рядок {i + 1}: ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        continue;
                    }

                    string[] elements = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (elements.Length != n)
                    {
                        Console.WriteLine($"Помилка: потрібно ввести рівно {n} чисел через пробіл. Спробуйте ще раз.");
                        continue;
                    }

                    bool parseOk = true;
                    for (int j = 0; j < n; j++)
                    {
                        if (!int.TryParse(elements[j], out matrix[i, j]))
                        {
                            parseOk = false;
                            break;
                        }
                    }

                    if (parseOk)
                    {
                        break;
                    }

                    Console.WriteLine("Помилка: серед введених значень є нечислові символи. Спробуйте ще раз.");
                }
            }

            // Обчислення головної та побічної діагоналей
            int[] mainDiagonal = new int[n];
            int[] sideDiagonal = new int[n];
            int mainDiagonalSum = 0;
            int sideDiagonalSum = 0;

            for (int i = 0; i < n; i++)
            {
                // Головна діагональ: i == j
                mainDiagonal[i] = matrix[i, i];
                mainDiagonalSum += matrix[i, i];

                // Побічна діагональ: i + j == n - 1  =>  j = n - 1 - i
                sideDiagonal[i] = matrix[i, n - 1 - i];
                sideDiagonalSum += matrix[i, n - 1 - i];
            }

            // Виведення результатів
            Console.WriteLine("\n=== Результати ===");
            Console.WriteLine("Головна діагональ: " + string.Join(" ", mainDiagonal));
            Console.WriteLine($"Сума головної діагоналі: {mainDiagonalSum}");
            Console.WriteLine("Побічна діагональ: " + string.Join(" ", sideDiagonal));
            Console.WriteLine($"Сума побічної діагоналі: {sideDiagonalSum}");
        }
    }
}
