
using Tyuiu.Programmiste.Sprint6.Task4.V8.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("=== Табулирование функции ===");
        Console.WriteLine("Функция: F(x) = sin(x) + cos(x) + 1/(2 - x) + 2x");
        Console.WriteLine("Диапазон: [-5; 5] с шагом 1");
        Console.WriteLine("=================================\n");

        try
        {
            // Create service instance
            DataService dataService = new DataService();

            // Get function values
            double[] results = dataService.GetMassFunction(-5, 5);

            // Display table in console
            DisplayTable(results);

            // Save results to file
            SaveResultsToFile(results);

            // Ask user if they want to draw chart
            AskToDrawChart(results);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    /// <summary>
    /// Displays function values table in console
    /// </summary>
    static void DisplayTable(double[] results)
    {
        Console.WriteLine("Таблица значений функции:");
        Console.WriteLine(new string('-', 40));
        Console.WriteLine("|   x   |   F(x)   |");
        Console.WriteLine(new string('-', 40));

        for (int i = 0, x = -5; x <= 5; x++, i++)
        {
            Console.WriteLine($"| {x,5} | {results[i],8:F2} |");
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine();
    }

    /// <summary>
    /// Saves results to text file
    /// </summary>
    static void SaveResultsToFile(double[] results)
    {
        try
        {
            string filePath = "OutPutFileTask4V8.txt";
            string content = "Табулирование функции F(x) = sin(x) + cos(x) + 1/(2 - x) + 2x\n";
            content += "Диапазон: [-5; 5] с шагом 1\n";
            content += "x\tF(x)\n";

            for (int i = 0, x = -5; x <= 5; x++, i++)
            {
                content += $"{x}\t{results[i]:F2}\n";
            }

            File.WriteAllText(filePath, content);
            Console.WriteLine($"Результаты сохранены в файл: {Path.GetFullPath(filePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
        }
    }

    /// <summary>
    /// Asks user if they want to see chart
    /// </summary>
    static void AskToDrawChart(double[] results)
    {
        Console.Write("\nХотите построить график в консоли? (y/n): ");
        string answer = Console.ReadLine();

        if (answer?.ToLower() == "y")
        {
            DrawConsoleChart(results);
        }
    }

    /// <summary>
    /// Draws simple chart in console
    /// </summary>
    static void DrawConsoleChart(double[] results)
    {
        Console.WriteLine("\nГрафик функции (упрощенный вид):");

        // Find min and max for scaling
        double minY = results[0];
        double maxY = results[0];
        foreach (double value in results)
        {
            if (value < minY) minY = value;
            if (value > maxY) maxY = value;
        }

        int chartHeight = 20;
        int chartWidth = 60;

        // Prepare chart array
        bool[,] chart = new bool[chartHeight, chartWidth];

        // Fill chart with points
        for (int i = 0, x = -5; x <= 5; x++, i++)
        {
            int xPos = (int)(i * (chartWidth - 1) / 10.0);
            int yPos = (int)((results[i] - minY) * (chartHeight - 1) / (maxY - minY));

            if (yPos >= 0 && yPos < chartHeight && xPos >= 0 && xPos < chartWidth)
            {
                chart[yPos, xPos] = true;
            }
        }

        // Draw chart
        for (int y = chartHeight - 1; y >= 0; y--)
        {
            Console.Write($"{minY + y * (maxY - minY) / (chartHeight - 1),6:F1} | ");

            for (int x = 0; x < chartWidth; x++)
            {
                if (chart[y, x])
                    Console.Write("*");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Draw X axis labels
        Console.Write("       ");
        for (int i = 0; i <= 10; i += 2)
        {
            Console.Write(new string(' ', 5) + (i - 5));
        }
        Console.WriteLine();
    }
}
