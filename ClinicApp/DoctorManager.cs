using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp
{
    internal class DoctorManager
    {
        private const int MaxDoctors = 50;
        private Doctor[] _doctors = new Doctor[MaxDoctors];

        private int _count = 0;

        public int Count => _count;

        public void AddDoctor(Doctor doctor)
        {
            if (_count >= MaxDoctors)
            {
                throw new InvalidOperationException("Максимальна кількість лікарів досягнута.");
            }
            _doctors[_count++] = doctor;
        }
        public Doctor? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    return _doctors[i];
                }
            }
            return null;
        }
        public Doctor[] FindBySpecialty(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
            {
                return Array.Empty<Doctor>();
            }
            int matchesCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Specialty.Contains(specialty, StringComparison.OrdinalIgnoreCase))
                {
                    matchesCount++;
                }
            }
            Doctor[] result = new Doctor[matchesCount];
            int resultIndex = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Specialty.Contains(specialty, StringComparison.OrdinalIgnoreCase))
                {
                    result[resultIndex++] = _doctors[i];
                }
            }
            return result;
        }
        public Doctor[] GetAllDoctors()
        {
            Doctor[] result = new Doctor[_count];
            Array.Copy(_doctors, result, _count);
            return result;
        }
        public bool Remove(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    // Зсув елементів масиву вліво
                    for (int j = i; j < _count - 1; j++)
                    {
                        _doctors[j] = _doctors[j + 1];
                    }
                    _doctors[--_count] = null!;
                    return true;
                }
            }
            return false;
        }
        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Лікарі ({_count} / {MaxDoctors}) ===");
            if (_count == 0)
            {
                Console.WriteLine("Список лікарів порожній.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_doctors[i]);
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine("\n=== Статистика лікарів ===");
            Console.WriteLine($"Всього: {_count}");

            if (_count == 0)
            {
                return;
            }

            int availableCount = 0;
            int currentHour = DateTime.Now.Hour;
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].IsAvailable(currentHour))
                {
                    availableCount++;
                }
            }
            Console.WriteLine($"Доступні зараз: {availableCount}");

            Console.WriteLine("По спеціальностях:");
            string[] countedSpecialities = new string[_count];
            int uniqueCount = 0;

            for (int i = 0; i < _count; i++)
            {
                string currentSpec = _doctors[i].Specialty;
                bool alreadyCounted = false;

                for (int j = 0; j < uniqueCount; j++)
                {
                    if (string.Equals(countedSpecialities[j], currentSpec, StringComparison.OrdinalIgnoreCase))
                    {
                        alreadyCounted = true;
                        break;
                    }
                }

                if (!alreadyCounted)
                {
                    int specFrequency = 0;
                    for (int k = 0; k < _count; k++)
                    {
                        if (string.Equals(_doctors[k].Specialty, currentSpec, StringComparison.OrdinalIgnoreCase))
                        {
                            specFrequency++;
                        }
                    }

                    countedSpecialities[uniqueCount++] = currentSpec;
                    Console.WriteLine($"  {currentSpec}: {specFrequency}");
                }
            }

        }
    }
}
