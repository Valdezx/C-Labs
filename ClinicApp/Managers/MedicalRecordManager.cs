using System;
using ClinicApp.Models;
using ClinicApp.Interfaces;
using ClinicApp.Utils;
using ClinicApp.Managers;

namespace ClinicApp.Managers
{
    public class MedicalRecordManager
    {
        private readonly MedicalRecord[] _records = new MedicalRecord[1000];
        private int _count = 0;

        public int Count => _count;
        

        public void Add(MedicalRecord record)
        {
            if (record == null) return;

            if (_count >= _records.Length)
            {
                Console.WriteLine("Досягнуто ліміт записів (1000).");
                return;
            }

            _records[_count++] = record;
            Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
        }

        public MedicalRecord? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_records[i]?.Id == id)
                {
                    return _records[i];
                }
            }
            return null;
        }

        public MedicalRecord[] GetByPatient(int patientId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i]?.PatientId == patientId) matchCount++;
            }

            MedicalRecord[] result = new MedicalRecord[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i]?.PatientId == patientId)
                {
                    result[index++] = _records[i];
                }
            }
            return result;
        }

        public MedicalRecord[] GetByDoctor(int doctorId)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i]?.DoctorId == doctorId) matchCount++;
            }

            MedicalRecord[] result = new MedicalRecord[matchCount];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i]?.DoctorId == doctorId)
                {
                    result[index++] = _records[i];
                }
            }
            return result;
        }

        public void DisplayAll()
        {
            if (_count == 0)
            {
                Console.WriteLine("Усі записи; їх немає — повідомлення");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_records[i]);
            }
        }

        public void DisplayList(MedicalRecord[] records)
        {
            if (records == null || records.Length == 0)
            {
                Console.WriteLine("Виводить масив; якщо порожній — повідомлення");
                return;
            }

            foreach (var record in records)
            {
                Console.WriteLine(record);
            }
        }
        public MedicalRecord? this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) return null;
                return _records[index];
            }
        }
    }
}