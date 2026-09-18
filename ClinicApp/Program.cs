
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
            Patient p3 = new Patient("Nadya", "Omelko" );
            Patient p4 = new Patient("Максим", "Сидоренко", new DateTime(2010, 5, 12), "A+", "+380501234567");
            Patient p5 = new Patient("Ольга", "Мельник", new DateTime(1955, 11, 20), "B+", "+380671234567", "olga@gmail.com");


            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p3);
            Console.WriteLine(p4);
            Console.WriteLine(p5);
        }
    }


}

