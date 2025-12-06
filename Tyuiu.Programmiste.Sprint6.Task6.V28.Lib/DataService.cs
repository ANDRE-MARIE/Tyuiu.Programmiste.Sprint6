
using System.Text;
using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task6.V28.Lib
{
    // Дан файл InPutFileTask6V28.txt который может находится в любом месте на диске. 
    //Загрузить файл в textBoxIn через openFileDialog. Вывести предпоследнее слово каждой строки 
    //в результирующею строку и вывести ее в textBoxOut.
    //Графический интерфейс пользователя оформить по образцу как в лекции


    public class DataService : ISprint6Task6V28
    {
        public string CollectTextFromFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}", path);

            StringBuilder result = new StringBuilder();
            string[] lines = File.ReadAllLines(path);

            bool isFirstWord = true;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                // Séparer les mots en utilisant les espaces et tabulations
                string[] words = trimmedLine.Split(
                    new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries
                );

                // RÈGLE UNIQUE : Prendre l'avant-dernier mot (pénultième)
                if (words.Length >= 2)
                {
                    string penultimateWord = words[words.Length - 2];

                    if (!isFirstWord)
                    {
                        result.AppendLine();
                    }

                    result.Append(penultimateWord);
                    isFirstWord = false;
                }
            }

            return result.ToString();
        }
    }
}