
using Tyuiu.Programmiste.Sprint6.Task2.V28.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task2.V28.Test
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
            // F(-5) = 2*sin(-5) - 2/(3*(-5)-1) - 3*(-5) + 2
            // sin(-5) ≈ 0.9589
            // 2/(3*(-5)-1) = 2/(-16) = -0.125
            // F(-5) = 2*0.9589 - (-0.125) + 15 + 2 ≈ 1.9178 + 0.125 + 15 + 2 ≈ 19.0428
            // Arrondi à 2 décimales: 19.04
            Assert.AreEqual(19.04, res[0]);

            // Pour x = 0
            // F(0) = 2*sin(0) - 2/(3*0-1) - 3*0 + 2 = 0 - 2/(-1) - 0 + 2 = 0 + 2 + 2 = 4
            Assert.AreEqual(4.00, res[5]);

            // Pour x = 5
            // F(5) = 2*sin(5) - 2/(3*5-1) - 3*5 + 2
            // sin(5) ≈ -0.9589
            // 2/(14) = 0.1429
            // F(5) = 2*(-0.9589) - 0.1429 - 15 + 2 ≈ -1.9178 - 0.1429 - 15 + 2 ≈ -15.0607
            // Arrondi à 2 décimales: -15.06
            Assert.AreEqual(-15.06, res[10]);

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

            double expected = Math.Round(2 * Math.Sin(0) - 2.0 / (3 * 0 - 1) - 3 * 0 + 2, 2);
            Assert.AreEqual(expected, res[0]);
        }

        [TestMethod]
        public void TestDivisionByZeroAvoidance()
        {
            DataService ds = new DataService();

            // Test avec x = 0 (dénominateur = -1, pas de division par zéro)
            double[] res = ds.GetMassFunction(0, 0);

            // Vérifier que ce n'est pas NaN ni infini
            Assert.IsFalse(double.IsNaN(res[0]));
            Assert.IsFalse(double.IsInfinity(res[0]));

            // Pour x = 0, le dénominateur est -1, donc le résultat devrait être 4
            Assert.AreEqual(4.00, res[0]);
        }

        [TestMethod]
        public void TestNegativeRange()
        {
            DataService ds = new DataService();

            double[] res = ds.GetMassFunction(-2, -1);

            Assert.AreEqual(2, res.Length);

            // Calcul manuel pour x = -2
            double expectedMinus2 = Math.Round(2 * Math.Sin(-2) - 2.0 / (3 * (-2) - 1) - 3 * (-2) + 2, 2);

            // Calcul manuel pour x = -1
            double expectedMinus1 = Math.Round(2 * Math.Sin(-1) - 2.0 / (3 * (-1) - 1) - 3 * (-1) + 2, 2);

            Assert.AreEqual(expectedMinus2, res[0]);
            Assert.AreEqual(expectedMinus1, res[1]);
        }
    }
}