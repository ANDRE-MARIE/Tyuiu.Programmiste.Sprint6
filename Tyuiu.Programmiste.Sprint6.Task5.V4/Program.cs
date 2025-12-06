
using System.Globalization;
using Tyuiu.Programmiste.Sprint6.Task5.V4.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        // Set invariant culture for consistent number parsing
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Data File Reader ===");
        Console.WriteLine("Reading data from InPutFileTask5V4.txt");
        Console.WriteLine("Current Culture: " + CultureInfo.CurrentCulture.Name);
        Console.WriteLine("Decimal Separator: " + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        Console.WriteLine("=========================\n");

        try
        {
            // Define file path (adjust as needed)
            string filePath = "InPutFileTask5V4.txt";

            // Check if file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                Console.WriteLine("Creating sample data file...");
                CreateSampleFile(filePath);
            }

            // Display file content
            Console.WriteLine("File Content:");
            Console.WriteLine(new string('-', 40));
            string[] fileLines = File.ReadAllLines(filePath);
            for (int i = 0; i < fileLines.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {fileLines[i]}");
            }
            Console.WriteLine(new string('-', 40));

            // Create service instance
            DataService dataService = new DataService();

            // Load data from file
            double[] data = dataService.LoadFromDataFile(filePath);

            // Display results
            DisplayResults(data);

            // Display integer values only
            DisplayIntegerValues(data);

            // Create simple console chart
            CreateConsoleChart(data);

            // Save processed data to output file
            SaveProcessedData(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Creates a sample data file if it doesn't exist
    /// </summary>
    static void CreateSampleFile(string filePath)
    {
        string[] sampleData = {
                "12.5",
                "25",
                "37.8",
                "42.1234",
                "55",
                "67.456",
                "80",
                "92.789",
                "100",
                "112.3",
                "125",
                "137.654",
                "150"
            };

        File.WriteAllLines(filePath, sampleData);
        Console.WriteLine($"Sample file created: {filePath}");
    }

    /// <summary>
    /// Displays all loaded data
    /// </summary>
    static void DisplayResults(double[] data)
    {
        Console.WriteLine($"\nTotal values loaded: {data.Length}");
        Console.WriteLine("All values (rounded to 3 decimal places with AwayFromZero):");
        Console.WriteLine(new string('-', 50));

        for (int i = 0; i < data.Length; i++)
        {
            Console.WriteLine($"[{i + 1,3}] {data[i],12:F3}");
        }

        Console.WriteLine(new string('-', 50));
    }

    /// <summary>
    /// Displays only integer values
    /// </summary>
    static void DisplayIntegerValues(double[] data)
    {
        Console.WriteLine("\nInteger values only:");
        Console.WriteLine(new string('-', 30));

        int integerCount = 0;
        for (int i = 0; i < data.Length; i++)
        {
            // Check if value is integer (no fractional part)
            if (Math.Abs(data[i] % 1) < 0.000001)
            {
                Console.WriteLine($"[{i + 1,3}] {(int)data[i],10}");
                integerCount++;
            }
        }

        Console.WriteLine(new string('-', 30));
        Console.WriteLine($"Total integers found: {integerCount}");
    }

    /// <summary>
    /// Creates a simple console chart
    /// </summary>
    static void CreateConsoleChart(double[] data)
    {
        if (data.Length == 0)
        {
            Console.WriteLine("\nNo data available for chart.");
            return;
        }

        Console.WriteLine("\nSimple Bar Chart:");
        Console.WriteLine(new string('-', 60));

        // Find min and max for scaling
        double minValue = data.Min();
        double maxValue = data.Max();
        double range = maxValue - minValue;

        if (range == 0) range = 1; // Avoid division by zero

        int chartWidth = 40;

        for (int i = 0; i < data.Length; i++)
        {
            // Scale value to chart width
            int barLength = (int)((data[i] - minValue) * chartWidth / range);
            barLength = Math.Max(1, barLength); // At least 1 character

            // Display bar
            Console.Write($"[{i + 1,3}] {data[i],7:F1} | ");
            Console.Write(new string('█', barLength));
            Console.WriteLine($" ({data[i]:F3})");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"Range: {minValue:F1} to {maxValue:F1}");
    }

    /// <summary>
    /// Saves processed data to output file
    /// </summary>
    static void SaveProcessedData(double[] data)
    {
        try
        {
            string outputPath = "OutPutFileTask5V4.txt";

            string[] outputLines = new string[data.Length + 4];
            outputLines[0] = "=== Processed Data ===";
            outputLines[1] = $"Total values: {data.Length}";
            outputLines[2] = "Values rounded to 3 decimal places (MidpointRounding.AwayFromZero):";
            outputLines[3] = "";

            for (int i = 0; i < data.Length; i++)
            {
                outputLines[i + 4] = $"{data[i]:F3}";
            }

            outputLines[data.Length + 3] = "=====================";

            File.WriteAllLines(outputPath, outputLines);
            Console.WriteLine($"\nData saved to: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError saving file: {ex.Message}");
        }
    }
}

