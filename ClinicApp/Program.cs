using ClinicApp.Enums;
using ClinicApp.Managers;
using System;
using System.Globalization;
using System.Threading;
using ClinicApp.Models;


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

        var growable = new GrowablePatientManager();
        Console.WriteLine($"Початкова ємність: {growable.Capacity}");

        for (int i = 1; i <= 6; i++)
        {
            growable.AddPatient(new Patient($"Тест{i}", $"Пацієнт{i}"));
            Console.WriteLine($"Додано [{i}]. Розмір: {growable.Count} / {growable.Capacity}");
        }

        bool exitApp = false;
        while (!exitApp)
        {
            Console.WriteLine($"\n===== {clinic.Name.ToUpper()} =====");
            Console.WriteLine("1. Управління пацієнтами");
            Console.WriteLine("2. Управління лікарями");
            Console.WriteLine("3. Управління записами");
            Console.WriteLine("4. Розклад на дату");
            Console.WriteLine("5. Генерація повного звіту клініки");
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
                    Console.Write("Введіть дату (рррр-мм-дд) [Enter для поточної]: ");
                    string? dateInput = Console.ReadLine();
                    DateTime targetDate = string.IsNullOrWhiteSpace(dateInput)
                        ? DateTime.Today
                        : (DateTime.TryParse(dateInput, CultureInfo.InvariantCulture, out DateTime d) ? d : DateTime.Today);

                    clinic.DisplaySchedule(targetDate);
                    break;
                case "5":
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

                    Console.WriteLine("Оберіть групу крові:");
                    Console.WriteLine("0 - Unknown, 1 - OPositive, 2 - ONegative, 3 - APositive, 4 - ANegative, 5 - BPositive, 6 - BNegative, 7 - ABPositive, 8 - ABNegative");
                    Console.Write("Номер: ");
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
                    Console.Write("Спеціальність (наприклад, Cardiologist): ");
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

                    Console.WriteLine("Оберіть спеціальність:");
                    Console.WriteLine("0 - General, 1 - Therapist, 2 - Cardiologist, 3 - Neurologist, 4 - Pediatrician, 5 - Dermatologist, 6 - Surgeon, 7 - Ophthalmologist");
                    Console.Write("Номер: ");
                    int.TryParse(Console.ReadLine(), out int specNumber);
                    Speciality spec = (Speciality)specNumber;

                    Console.Write("Номер ліцензії: ");
                    string lic = Console.ReadLine() ?? "";
                    Console.Write("Телефон: ");
                    string dph = Console.ReadLine() ?? "";
                    Console.Write("Початок зміни (година, напр. 8): ");
                    int.TryParse(Console.ReadLine(), out int startHour);
                    Console.Write("Кінець зміни (година, напр. 17): ");
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
                    if (!DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime dt))
                    {
                        Console.WriteLine("Некоректна дата/час.");
                        break;
                    }
                    Console.Write("Тривалість (хв, дефолт 30): ");
                    string? durInput = Console.ReadLine();
                    int dur = string.IsNullOrWhiteSpace(durInput) ? 30 : int.Parse(durInput);
                    manager.Book(pId, dId, dt, dur);
                    break;
                case "3":
                    Console.Write("ID запису: ");
                    int cId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Причина скасування: ");
                    string reason = Console.ReadLine() ?? "";
                    Console.WriteLine(manager.Cancel(cId, reason) ? "Запис скасовано." : "Помилка скасування.");
                    break;
                case "4":
                    Console.Write("ID запису: ");
                    int compId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine(manager.Complete(compId) ? "Прийом завершено." : "Помилка завершення.");
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