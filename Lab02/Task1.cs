using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task1
    {
        double CalculateAverageWeight(double[] weights)
        {
            double sum = 0;
            foreach (double weight in weights)
            {
                sum += weight;
            }
            return sum / weights.Length;
        }
        int MoreThanAverageWeightCount(double[] weights)
        {
            double average = CalculateAverageWeight(weights);
            int count = 0;
            foreach (double weight in weights)
            {
                if (weight > average)
                {
                    count++;
                }
            }
            return count;
        }
        double MinimalWeight(double[] weights)
        {
            double min = weights[0];
            foreach (double weight in weights)
            {
                if (weight < min)
                {
                    min = weight;
                }
            }
            return min;
        }
        double MaximalWeight(double[] weights)
        {
            double max = weights[0];
            foreach (double weight in weights)
            {
                if (weight > max)
                {
                    max = weight;
                }
            }
            return max;
        }
        public static void Run()
        {
            int countOfPatients = 0;
            Console.Write("Enter the number of patients: ");
            countOfPatients = int.Parse(Console.ReadLine());
            double[] weightOfPatients = new double[countOfPatients];
            for (int i = 0; i < countOfPatients; i++)
            {
                Console.WriteLine($"Enter the weight of patient {i + 1}: ");
                weightOfPatients[i] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine($"Середня Вага: {new Task1().CalculateAverageWeight(weightOfPatients):F2}");
            Console.WriteLine($"Кількість вище середнього: {new Task1().MoreThanAverageWeightCount(weightOfPatients)} з {countOfPatients}");
            Console.WriteLine($"Мінімальна вага: {new Task1().MinimalWeight(weightOfPatients):F2}");
            Console.WriteLine($"Максимальна вага: {new Task1().MaximalWeight(weightOfPatients):F2}");
        }
    }
}
