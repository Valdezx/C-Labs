using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Models;

namespace ClinicApp.Utils
{
    public class ClinicExporter
    {
        private readonly Clinic _clinic;
        private readonly string _baseDir;

        public ClinicExporter(Clinic clinic, string baseDir = "reports")
        {
            _clinic = clinic;
            _baseDir = string.IsNullOrWhiteSpace(baseDir) ? "reports" : baseDir;
        }

        public string ExportPatients()
        {
            string dir = GetOrCreateDateDirectory();
            string filePath = Path.Combine(dir, "patients.txt");

            var patients = _clinic.Patients.GetAll();

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                WriteHeader(writer, "Пацієнти — звіт");

                int index = 0;
                foreach (Patient patient in patients)
                {
                    writer.WriteLine($"{index + 1,3}. {patient}");
                    index++;
                }

                writer.WriteLine();
                writer.WriteLine($"Всього: {index}");
            }

            return filePath;
        }

        public string ExportAppointments()
        {
            string dir = GetOrCreateDateDirectory();
            string filePath = Path.Combine(dir, "appointments.txt");

            var appointments = _clinic.Appointments.GetAll();

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                WriteHeader(writer, "Записи на прийом — звіт");

                int index = 0;
                foreach (Appointment appointment in appointments)
                {
                    writer.WriteLine($"{index + 1,3}. {appointment}");
                    index++;
                }

                writer.WriteLine();
                writer.WriteLine($"Всього: {index}");
            }

            return filePath;
        }

        public string ExportBilling()
        {
            string dir = GetOrCreateDateDirectory();
            string filePath = Path.Combine(dir, "billing.txt");

            var unpaid = _clinic.Billing.GetAllUnpaid();
            decimal totalDebt = _clinic.Billing.GetTotalDebt();

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                WriteHeader(writer, "Рахунки (неоплачені записи) — звіт");

                int index = 0;
                foreach (var item in unpaid)
                {
                    writer.WriteLine($"{index + 1,3}. {item}");
                    index++;
                }

                writer.WriteLine();
                writer.WriteLine($"Всього: {index}");
                writer.WriteLine($"Загальний борг: {totalDebt.ToString("F2", CultureInfo.InvariantCulture)} грн");
            }

            return filePath;
        }

        public string ExportTreatmentPlans()
        {
            string dir = GetOrCreateDateDirectory();
            string filePath = Path.Combine(dir, "treatment_plans.txt");

            var plans = _clinic.TreatmentPlans.GetAll();

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                WriteHeader(writer, "Плани лікування — звіт");

                int index = 0;
                foreach (TreatmentPlan plan in plans)
                {
                    writer.WriteLine($"{index + 1,3}. {plan}");
                    index++;
                }

                writer.WriteLine();
                writer.WriteLine($"Всього: {index}");
            }

            return filePath;
        }

        public string[] ExportAll()
        {
            string[] paths = new string[4];
            paths[0] = ExportPatients();
            paths[1] = ExportAppointments();
            paths[2] = ExportBilling();
            paths[3] = ExportTreatmentPlans();
            return paths;
        }

        private string GetOrCreateDateDirectory()
        {
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string fullDir = Path.Combine(_baseDir, dateFolder);
            Directory.CreateDirectory(fullDir);
            return fullDir;
        }

        private static void WriteHeader(StreamWriter writer, string title)
        {
            writer.WriteLine($"=== {title} ===");
            writer.WriteLine($"Згенеровано: {DateTime.Now.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)}");
            writer.WriteLine(new string('-', 50));
        }
    }
}