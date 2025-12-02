

using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task0.V28.Lib
{

    //дано выражение F(x) = 6.1x^3 + 0.23x^2 + 1.04x
    //вычислить его значение при Х = 3, зезультат вывести
    //в TexBox. Округлить до трёх знаков после запятой.
    //Графический интерфейс оформить по шаблону из лекции
    public class DataService : ISprint6Task0V28
    {
        public double Calculate(int x)
        {
            // F(x) = 6.1x³ + 0.23x² + 1.04x
            double result = 6.1 * Math.Pow(x, 3) + 0.23 * Math.Pow(x, 2) + 1.04 * x;

            // Arrondir à trois chiffres après la virgule
            return Math.Round(result, 3);
        }
    }
}