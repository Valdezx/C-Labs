using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp
{
    internal class PatientManager
    {
        private const int MaxPatients = 100;

        private Patient[] _patients = new Patient[MaxPatients];

        private int  _count = 0;

        public int Count => _count;

        public void AddPatient(Patient patient)
        {
            if (_count >= MaxPatients)
            {
                throw new InvalidOperationException("Максимальна кількість пацієнтів досягнута.");
            }
            _patients[_count++] = patient;

        }
        public Patient? FindById (int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    return _patients[i];
                }
            }
            return null;
        }

        public Patient[] FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Array.Empty<Patient>();
            }

            int matchesCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    matchesCount++;
                }
            }

            Patient[] result = new Patient[matchesCount];
            int resultIndex = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    result[resultIndex++] = _patients[i];
                }
            }

            return result;
        }
        public bool Remove(int id)
        {
            for (int i=0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    for (int j = i; j < _count - 1; j++)
                    {
                        _patients[j] = _patients[j + 1];
                    }
                    _patients[--_count] = null!;
                    return true;
                }
            }
            return false;
        }
        public void DisplayAllPatients()
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_patients[i]);
            }
        }
        public void DisplayStats()
        {
            Console.WriteLine("\n=== Статистика пацієнтів ===");
            if (_count == 0)
            {
                Console.WriteLine("Немає даних для розрахунку статистики.");
                return;
            }

            int totalAge = 0;
            int adultsCount = 0;
            int minAge = _patients[0].Age;
            int maxAge = _patients[0].Age;

            for (int i = 0; i < _count; i++)
            {
                int currentAge = _patients[i].Age;
                totalAge += currentAge;

                if (_patients[i].IsAdult)
                {
                    adultsCount++;
                }

                if (currentAge < minAge) minAge = currentAge;
                if (currentAge > maxAge) maxAge = currentAge;
            }

            double averageAge = (double)totalAge / _count;

            Console.WriteLine($"Загальна кількість: {_count}");
            Console.WriteLine($"Середній вік: {averageAge:F1} р.");
            Console.WriteLine($"Наймолодший: {minAge} р.");
            Console.WriteLine($"Найстарший: {maxAge} р.");
            Console.WriteLine($"Кількість дорослих (>= 18): {adultsCount}");
        }
        public Patient this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException($"Індекс {index} виходить за межі діапазону (0..{_count - 1}).");

                return _patients[index];
            }
        }
    }
}
