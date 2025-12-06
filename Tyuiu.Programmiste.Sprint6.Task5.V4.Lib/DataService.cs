
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

            List<double> numbers = new List<double>();
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string normalized = NormalizeNumber(line.Trim());

                if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
                {
                    // Arrondi à 3 décimales avec la règle AwayFromZero
                    double rounded = Math.Round(number, 3, MidpointRounding.AwayFromZero);
                    numbers.Add(rounded);
                }
                else
                {
                    throw new FormatException($"Invalid number format in line: {line}");
                }
            }

            return numbers.ToArray();
        }

        private string NormalizeNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Supprimer les séparateurs de milliers courants
            string result = input.Replace(" ", "")
                                 .Replace("'", "")
                                 .Replace("_", "");

            // Remplacer la virgule par un point pour le séparateur décimal
            result = result.Replace(",", ".");

            // Gérer le cas où il y a plusieurs points
            // Garder uniquement le dernier point comme séparateur décimal
            int dotCount = result.Count(c => c == '.');
            if (dotCount > 1)
            {
                int lastIndex = result.LastIndexOf('.');
                result = result.Replace(".", "");
                result = result.Insert(lastIndex - (dotCount - 1), ".");
            }

            return result;
        }
    }
}