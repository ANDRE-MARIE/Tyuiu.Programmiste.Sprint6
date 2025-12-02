
using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task2.V28.Lib
{
 //Написать программу, которая выводит таблицу значений функции: F(X) = sin (x) - 2/3x -1 + sin(x) - 3x + 2
//(Проивести таблирование) f(x)  на заданном диапазоне
//[-5;5] с шагом1. Произвести проверку деления на ноль. При делении на ноль вернуть значение 0. Значения занести в DataGridView
    //Значенмя округлить до двух знаков после запятой. Графический интерфейс оформить по шаблщну из лекции


    public class DataService : ISprint6Task2V28
    {
        public double[] GetMassFunction(int startValue, int stopValue)
        {
            // Calculer la taille du tableau
            int len = stopValue - startValue + 1;
            double[] valueArray = new double[len];

            // Variable pour suivre l'index dans le tableau
            int index = 0;

            // Parcourir toutes les valeurs de x dans l'intervalle
            for (int x = startValue; x <= stopValue; x++)
            {
                // Vérifier la division par zéro : 3x - 1 = 0 => x = 1/3
                // Comme nous utilisons des entiers, x ne sera jamais 1/3
                // Mais nous gardons la vérification pour être robuste
                double denominator = 3 * x - 1;

                if (Math.Abs(denominator) < 0.000001) // Éviter les problèmes de précision
                {
                    valueArray[index] = 0; // Retourner 0 en cas de division par zéro
                }
                else
                {
                    // Calculer la fonction : F(x) = sin(x) - 2/(3x-1) + sin(x) - 3x + 2
                    // Simplifié : F(x) = 2*sin(x) - 2/(3x-1) - 3x + 2
                    double term1 = 2 * Math.Sin(x);
                    double term2 = 2.0 / denominator;
                    double term3 = -3 * x;

                    double result = term1 - term2 + term3 + 2;

                    // Arrondir à deux décimales
                    valueArray[index] = Math.Round(result, 2);
                }

                index++;
            }

            return valueArray;
        }
    }
}
