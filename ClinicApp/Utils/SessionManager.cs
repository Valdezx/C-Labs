using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils
{
    public class SessionManager
    {
        private readonly string _sessionPath;

        public SessionManager(string sessionPath = "session.dat")
        {
            _sessionPath = string.IsNullOrWhiteSpace(sessionPath) ? "session.dat" : sessionPath;
        }

        public bool Exists()
        {
            return File.Exists(_sessionPath);
        }

        public void Save(Clinic clinic)
        {
            if (clinic == null)
            {
                return;
            }

            var patients = clinic.Patients.GetAll();

            using (StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8))
            {
                writer.WriteLine("[PATIENTS]");
                foreach (Patient patient in patients)
                {
                    string dobFormatted = patient.DateOfBirth.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    writer.WriteLine($"{patient.FirstName},{patient.LastName},{dobFormatted},{patient.BloodType},{patient.Phone}");
                }
                writer.WriteLine("[END]");
            }
        }

        public int Load(Clinic clinic)
        {
            if (clinic == null || !File.Exists(_sessionPath))
            {
                return 0;
            }

            string[] lines = File.ReadAllLines(_sessionPath);
            string currentSection = string.Empty;
            int addedCount = 0;

            var existingPatients = clinic.Patients.GetAll();

            for (int i = 0; i < lines.Length; i++)
            {
                string trimmed = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(trimmed))
                {
                    continue;
                }

                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    currentSection = trimmed.ToUpperInvariant();
                    continue;
                }

                if (currentSection != "[PATIENTS]")
                {
                    continue;
                }

                string[] parts = trimmed.Split(',');
                if (parts.Length != 5)
                {
                    continue;
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();
                string dobRaw = parts[2].Trim();
                string bloodRaw = parts[3].Trim();
                string phone = parts[4].Trim();

                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(phone))
                {
                    continue;
                }

                if (!TryParseBirthDate(dobRaw, out DateTime dateOfBirth))
                {
                    continue;
                }

                if (!Enum.TryParse<BloodType>(bloodRaw, ignoreCase: true, out BloodType bloodType))
                {
                    continue;
                }

                if (IsDuplicate(existingPatients, clinic.Patients.GetAll(), firstName, lastName, dateOfBirth))
                {
                    continue;
                }

                Patient patient = new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
                clinic.Patients.AddPatient(patient);
                addedCount++;
            }

            return addedCount;
        }

        private static bool IsDuplicate(
            System.Collections.Generic.IEnumerable<Patient> initialPatients,
            System.Collections.Generic.IEnumerable<Patient> currentPatients,
            string firstName,
            string lastName,
            DateTime dob)
        {
            foreach (Patient p in initialPatients)
            {
                if (MatchesPatient(p, firstName, lastName, dob))
                {
                    return true;
                }
            }

            foreach (Patient p in currentPatients)
            {
                if (MatchesPatient(p, firstName, lastName, dob))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool MatchesPatient(Patient p, string firstName, string lastName, DateTime dob)
        {
            return string.Equals(p.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(p.LastName, lastName, StringComparison.OrdinalIgnoreCase) &&
                   p.DateOfBirth.Date == dob.Date;
        }

        private static bool TryParseBirthDate(string rawDate, out DateTime date)
        {
            string[] formats = { "dd.MM.yyyy", "yyyy-MM-dd", "MM/dd/yyyy" };
            return DateTime.TryParseExact(
                rawDate,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
        }
    }
}