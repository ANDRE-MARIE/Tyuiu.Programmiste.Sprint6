
using Tyuiu.Programmiste.Sprint6.Task4.V8.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task4.V8.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void TestGetMassFunction_NormalRange()
        {
            // Arrange
            DataService ds = new DataService();

            // Act
            double[] results = ds.GetMassFunction(-5, 5);

            // Assert
            // Check array length (from -5 to 5 inclusive = 11 elements)
            Assert.AreEqual(11, results.Length);

            // Check division by zero case (x = 2 should return 0)
            Assert.AreEqual(0, results[7], 0.001); // Index 7 corresponds to x = 2

            // Check rounding to 2 decimal places
            foreach (double result in results)
            {
                // Check that it's rounded to 2 decimal places
                double roundedValue = Math.Round(result, 2);
                Assert.AreEqual(roundedValue, result, 0.0001);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetMassFunction_InvalidRange()
        {
            // Arrange
            DataService ds = new DataService();

            // Act - This should throw ArgumentException
            double[] results = ds.GetMassFunction(5, -5);
        }

        [TestMethod]
        public void TestGetMassFunction_SingleValue()
        {
            // Arrange
            DataService ds = new DataService();

            // Act
            double[] results = ds.GetMassFunction(0, 0);

            // Assert
            Assert.AreEqual(1, results.Length);
            // F(0) = sin(0) + cos(0) + 1/(2-0) + 2*0 = 0 + 1 + 0.5 + 0 = 1.5
            Assert.AreEqual(1.5, results[0], 0.001);
        }

        [TestMethod]
        public void TestGetMassFunction_WithDivisionByZero()
        {
            // Arrange
            DataService ds = new DataService();

            // Act
            double[] results = ds.GetMassFunction(2, 2);

            // Assert
            Assert.AreEqual(1, results.Length);
            // Division by zero should return 0
            Assert.AreEqual(0, results[0], 0.001);
        }

        [TestMethod]
        public void TestGetMassFunction_NegativeRange()
        {
            // Arrange
            DataService ds = new DataService();

            // Act
            double[] results = ds.GetMassFunction(-2, -2);

            // Assert
            Assert.AreEqual(1, results.Length);
            // F(-2) = sin(-2) + cos(-2) + 1/(2-(-2)) + 2*(-2)
            double expected = Math.Sin(-2) + Math.Cos(-2) + 1.0 / 4.0 + (-4);
            expected = Math.Round(expected, 2);
            Assert.AreEqual(expected, results[0], 0.001);
        }
    }
}