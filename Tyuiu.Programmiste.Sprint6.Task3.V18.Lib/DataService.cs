

using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task3.V18.Lib
{
    // Дан массив 5 на 5 элементов. Заменить четные значения в четвертой строке на 0.
    //Результат вывести в DataGridView.
    //Графический интерфейс оформить по шаблону из лекции.
    // -19 -19   1  18   7
    //  5   3  -4  -6 -12
    // -15   6   2   2 -14
    //-9 -10  15  -5  -6
    //-13 -15  -9   7   1
    public class DataService : ISprint6Task3V18
    {
        public int[,] Calculate(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            // Copier la matrice
            int[,] resultMatrix = (int[,])matrix.Clone();

            // Remplacer les valeurs paires de la 4ème ligne par 0
            for (int j = 0; j < columns; j++)
            {
                if (resultMatrix[3, j] % 2 == 0)
                {
                    resultMatrix[3, j] = 0;
                }
            }

            return resultMatrix;
        }

        public string CollectTextFromFile(string path)
        {
            // Implémentation basique pour Task3
            return "Méthode non utilisée pour Task3";
        }
    }
}

