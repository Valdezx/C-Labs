using System;
using ClinicApp.Utils;

namespace ClinicApp.Models
{
    public class LabResult : MedicalRecord
    {
        private string _testName = "";
        private string _unit = "";
        private string _referenceRange = "";

        public string TestName
        {
            get => _testName;
            set
            {
                ClinicValidator.ValidateName(value, nameof(TestName));
                _testName = value;
            }
        }

        public double Value { get; set; }

        public string Unit
        {
            get => _unit;
            set
            {
                ClinicValidator.ValidateName(value, nameof(Unit));
                _unit = value;
            }
        }

        public string ReferenceRange
        {
            get => _referenceRange;
            set
            {
                ClinicValidator.ValidateName(value, nameof(ReferenceRange));
                _referenceRange = value;
            }
        }

        public bool IsNormal { get; set; }

        public LabResult(int id, int patientId, int doctorId, DateTime date, string notes, string testName, double value, string unit, string referenceRange, bool isNormal)
            : base(id, patientId, doctorId, date, notes)
        {
            TestName = testName;
            Value = value;
            Unit = unit;
            ReferenceRange = referenceRange;
            IsNormal = isNormal;
        }

        public override string GetSummary()
        {
            string status = IsNormal ? "В межах норми" : "Відхилення від норми";
            return $"Аналіз: {TestName} - {Value} {Unit} (Норма: {ReferenceRange}) [{status}]";
        }

        public override string GetRecordType() => "Аналіз";
    }
}