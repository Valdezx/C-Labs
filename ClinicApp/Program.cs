using System;
using System.Globalization;
using System.Threading;

using System;
using System.Globalization;
using System.Threading;

namespace ClinicApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var patientManager = new PatientManager();
        var doctorManager = new DoctorManager();
        var appointmentManager = new AppointmentManager(patientManager, doctorManager);

        // Початкові дані
        patientManager.AddPatient(new Patient("Іван", "Петренко", new DateTime(1995, 3, 14), "A+", "+380501112233"));
        patientManager.AddPatient(new Patient("Ольга", "Коваль", new DateTime(2012, 7, 21), "B-", "+380672223344"));
        patientManager.AddPatient(new Patient("Петро", "Іваненко", new DateTime(1960, 11, 5), "O+", "+380933334455"));

        doctorManager.AddDoctor(new Doctor("Олег Сидоренко", "Кардіологія", "LIC-001", "0441234567", 8, 16));
        doctorManager.AddDoctor(new Doctor("Іван Білоус", "Дерматологія", "LIC12345", "+380501234567", 8, 17));
        doctorManager.AddDoctor(new Doctor("Наталія Рубінова", "Педіатрія", "LIC67890", "+380671234567", 8, 17));

        // Тестові записи
        appointmentManager.Book(1, 1, DateTime.Now.AddHours(2), 30);
        appointmentManager.Book(2, 3, DateTime.Now.AddDays(1).Date.AddHours(10), 45);

        bool exitApp = false;
        while (!exitApp)
        {
            Console.WriteLine("\n===== ГОЛОВНЕ МЕНЮ КЛІНІКИ =====");
            Console.WriteLine("1. Управління пацієнтами");
            Console.WriteLine("2. Управління лікарями");
            Console.WriteLine("3. Управління записами");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть розділ: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    HandlePatientsMenu(patientManager);
                    break;
                case "2":
                    HandleDoctorsMenu(doctorManager);
                    break;
                case "3":
                    HandleAppointmentsMenu(appointmentManager);
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

    private static void HandleAppointmentsMenu(AppointmentManager manager)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\n--- Підменю «Записи» ---");
            Console.WriteLine("1. Показати всі записи");
            Console.WriteLine("2. Забронювати новий запис");
            Console.WriteLine("3. Скасувати запис");
            Console.WriteLine("4. Завершити прийом (Complete)");
            Console.WriteLine("5. Показати майбутні записи (Upcoming)");
            Console.WriteLine("6. Записи пацієнта");
            Console.WriteLine("7. Записи лікаря");
            Console.WriteLine("8. Записи на дату");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть дію: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayList(manager.GetAll());
                    break;

                case "2":
                    Console.Write("Введіть ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Введіть ID лікаря: ");
                    int dId = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Введіть дату та час (рррр-мм-дд гг:хв): ");
                    if (!DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime dt))
                    {
                        Console.WriteLine("Некоректна дата/час.");
                        break;
                    }

                    Console.Write("Тривалість у хвилинах (за замовчуванням 30): ");
                    string? durInput = Console.ReadLine();
                    int dur = string.IsNullOrWhiteSpace(durInput) ? 30 : int.Parse(durInput);

                    manager.Book(pId, dId, dt, dur);
                    break;

                case "3":
                    Console.Write("Введіть ID запису для скасування: ");
                    int cId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Причина скасування: ");
                    string reason = Console.ReadLine() ?? "";
                    if (manager.Cancel(cId, reason))
                    {
                        Console.WriteLine("Запис скасовано.");
                    }
                    else
                    {
                        Console.WriteLine("Не вдалося скасувати запис (не знайдено або вже не в статусі Scheduled).");
                    }
                    break;

                case "4":
                    Console.Write("Введіть ID запису для завершення: ");
                    int compId = int.Parse(Console.ReadLine() ?? "0");
                    if (manager.Complete(compId))
                    {
                        Console.WriteLine("Прийом успішно завершено.");
                    }
                    else
                    {
                        Console.WriteLine("Не вдалося завершити запис.");
                    }
                    break;

                case "5":
                    Console.WriteLine("\nМайбутні записи:");
                    manager.DisplayList(manager.GetUpcoming());
                    break;

                case "6":
                    Console.Write("Введіть ID пацієнта: ");
                    int filterPId = int.Parse(Console.ReadLine() ?? "0");
                    manager.DisplayList(manager.GetByPatient(filterPId));
                    break;

                case "7":
                    Console.Write("Введіть ID лікаря: ");
                    int filterDId = int.Parse(Console.ReadLine() ?? "0");
                    manager.DisplayList(manager.GetByDoctor(filterDId));
                    break;

                case "8":
                    Console.Write("Введіть дату (рррр-мм-дд): ");
                    if (DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime searchDate))
                    {
                        manager.DisplayList(manager.GetByDate(searchDate));
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат дати.");
                    }
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Невідома дія.");
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
                    Console.Write("Група крові: ");
                    string bt = Console.ReadLine() ?? "";
                    Console.Write("Телефон: ");
                    string ph = Console.ReadLine() ?? "";
                    manager.AddPatient(new Patient(fn, ln, dob, bt, ph));
                    break;
                case "3":
                    Console.Write("Пошуковий запит: ");
                    var found = manager.FindByName(Console.ReadLine() ?? "");
                    if (found.Length == 0) Console.WriteLine("Нікого не знайдено.");
                    else foreach (var p in found) Console.WriteLine(p);
                    break;
                case "4":
                    Console.Write("ID для видалення: ");
                    int id = int.Parse(Console.ReadLine() ?? "0");
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
            Console.WriteLine("4. Видалити лікаря за ID");
            Console.WriteLine("5. Статистика");
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
                    int id = int.Parse(Console.ReadLine() ?? "0");
                    var doc = manager.FindById(id);
                    Console.WriteLine(doc != null ? doc.ToString() : "Не знайдено.");
                    break;
                case "3":
                    Console.Write("Спеціальність: ");
                    var docs = manager.FindBySpecialty(Console.ReadLine() ?? "");
                    if (docs.Length == 0) Console.WriteLine("Не знайдено.");
                    else foreach (var d in docs) Console.WriteLine(d);
                    break;
                case "4":
                    Console.Write("ID для видалення: ");
                    int delId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine(manager.Remove(delId) ? "Видалено." : "Не знайдено.");
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
}
