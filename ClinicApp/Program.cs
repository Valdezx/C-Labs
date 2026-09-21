
using System.Globalization;

namespace ClinicApp
{

    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
     System.Globalization.CultureInfo.InvariantCulture;
            Patient p1 = new Patient();
            Patient p2 = new Patient("Ivan", "Petrenko");
            Patient p3 = new Patient("Nadya", "Omelko");
            Patient p4 = new Patient("Максим", "Сидоренко", new DateTime(2010, 5, 12), "A+", "+380501234567");
            Patient p5 = new Patient("Ольга", "Мельник", new DateTime(1955, 11, 20), "B+", "+380671234567", "olga@gmail.com");


            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);
            Console.WriteLine(p4);
            Console.WriteLine(p5);

            Doctor d1 = new Doctor("Dr. Smith", "Кардіологія", "LCI1231");
            Doctor d2 = new Doctor("Іван Білоус", "Дерматологія", "LIC12345", "+380501234567", 8, 17);
            Doctor d3 = new Doctor("Наталія Рубінова", "Педіатрія", "LIC67890", "+380671234567", 8, 17);
            Console.WriteLine(d1);
            Console.WriteLine(d2);
            Console.WriteLine(d3);
            var manager = new PatientManager();
            manager.AddPatient(new Patient("Іван", "Петренко", new DateTime(1995, 3, 14), "A+", "+380501112233"));
            manager.AddPatient(new Patient("Ольга", "Коваль", new DateTime(2012, 7, 21), "B-", "+380672223344"));
            manager.AddPatient(new Patient("Петро", "Іваненко", new DateTime(1960, 11, 5), "O+", "+380933334455"));

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Підменю «Пацієнти» ---");
                Console.WriteLine("1. Показати всіх");
                Console.WriteLine("2. Додати пацієнта");
                Console.WriteLine("3. Знайти за ім'ям / прізвищем");
                Console.WriteLine("4. Видалити за ID");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("0. Вихід");
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
                            Console.WriteLine($"Знайдено ({found.Length}):");
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
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Невідома дія. Спробуйте ще раз.");
                        break;
                }
            }
        }

    }
}

