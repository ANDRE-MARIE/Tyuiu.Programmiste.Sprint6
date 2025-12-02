

using Tyuiu.Programmiste.Sprint6.Task2.V28.Lib;
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
        Console.WriteLine("Функция: F(x) = sin(x) - 2/(3x-1) + sin(x) - 3x + 2");
        Console.WriteLine("Упрощённо: F(x) = 2*sin(x) - 2/(3x-1) - 3x + 2");
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
        Console.WriteLine($"F(0) = 2*sin(0) - 2/(3*0-1) - 3*0 + 2");
        Console.WriteLine($"     = 0 - 2/(-1) - 0 + 2");
        Console.WriteLine($"     = 0 + 2 + 2 = 4");
        Console.WriteLine($"После округления: {Math.Round(4.0, 2)}");

        Console.WriteLine();
        Console.WriteLine("Пример вычисления для x = 1:");
        Console.WriteLine($"F(1) = 2*sin(1) - 2/(3*1-1) - 3*1 + 2");
        Console.WriteLine($"     = 2*0.8415 - 2/2 - 3 + 2");
        Console.WriteLine($"     = 1.683 - 1 - 3 + 2 = -0.317");
        Console.WriteLine($"После округления: {Math.Round(-0.317, 2)}");

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }
}
