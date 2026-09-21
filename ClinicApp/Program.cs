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

        // Початкові дані пацієнтів
        patientManager.AddPatient(new Patient());
        patientManager.AddPatient(new Patient("Ivan", "Petrenko"));
        patientManager.AddPatient(new Patient("Nadya", "Omelko"));
        patientManager.AddPatient(new Patient("Максим", "Сидоренко", new DateTime(2010, 5, 12), "A+", "+380501234567"));
        patientManager.AddPatient(new Patient("Ольга", "Мельник", new DateTime(1955, 11, 20), "B+", "+380671234567", "olga@gmail.com"));
        patientManager.AddPatient(new Patient("Іван", "Петренко", new DateTime(1995, 3, 14), "A+", "+380501112233"));
        patientManager.AddPatient(new Patient("Ольга", "Коваль", new DateTime(2012, 7, 21), "B-", "+380672223344"));
        patientManager.AddPatient(new Patient("Петро", "Іваненко", new DateTime(1960, 11, 5), "O+", "+380933334455"));

        // Початкові дані лікарів (передаємо int години: 8, 16)
        doctorManager.AddDoctor(new Doctor("Dr. Smith", "Кардіологія", "LCI1231"));
        doctorManager.AddDoctor(new Doctor("Олег Сидоренко", "Кардіологія", "LIC-001", "0441234567", 8, 16));
        doctorManager.AddDoctor(new Doctor("Іван Білоус", "Дерматологія", "LIC12345", "+380501234567", 8, 17));
        doctorManager.AddDoctor(new Doctor("Наталія Рубінова", "Педіатрія", "LIC67890", "+380671234567", 8, 17));

        bool exitApp = false;
        while (!exitApp)
        {
            Console.WriteLine("\n===== ГОЛОВНЕ МЕНЮ КЛІНІКИ =====");
            Console.WriteLine("1. Управління пацієнтами");
            Console.WriteLine("2. Управління лікарями");
            Console.WriteLine("0. Вихід з програми");
            Console.Write("Оберіть розділ: ");

            string? sectionChoice = Console.ReadLine();
            switch (sectionChoice)
            {
                case "1":
                    HandlePatientsMenu(patientManager);
                    break;

                case "2":
                    HandleDoctorsMenu(doctorManager);
                    break;

                case "0":
                    exitApp = true;
                    Console.WriteLine("Роботу завершено.");
                    break;

                default:
                    Console.WriteLine("Невідомий пункт меню. Спробуйте ще раз.");
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
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть пункт: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayAllPatients();
                    break;

                case "2":
                    Console.Write("Введіть ім'я: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine() ?? "";

                    Console.Write("Введіть дату народження (рррр-мм-дд): ");
                    if (!DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out DateTime dob))
                    {
                        dob = DateTime.Today;
                    }

                    Console.Write("Введіть групу крові: ");
                    string bloodType = Console.ReadLine() ?? "";

                    Console.Write("Введіть номер телефону: ");
                    string phone = Console.ReadLine() ?? "";

                    manager.AddPatient(new Patient(firstName, lastName, dob, bloodType, phone));
                    break;

                case "3":
                    Console.Write("Введіть ім'я або прізвище для пошуку: ");
                    string query = Console.ReadLine() ?? "";
                    Patient[] found = manager.FindByName(query);

                    if (found.Length == 0)
                    {
                        Console.WriteLine("Пацієнтів не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine($"\nЗнайдено ({found.Length}):");
                        foreach (var p in found)
                        {
                            Console.WriteLine(p);
                        }
                    }
                    break;

                case "4":
                    Console.Write("Введіть ID пацієнта для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        if (manager.Remove(removeId))
                        {
                            Console.WriteLine($"Пацієнта з ID [{removeId}] успішно видалено.");
                        }
                        else
                        {
                            Console.WriteLine($"Пацієнта з ID [{removeId}] не знайдено.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат ID.");
                    }
                    break;

                case "5":
                    manager.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Невідома дія. Спробуйте ще раз.");
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
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.DisplayAll();
                    break;

                case "2":
                    Console.Write("Введіть ID лікаря: ");
                    if (int.TryParse(Console.ReadLine(), out int searchId))
                    {
                        Doctor? doc = manager.FindById(searchId);
                        if (doc != null)
                        {
                            Console.WriteLine($"Знайдено: {doc}");
                        }
                        else
                        {
                            Console.WriteLine($"Лікаря з ID [{searchId}] не знайдено.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат ID.");
                    }
                    break;

                case "3":
                    Console.Write("Введіть назву спеціальності: ");
                    string specialityQuery = Console.ReadLine() ?? "";
                    Doctor[] matchedDoctors = manager.FindBySpecialty(specialityQuery);

                    if (matchedDoctors.Length == 0)
                    {
                        Console.WriteLine("Лікарів за такою спеціальністю не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine($"\nЗнайдено лікарів ({matchedDoctors.Length}):");
                        foreach (var doc in matchedDoctors)
                        {
                            Console.WriteLine(doc);
                        }
                    }
                    break;

                case "4":
                    Console.Write("Введіть ID для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        if (manager.Remove(removeId))
                        {
                            Console.WriteLine($"Лікаря з ID [{removeId}] успішно видалено.");
                        }
                        else
                        {
                            Console.WriteLine($"Лікаря з ID [{removeId}] не знайдено.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат ID.");
                    }
                    break;

                case "5":
                    manager.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Невідомий пункт меню. Спробуйте ще раз.");
                    break;
            }
        }
    }
}

