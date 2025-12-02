
using Tyuiu.Programmiste.Sprint6.Task1.V8.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        DataService ds = new DataService();

        Console.WriteLine("***************************************************************************");
        Console.WriteLine("* ИСХОДНЫЕ ДАННЫЕ:                                                       *");
        Console.WriteLine("***************************************************************************");

        int startValue = -5;
        int stopValue = 5;

        Console.WriteLine($"Начало диапазона: {startValue}");
        Console.WriteLine($"Конец диапазона: {stopValue}");
        Console.WriteLine();
        Console.WriteLine("Функция: F(x) = cos(x)/(x-0.4) + sin(x)*8*x + 2");
        Console.WriteLine();

        Console.WriteLine("***************************************************************************");
        Console.WriteLine("* РЕЗУЛЬТАТ:                                                             *");
        Console.WriteLine("***************************************************************************");

        Console.WriteLine("+----------+----------+");
        Console.WriteLine("|    X     |   F(x)   |");
        Console.WriteLine("+----------+----------+");

        double[] res = ds.GetMassFunction(startValue, stopValue);

        for (int i = 0; i <= stopValue - startValue; i++)
        {
            int x = startValue + i;
            Console.WriteLine("|{0,6}    | {1,8:F2} |", x, res[i]);
        }

        Console.WriteLine("+----------+----------+");
        Console.WriteLine();

        // Afficher quelques détails de calcul pour vérification
        Console.WriteLine("Пример вычисления для x = 0:");
        Console.WriteLine($"F(0) = cos(0)/(0-0.4) + sin(0)*8*0 + 2");
        Console.WriteLine($"     = 1/(-0.4) + 0 + 2");
        Console.WriteLine($"     = -2.5 + 2 = -0.5");
        Console.WriteLine($"После округления: {Math.Round(-0.5, 2)}");

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }
}
