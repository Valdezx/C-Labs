using System;
using ClinicApp.Utils;
using ClinicApp.Managers;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Models
{
    public class Diagnosis : MedicalRecord
    {
        private string _diagnosisCode = "";
        private string _description = "";

        public string DiagnosisCode
        {
            get => _diagnosisCode;
            set
            {
                ClinicValidator.ValidateName(value, nameof(DiagnosisCode));
                _diagnosisCode = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                ClinicValidator.ValidateName(value, nameof(Description));
                _description = value;
            }
        }

        public Diagnosis(int id, int patientId, int doctorId, DateTime date, string notes, string diagnosisCode, string description)
            : base(id, patientId, doctorId, date, notes)
        {
            DiagnosisCode = diagnosisCode;
            Description = description;
        }

        public override string GetSummary() => $"МКХ: {DiagnosisCode} | Хвороба: {Description}";
        public override string GetRecordType() => "Діагноз";
    }
}