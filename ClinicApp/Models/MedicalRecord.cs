using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Utils;
namespace ClinicApp.Models
{
    abstract class MedicalRecords
    {
        private static int _nextId = 1;

        public int PatientId { get; }

        public int DoctorId { get; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        protected MedicalRecords(int patientId, int doctorId, DateTime date, string notes)
        {
            ClinicValidator.ValidatePositive(patientId, nameof(patientId));
            ClinicValidator.ValidatePositive(doctorId, nameof(doctorId));
            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Notes = notes;
        }
    }
}
