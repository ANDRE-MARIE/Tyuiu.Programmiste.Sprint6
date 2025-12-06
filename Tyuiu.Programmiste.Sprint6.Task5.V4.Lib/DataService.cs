
using System.Globalization;
using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task5.V4.Lib
{
    //Прочитать данные из файла InPutFileTask5V4.txt. Вывести в dataGridView. Вывести все целые числа.
    // Построить диаграмму по этим значениям. Графический интерфейс пользователя оформить по примеру из лекции. 
    //У вещественных значений округлить до трёх знаков после запятой.



    public class DataService : ISprint6Task5V4
    {
        public double[] LoadFromDataFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(path);

                // Filter out empty lines and whitespace
                var validLines = lines
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .ToArray();

                // Initialize result array
                double[] result = new double[validLines.Length];

                for (int i = 0; i < validLines.Length; i++)
                {
                    string line = validLines[i];

                    // Try multiple parsing strategies for number format flexibility
                    if (TryParseDouble(line, out double value))
                    {
                        // Round to 3 decimal places using MidpointRounding.AwayFromZero
                        // This ensures 1.2345 rounds to 1.235 (not 1.234)
                        result[i] = Math.Round(value, 3, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        throw new FormatException($"Invalid number format in line {i + 1}: '{line}'");
                    }
                }

                return result;
            }
            catch (Exception ex) when (!(ex is FileNotFoundException) && !(ex is FormatException))
            {
                throw new Exception($"Error reading file {path}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tries to parse a double using multiple culture formats
        /// </summary>
        private bool TryParseDouble(string input, out double result)
        {
            result = 0;

            // Normalize input: remove spaces (thousands separators) and handle different decimal separators
            string normalized = NormalizeNumber(input);

            // Try parsing with invariant culture
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;

            // Try with current culture
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.CurrentCulture, out result))
                return true;

            // Try with comma as decimal separator (common in some European cultures)
            normalized = normalized.Replace(",", ".");
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;

            return false;
        }

        /// <summary>
        /// Normalizes number string by removing thousands separators and handling spaces
        /// </summary>
        private string NormalizeNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Remove all spaces (common thousands separator)
            input = input.Replace(" ", "");

            // Remove other common thousands separators
            input = input.Replace("'", "");
            input = input.Replace("_", "");

            // Handle multiple decimal separators
            int lastDot = input.LastIndexOf('.');
            int lastComma = input.LastIndexOf(',');

            // If both . and , are present, treat the last one as decimal separator
            if (lastDot > -1 && lastComma > -1)
            {
                if (lastDot > lastComma)
                {
                    // Dot is the decimal separator, remove commas (thousands separators)
                    input = input.Replace(",", "");
                }
                else
                {
                    // Comma is the decimal separator, remove dots (thousands separators)
                    input = input.Replace(".", "");
                    // Replace comma with dot for invariant culture
                    input = input.Replace(",", ".");
                }
            }
            else if (lastComma > -1 && lastDot == -1)
            {
                // Only comma present - could be decimal or thousands separator
                int commaCount = input.Count(c => c == ',');
                if (commaCount == 1)
                {
                    // Single comma - likely decimal separator
                    input = input.Replace(",", ".");
                }
                else
                {
                    // Multiple commas - treat as thousands separators
                    input = input.Replace(",", "");
                }
            }
            // If only dot present, it's already in correct format for invariant culture

            return input;
        }
    }
}