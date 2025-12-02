

using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task1.V8.Lib
{
    //Написать программу, которая выводит таблицу значений функции: F(X) = cos (x)/x-0.4 + sin(x) * 8x + 2
    //(Проивести таблирование) f(x)  на заданном диапазоне
    //[-5;5] с шагом1. Произвести проверку деления на ноль. При делении на ноль вернуть значение 0. Результат вывести в Textbox
    //Значенмя округлить до двух знаков после запятой. Графический интерфейс оформить по шаблщну из лекции
    public class DataService : ISprint6Task1V8
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
                // Vérifier la division par zéro : x - 0.4 = 0 => x = 0.4
                // Comme nous utilisons des entiers, x ne sera jamais 0.4
                // Mais nous gardons la vérification pour être robuste
                double denominator = x - 0.4;

                if (Math.Abs(denominator) < 0.000001) // Éviter les problèmes de précision
                {
                    valueArray[index] = 0; // Retourner 0 en cas de division par zéro
                }
                else
                {
                    // Calculer la fonction : F(x) = cos(x)/(x-0.4) + sin(x)*8*x + 2
                    double numerator = Math.Cos(x);
                    double term1 = numerator / denominator;
                    double term2 = Math.Sin(x) * 8 * x;

                    double result = term1 + term2 + 2;

                    // Arrondir à deux décimales
                    valueArray[index] = Math.Round(result, 2);
                }

                index++;
            }

            return valueArray;
        }
    }
}