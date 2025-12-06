
using System.Globalization;
using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task7.V23.Lib
{

    //Дан файл InPutFileTask7V23.csv в котором хранится матрица целочисленных значений. 
    //Загрузить файл через openFileDialog в объект dataGridViewIn. Изменить в последнем столбце значения меньше 2 на 2. 
    //Результат вывести в объект dataGridViewOut. Сохранить результат в файл OutPutFileTask7.csv через saveFileDialog.
    //Графический интерфейс пользователя оформить по образцу как в лекции
    public class DataService : ISprint6Task7V23
    {
        public int[,] GetMatrix(string path)
        {
            string[] allFileLines = File.ReadAllLines(path);

            // Déterminer les dimensions de la matrice
            int rows = allFileLines.Length;
            int columns = allFileLines[0].Split(';').Length;

            // Créer la matrice
            int[,] matrix = new int[rows, columns];

            // Remplir la matrice avec les valeurs du fichier
            for (int i = 0; i < rows; i++)
            {
                string[] line = allFileLines[i].Split(';');
                for (int j = 0; j < columns; j++)
                {
                    // Convertir chaque valeur en entier
                    matrix[i, j] = int.Parse(line[j], CultureInfo.InvariantCulture);
                }
            }

            // Modifier les valeurs dans la dernière colonne qui sont inférieures à 2
            for (int i = 0; i < rows; i++)
            {
                // Vérifier la valeur dans la dernière colonne
                int lastColumnIndex = columns - 1;
                if (matrix[i, lastColumnIndex] < 2)
                {
                    matrix[i, lastColumnIndex] = 2;
                }
            }

            return matrix;
        }

        // Méthode supplémentaire pour sauvegarder la matrice
        public void SaveMatrixToFile(int[,] matrix, string path)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        writer.Write(matrix[i, j]);
                        if (j < columns - 1)
                        {
                            writer.Write(";");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}