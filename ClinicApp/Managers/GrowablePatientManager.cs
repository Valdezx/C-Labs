using ClinicApp.Enums;
using ClinicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Managers
{
    public class GrowablePatientManager
    {
        private Patient[] _patients = new Patient[20];
        private int _count;

        public int Count => _count;

        public int Capacity => _patients.Length;

        public void AddPatient(Patient patient)
        {
            if (_count >= Capacity)
            {

                Patient[] newPatients = new Patient[Capacity * 2];
                Array.Copy(_patients, newPatients, _count);
                _patients = newPatients;    
            }
            _patients[_count++] = patient;
        }
        public Patient? FindById(int id)
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
        public bool Remove(int id)
        {
            for (int i = 0; i < _count; i++)
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
        public void DisplayAll()
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine($"Пацієнт {i + 1}: {_patients[i].FullName}, Вік: {_patients[i].Age}, Телефон: {_patients[i].Phone}");
            }
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
        public bool TryFindById(int id, out Patient? patient)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    patient = _patients[i];
                    return true;
                }
            }

            patient = null;
            return false;
        }
        public Patient[] FindByBloodType(BloodType bloodType)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].BloodType == bloodType)
                {
                    matchCount++;
                }
            }

            Patient[] result = new Patient[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].BloodType == bloodType)
                {
                    result[index++] = _patients[i];
                }
            }

            return result;
        }
    }
}
