using ClinicApp.Enums;
using ClinicApp.Managers;
using System;
using System.Globalization;
using System.Threading;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Clinic clinic = new Clinic("MyClinicApp");

        clinic.Patients.AddPatient(new Patient("Іван", "Петренко", new DateTime(1995, 3, 14), BloodType.APositive, "+380501112233"));
        clinic.Patients.AddPatient(new Patient("Ольга", "Коваль", new DateTime(2012, 7, 21), BloodType.BNegative, "+380672223344"));
        clinic.Patients.AddPatient(new Patient("Петро", "Іваненко", new DateTime(1960, 11, 5), BloodType.OPositive, "+380933334455"));

        clinic.Doctors.AddDoctor(new Doctor("Олег", "Сидоренко", Speciality.Cardiologist, "LIC-001", "0441234567", 8, 16));
        clinic.Doctors.AddDoctor(new Doctor("Наталія", "Мороз", Speciality.Neurologist, "LIC-002", "0442345678", 9, 18));
        clinic.Doctors.AddDoctor(new Doctor("Андрій", "Власенко", Speciality.Pediatrician, "LIC-003", "0443456789", 8, 17));

        DateTime today = DateTime.Today;
        clinic.Appointments.Book(1, 1, today.AddHours(10), 30);
        clinic.Appointments.Book(2, 2, today.AddHours(11), 45);

        
        clinic.MedicalRecords.Add(new Diagnosis(1, 1, today.AddDays(-30), "I10", "Гіпертонічна хвороба", true));
        clinic.MedicalRecords.Add(new Diagnosis(1, 2, today.AddDays(-5), "J02", "Гострий фарингіт", false));
        clinic.MedicalRecords.Add(new LabResult(1, 1, today, "Гемоглобін", 145, "г/л", "120-160", true));
        clinic.MedicalRecords.Add(new LabResult(1, 1, today, "Холестерин", 6.2, "ммоль/л", "< 5.2", false));
        clinic.MedicalRecords.Add(new Prescription(1, 1, today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));
        clinic.MedicalRecords.Add(new Prescription(2, 2, today.AddDays(-40), "Амоксицилін", "500 мг", 10, "2 рази на день"));

        bool exitApp = false;
        while (!exitApp)
        {
            Console.WriteLine($"\n===== {clinic.Name.ToUpper()} =====");
            Console.WriteLine("1. Управління пацієнтами");
            Console.WriteLine("2. Управління лікарями");
            Console.WriteLine("3. Управління записами");
            Console.WriteLine("4. Медична картка з пунктами");
            Console.WriteLine("5. Розклад на дату");
            Console.WriteLine("6. Генерація повного звіту клініки");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть пункт: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    HandlePatientsMenu(clinic.Patients);
                    break;
                case "2":
                    HandleDoctorsMenu(clinic.Doctors);
                    break;
                case "3":
                    HandleAppointmentsMenu(clinic.Appointments);
                    break;
                case "4":
                    HandleMedicalRecordsMenu(clinic.MedicalRecords);
                    break;
                case "5":
                    Console.Write("Введіть дату (рррр-мм-дд) [Enter для поточної]: ");
                    string? dateInput = Console.ReadLine();
                    DateTime targetDate = string.IsNullOrWhiteSpace(dateInput)
                        ? DateTime.Today
                        : (DateTime.TryParse(dateInput, CultureInfo.InvariantCulture, out DateTime d) ? d : DateTime.Today);

                    clinic.DisplaySchedule(targetDate);
                    break;
                case "6":
                    clinic.GenerateReport();
                    break;
                case "0":
                    exitApp = true;
                    Console.WriteLine("Роботу завершено.");
                    break;
                default:
                    Console.WriteLine("Невідомий пункт меню.");
                    break;
            }
        }
    }

    private static void HandleMedicalRecordsMenu(MedicalRecordManager manager)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n--- Підменю «Медична картка» ---");
            Console.WriteLine("1. Картка пацієнта");
            Console.WriteLine("2. Усі записи пацієнта");
            Console.WriteLine("3. Додати діагноз");
            Console.WriteLine("4. Додати аналіз");
            Console.WriteLine("5. Додати рецепт");
            Console.WriteLine("6. Записи лікаря");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть пункт: ");

            string? choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ID пацієнта: ");
                        if (int.TryParse(Console.ReadLine(), out int pIdSum))
                        {
                            manager.DisplayPatientSummary(pIdSum);
                        }
                        else
                        {
                            Console.WriteLine("Повідомлення про некоректне число.");
                        }
                        break;

                    case "2":
                        Console.Write("Введіть ID пацієнта: ");
                        if (int.TryParse(Console.ReadLine(), out int pIdAll))
                        {
                            var recs = manager.GetByPatient(pIdAll);
                            manager.DisplayList(recs);
                        }
                        else
                        {
                            Console.WriteLine("Повідомлення про некоректне число.");
                        }
                        break;

                    case "3":
                        Console.Write("ID пацієнта (ціле): ");
                        int diagPId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("ID лікаря (ціле): ");
                        int diagDId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Код діагнозу (напр. I10): ");
                        string code = Console.ReadLine() ?? "";
                        Console.Write("Опис: ");
                        string desc = Console.ReadLine() ?? "";
                        Console.Write("Хронічне? (1-так, 0-ні): ");
                        int isChronicInt = int.Parse(Console.ReadLine() ?? "0");

                        manager.Add(new Diagnosis(diagPId, diagDId, DateTime.Today, code, desc, isChronicInt == 1));
                        break;

                    case "4":
                        Console.Write("ID пацієнта (ціле): ");
                        int labPId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("ID лікаря (ціле): ");
                        int labDId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Назва аналізу: ");
                        string testName = Console.ReadLine() ?? "";
                        Console.Write("Значення (число з крапкою): ");
                        double val = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);
                        Console.Write("Одиниця виміру: ");
                        string unit = Console.ReadLine() ?? "";
                        Console.Write("Норма (текст, напр. 120-160): ");
                        string norm = Console.ReadLine() ?? "";
                        Console.Write("В нормі? (1-так, 0-ні): ");
                        int isNormInt = int.Parse(Console.ReadLine() ?? "0");

                        manager.Add(new LabResult(labPId, labDId, DateTime.Today, testName, val, unit, norm, isNormInt == 1));
                        break;

                    case "5":
                        Console.Write("ID пацієнта (ціле): ");
                        int presPId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("ID лікаря (ціле): ");
                        int presDId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Препарат: ");
                        string med = Console.ReadLine() ?? "";
                        Console.Write("Дозування (напр. 10 мг): ");
                        string dosage = Console.ReadLine() ?? "";
                        Console.Write("Кількість днів (ціле): ");
                        int days = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Інструкція (Enter — пропустити): ");
                        string inst = Console.ReadLine() ?? "";

                        manager.Add(new Prescription(presPId, presDId, DateTime.Today, med, dosage, days, inst));
                        break;

                    case "6":
                        Console.Write("Введіть ID лікаря: ");
                        if (int.TryParse(Console.ReadLine(), out int dIdDoc))
                        {
                            var docRecs = manager.GetByDoctor(dIdDoc);
                            manager.DisplayList(docRecs);
                        }
                        else
                        {
                            Console.WriteLine("Повідомлення про некоректне число.");
                        }
                        break;

                    case "0":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }

    private static void HandlePatientsMenu(PatientManager manager)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n--- Підменю «Пацієнти» ---");
            Console.WriteLine("1. Показати всіх");
            Console.WriteLine("2. Додати пацієнта");
            Console.WriteLine("3. Знайти за ім'ям / прізвищем");
            Console.WriteLine("4. Видалити за ID");
            Console.WriteLine("5. Статистика");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть пункт: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayAllPatients();
                    break;
                case "2":
                    Console.Write("Ім'я: ");
                    string fn = Console.ReadLine() ?? "";
                    Console.Write("Прізвище: ");
                    string ln = Console.ReadLine() ?? "";
                    Console.Write("Дата народження (рррр-мм-дд): ");
                    DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime dob);

                    Console.WriteLine("Оберіть групу крові (0-8): ");
                    int.TryParse(Console.ReadLine(), out int btNumber);
                    BloodType bloodType = (BloodType)btNumber;

                    Console.Write("Телефон: ");
                    string ph = Console.ReadLine() ?? "";
                    manager.AddPatient(new Patient(fn, ln, dob, bloodType, ph));
                    break;
                case "3":
                    Console.Write("Пошуковий запит: ");
                    var found = manager.FindByName(Console.ReadLine() ?? "");
                    if (found.Length == 0) Console.WriteLine("Нікого не знайдено.");
                    else foreach (var p in found) Console.WriteLine(p);
                    break;
                case "4":
                    Console.Write("ID для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        Console.WriteLine(manager.Remove(id) ? "Видалено." : "Не знайдено.");
                    break;
                case "5":
                    manager.DisplayStats();
                    break;
                case "0":
                    back = true;
                    break;
            }
        }
    }

    private static void HandleDoctorsMenu(DoctorManager manager)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n--- Підменю «Лікарі» ---");
            Console.WriteLine("1. Показати всіх лікарів");
            Console.WriteLine("2. Знайти за ID");
            Console.WriteLine("3. Знайти за спеціальністю");
            Console.WriteLine("4. Додати лікаря");
            Console.WriteLine("5. Видалити лікаря за ID");
            Console.WriteLine("6. Статистика");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть дію: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayAll();
                    break;
                case "2":
                    Console.Write("ID лікаря: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var doc = manager.FindById(id);
                        Console.WriteLine(doc != null ? doc.ToString() : "Не знайдено.");
                    }
                    break;
                case "3":
                    Console.Write("Спеціальність: ");
                    string specQuery = Console.ReadLine() ?? "";
                    var docs = manager.FindBySpeciality(specQuery);
                    if (docs.Length == 0) Console.WriteLine("Не знайдено.");
                    else foreach (var d in docs) Console.WriteLine(d);
                    break;
                case "4":
                    Console.Write("Ім'я: ");
                    string dfn = Console.ReadLine() ?? "";
                    Console.Write("Прізвище: ");
                    string dln = Console.ReadLine() ?? "";
                    Console.WriteLine("Спеціальність (0-7): ");
                    int.TryParse(Console.ReadLine(), out int specNumber);
                    Speciality spec = (Speciality)specNumber;

                    Console.Write("Номер ліцензії: ");
                    string lic = Console.ReadLine() ?? "";
                    Console.Write("Телефон: ");
                    string dph = Console.ReadLine() ?? "";
                    Console.Write("Початок зміни (година): ");
                    int.TryParse(Console.ReadLine(), out int startHour);
                    Console.Write("Кінець зміни (година): ");
                    int.TryParse(Console.ReadLine(), out int endHour);

                    manager.AddDoctor(new Doctor(dfn, dln, spec, lic, dph, startHour == 0 ? 8 : startHour, endHour == 0 ? 17 : endHour));
                    break;
                case "5":
                    Console.Write("ID для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int delId))
                        Console.WriteLine(manager.Remove(delId) ? "Видалено." : "Не знайдено.");
                    break;
                case "6":
                    manager.DisplayStats();
                    break;
                case "0":
                    back = true;
                    break;
            }
        }
    }

    private static void HandleAppointmentsMenu(AppointmentManager manager)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n--- Підменю «Записи» ---");
            Console.WriteLine("1. Показати всі записи");
            Console.WriteLine("2. Забронювати запис");
            Console.WriteLine("3. Скасувати запис");
            Console.WriteLine("4. Завершити прийом");
            Console.WriteLine("5. Майбутні записи");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть дію: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayList(manager.GetAll());
                    break;
                case "2":
                    Console.Write("ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("ID лікаря: ");
                    int dId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Дата та час (рррр-мм-дд гг:хв): ");
                    if (!DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime dt)) break;
                    Console.Write("Тривалість (хв): ");
                    string? durInput = Console.ReadLine();
                    int dur = string.IsNullOrWhiteSpace(durInput) ? 30 : int.Parse(durInput);
                    manager.Book(pId, dId, dt, dur);
                    break;
                case "3":
                    Console.Write("ID запису: ");
                    int cId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Причина: ");
                    string reason = Console.ReadLine() ?? "";
                    Console.WriteLine(manager.Cancel(cId, reason) ? "Запис скасовано." : "Помилка.");
                    break;
                case "4":
                    Console.Write("ID запису: ");
                    int compId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine(manager.Complete(compId) ? "Прийом завершено." : "Помилка.");
                    break;
                case "5":
                    manager.DisplayList(manager.GetUpcoming());
                    break;
                case "0":
                    back = true;
                    break;
            }
        }
    }
}
