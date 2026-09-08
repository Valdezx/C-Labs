

namespace Lab01
{
    internal class Program
    {

    private static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
    System.Globalization.CultureInfo.InvariantCulture;
            Task1.Run();
            Task2.Run();
        }
}
}
