
using Tyuiu.Programmiste.Sprint6.Task1.V8.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task1.V8.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void ValidGetMassFunction()
        {
            DataService ds = new DataService();

            int startValue = -5;
            int stopValue = 5;

            double[] res = ds.GetMassFunction(startValue, stopValue);

            // Vérifier la taille du tableau
            Assert.AreEqual(11, res.Length);

            // Vérifier quelques valeurs spécifiques (calculées manuellement)
            // Pour x = -5
            double expectedMinus5 = Math.Round(Math.Cos(-5) / (-5 - 0.4) + Math.Sin(-5) * 8 * (-5) + 2, 2);
            Assert.AreEqual(expectedMinus5, res[0]);

            // Pour x = 0
            double expectedZero = Math.Round(Math.Cos(0) / (0 - 0.4) + Math.Sin(0) * 8 * 0 + 2, 2);
            Assert.AreEqual(expectedZero, res[5]);

            // Pour x = 5
            double expectedFive = Math.Round(Math.Cos(5) / (5 - 0.4) + Math.Sin(5) * 8 * 5 + 2, 2);
            Assert.AreEqual(expectedFive, res[10]);

            // Vérifier que tous les éléments sont arrondis à 2 décimales
            foreach (double value in res)
            {
                // Multiplier par 100 et vérifier qu'il n'y a pas de partie décimale
                double multiplied = value * 100;
                Assert.AreEqual(Math.Round(multiplied), multiplied, 0.0001,
                    $"Значение {value} не округлено до 2 десятичных знаков");
            }
        }

        [TestMethod]
        public void TestSingleValueRange()
        {
            DataService ds = new DataService();

            // Test avec une seule valeur
            double[] res = ds.GetMassFunction(0, 0);

            Assert.AreEqual(1, res.Length);

            double expected = Math.Round(Math.Cos(0) / (0 - 0.4) + Math.Sin(0) * 8 * 0 + 2, 2);
            Assert.AreEqual(expected, res[0]);
        }

        [TestMethod]
        public void TestDivisionByZeroAvoidance()
        {
            DataService ds = new DataService();

            // Test avec une valeur qui pourrait causer une division par zéro
            // x = 0.4 n'est pas un entier, donc ne sera pas dans notre plage
            // Mais testons avec des valeurs proches

            double[] res = ds.GetMassFunction(0, 1); // Contient x = 0 et x = 1

            // Aucune division par zéro ne devrait se produire
            Assert.IsFalse(double.IsNaN(res[0]));
            Assert.IsFalse(double.IsNaN(res[1]));
            Assert.IsFalse(double.IsInfinity(res[0]));
            Assert.IsFalse(double.IsInfinity(res[1]));
        }

        [TestMethod]
        public void TestNegativeRange()
        {
            DataService ds = new DataService();

            double[] res = ds.GetMassFunction(-2, -1);

            Assert.AreEqual(2, res.Length);

            double expectedMinus2 = Math.Round(Math.Cos(-2) / (-2 - 0.4) + Math.Sin(-2) * 8 * (-2) + 2, 2);
            double expectedMinus1 = Math.Round(Math.Cos(-1) / (-1 - 0.4) + Math.Sin(-1) * 8 * (-1) + 2, 2);

            Assert.AreEqual(expectedMinus2, res[0]);
            Assert.AreEqual(expectedMinus1, res[1]);
        }
    }
}
