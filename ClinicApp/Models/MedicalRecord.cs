using System;

namespace ClinicApp.Models
{
    public abstract class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        protected MedicalRecord(int id, int patientId, int doctorId, DateTime date, string notes)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Notes = notes;
        }

        public abstract string GetSummary();
        public abstract string GetRecordType();
    }
}