
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

            List<string> groups = new List<string>();
            StringBuilder currentGroup = new StringBuilder();
            string[] lines = File.ReadAllLines(path);

            bool firstWordInGroup = true;
            bool hasContentInGroup = false;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                // Si la ligne est vide, c'est un séparateur de groupes
                if (string.IsNullOrWhiteSpace(trimmedLine))
                {
                    if (hasContentInGroup)
                    {
                        groups.Add(currentGroup.ToString().Trim());
                        currentGroup.Clear();
                        firstWordInGroup = true;
                        hasContentInGroup = false;
                    }
                    continue;
                }

                // Traiter la ligne non vide
                string[] words = trimmedLine.Split(
                    new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries
                );

                if (words.Length >= 2)
                {
                    string penultimateWord = words[words.Length - 2];

                    if (!firstWordInGroup)
                    {
                        currentGroup.Append(' ');
                    }

                    currentGroup.Append(penultimateWord);
                    firstWordInGroup = false;
                    hasContentInGroup = true;
                }
            }

            // Ajouter le dernier groupe s'il y a du contenu
            if (hasContentInGroup)
            {
                groups.Add(currentGroup.ToString().Trim());
            }

            // Joindre les groupes avec " | "
            return string.Join(" | ", groups);
        }
    }
}