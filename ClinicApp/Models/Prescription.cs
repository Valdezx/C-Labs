using System;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class Prescription : MedicalRecord
    {
        private string _medicationName = "";
        private string _dosage = "";
        private int _durationDays;

        public string MedicationName
        {
            get => _medicationName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(MedicationName));
                _medicationName = value;
            }
        }

        public string Dosage
        {
            get => _dosage;
            set
            {
                ClinicValidator.ValidateName(value, nameof(Dosage));
                _dosage = value;
            }
        }

        public int DurationDays
        {
            get => _durationDays;
            set
            {
                ClinicValidator.ValidatePositive(value, nameof(DurationDays));
                _durationDays = value;
            }
        }

        public string Instructions { get; set; }
        public DateTime ExpiresAt => Date.AddDays(DurationDays);

        public Prescription(int id, int patientId, int doctorId, DateTime date, string notes, string medicationName, string dosage, int durationDays, string instructions)
            : base(id, patientId, doctorId, date, notes)
        {
            MedicationName = medicationName;
            Dosage = dosage;
            DurationDays = durationDays;
            Instructions = instructions;
        }

        public override string GetSummary() => $"Рецепт: {MedicationName} {Dosage} на {DurationDays} днів ({Instructions})";
        public override string GetRecordType() => "Рецепт";
    }
}