

using Tyuiu.Programmiste.Sprint6.Task0.V28.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task0.V28.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void ValidCalculate()
        {
            // Arrange (Подготовка)
            DataService ds = new DataService();
            int x = 3;

            // Act (Действие)
            double result = ds.Calculate(x);

            // Assert (Проверка)
            double wait = 169.890;  // Ожидаемый результат после округления

            // Проверяем с учетом округления до 3 знаков
            Assert.AreEqual(wait, result);

            // Дополнительная проверка: вывод для отладки
            System.Diagnostics.Debug.WriteLine($"x = {x}, результат = {result}, ожидалось = {wait}");
        }

        [TestMethod]
        public void CalculateWithZero()
        {
            // Тест для x = 0
            DataService ds = new DataService();
            double result = ds.Calculate(0);

            // F(0) = 6.1*0 + 0.23*0 + 1.04*0 = 0
            Assert.AreEqual(0.000, result);
        }

        [TestMethod]
        public void CalculateWithNegative()
        {
            // Тест для x = -2
            DataService ds = new DataService();
            double result = ds.Calculate(-2);

            // F(-2) = 6.1*(-8) + 0.23*4 + 1.04*(-2) = -48.8 + 0.92 - 2.08 = -49.96
            double expected = Math.Round(-49.96, 3);  // -49.960
            Assert.AreEqual(expected, result);
        }
    }
}
