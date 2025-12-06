
using Tyuiu.Programmiste.Sprint6.Task4.V8.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task4.V8.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void TestGetMassFunction_NormalRange()
        {
            DataService ds = new DataService();
            // Act
            double[] results = ds.GetMassFunction(-5, 5);

            // Assert - selon l'énoncé
            Assert.AreEqual(11, results.Length);

            // Vérifier les valeurs selon l'énoncé
            Assert.AreEqual(-8.86, results[0], 0.001);   // x = -5
            Assert.AreEqual(-7.19, results[1], 0.001);   // x = -4
            Assert.AreEqual(-6.14, results[2], 0.001);   // x = -3
            Assert.AreEqual(-4.76, results[3], 0.001);   // x = -2
            Assert.AreEqual(-2.33, results[4], 0.001);   // x = -1
            Assert.AreEqual(1.0, results[5], 0.001);     // x = 0
            Assert.AreEqual(4.38, results[6], 0.001);    // x = 1
            Assert.AreEqual(0.0, results[7], 0.001);     // x = 2 (division par zéro)
            Assert.AreEqual(6.13, results[8], 0.001);    // x = 3
            Assert.AreEqual(7.07, results[9], 0.001);    // x = 4
            Assert.AreEqual(8.61, results[10], 0.001);   // x = 5
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
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
            // Selon l'énoncé, F(0) = 1.0
            Assert.AreEqual(1.0, results[0], 0.001);
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
            // Division par zéro should return 0
            Assert.AreEqual(0.0, results[0], 0.001);
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
            // Selon l'énoncé, F(-2) = -4.76
            Assert.AreEqual(-4.76, results[0], 0.001);
        }
    }
}