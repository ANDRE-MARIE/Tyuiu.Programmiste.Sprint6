

using tyuiu.cources.programming.interfaces.Sprint6;
namespace Tyuiu.Programmiste.Sprint6.Task4.V8.Lib
{
    //Написать программу, которая выводит таблицу значений функции: F(X) = sin (x) + cos(x) + 1/ 2 - x  + 2x
    //(Проивести таблирование) f(x)  на заданном диапазоне
    //[-5;5] с шагом1. Произвести проверку деления на ноль. При делении на ноль вернуть значение 0. 
    //Округлить до двух знаков после запятой.Результат вывести в Textbox
    //Построить График функции и сохранить в файл OutPutFileTask4V8.tex по нажатинию кнопки.
    //Графический интерфейс пользователя оформить по примеру из лекции.



    public class DataService : ISprint6Task4V8
    {
        public double[] GetMassFunction(int startValue, int stopValue)
        {
            if (startValue > stopValue)
            {
                throw new ArgumentException("Начальное значение должно быть меньше или равно конечному значению", nameof(startValue));
            }

            int size = Math.Abs(stopValue - startValue) + 1;
            double[] results = new double[size];

            for (int i = 0; i < size; i++)
            {
                int x = startValue + i;
                double result = 0;

                try
                {
                    // Calculate F(x) = sin(x) + cos(x) + 1/(2 - x) + 2x
                    double denominator = 2.0 - x;

                    // Check for division by zero
                    if (Math.Abs(denominator) < 0.0000001)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = Math.Sin(x) + Math.Cos(x) + (1.0 / denominator) + (2 * x);
                    }
                }
                catch (DivideByZeroException)
                {
                    result = 0;
                }
                catch (Exception)
                {
                    result = 0;
                }

                // Round to 2 decimal places
                results[i] = Math.Round(result, 2);
            }

            return results;
        }
    }
}