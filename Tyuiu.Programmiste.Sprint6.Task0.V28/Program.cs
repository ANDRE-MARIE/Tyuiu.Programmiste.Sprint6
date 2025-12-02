

using Tyuiu.Programmiste.Sprint6.Task0.V28.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("***************************************************************************");
        Console.WriteLine("* ИСХОДНЫЕ ДАННЫЕ:                                                       *");
        Console.WriteLine("***************************************************************************");
        Console.WriteLine("Вычислить значение выражения F(x) = 6.1x³ + 0.23x² + 1.04x");
        Console.WriteLine("при x = 3");
        Console.WriteLine("Результат вывести с округлением до трёх знаков после запятой");
        Console.WriteLine();

        Console.WriteLine("***************************************************************************");
        Console.WriteLine("* РЕЗУЛЬТАТ:                                                             *");
        Console.WriteLine("***************************************************************************");

        try
        {
            // Créer l'instance du service
            DataService ds = new DataService();

            // Calculer pour x = 3
            double result = ds.Calculate(3);

            // Afficher le résultat
            Console.WriteLine($"Значение функции F(3) = {result:F3}");

            // Vérification manuelle du calcul
            Console.WriteLine();
            Console.WriteLine("Проверка расчета:");
            Console.WriteLine($"6.1 * 3³ + 0.23 * 3² + 1.04 * 3 =");
            Console.WriteLine($"6.1 * 27 + 0.23 * 9 + 1.04 * 3 =");
            Console.WriteLine($"164.7 + 2.07 + 3.12 = 169.89");
            Console.WriteLine($"После округления: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }
}
