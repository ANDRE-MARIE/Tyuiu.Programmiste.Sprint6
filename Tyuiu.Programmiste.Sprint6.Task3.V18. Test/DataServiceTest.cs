
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyuiu.Programmiste.Sprint6.Task3.V18._Lib;

namespace Tyuiu.Programmiste.Sprint6.Task3.V18._Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void ValidCalculate()
        {
            DataService ds = new DataService();

            int[,] matrix = {
                {-19, -19, 1, 18, 7},
                {5, 3, -4, -6, -12},
                {-15, 6, 2, 2, -14},
                {-9, -10, 15, -5, -6},
                {-13, -15, -9, 7, 1}
            };

            // Appeler la méthode Calculate
            int[,] result = ds.Calculate(matrix);

            // CORRECTION 1: Assert.AreEqual au lieu de Assert.Arefqual
            // CORRECTION 2: Comparer directement les valeurs numériques au lieu des chaînes

            // Vérifier uniquement la 4ème ligne transformée
            Assert.AreEqual(-9, result[3, 0]);
            Assert.AreEqual(0, result[3, 1]);  // -10 → 0 (pair)
            Assert.AreEqual(15, result[3, 2]);
            Assert.AreEqual(-5, result[3, 3]);
            Assert.AreEqual(0, result[3, 4]);  // -6 → 0 (pair)

            // Vérifier que les autres lignes restent inchangées
            Assert.AreEqual(-19, result[0, 0]);
            Assert.AreEqual(-19, result[0, 1]);
            Assert.AreEqual(1, result[0, 2]);
            Assert.AreEqual(18, result[0, 3]);
            Assert.AreEqual(7, result[0, 4]);

            Assert.AreEqual(5, result[1, 0]);
            Assert.AreEqual(3, result[1, 1]);
            Assert.AreEqual(-4, result[1, 2]);
            Assert.AreEqual(-6, result[1, 3]);
            Assert.AreEqual(-12, result[1, 4]);

            Assert.AreEqual(-15, result[2, 0]);
            Assert.AreEqual(6, result[2, 1]);
            Assert.AreEqual(2, result[2, 2]);
            Assert.AreEqual(2, result[2, 3]);
            Assert.AreEqual(-14, result[2, 4]);

            Assert.AreEqual(-13, result[4, 0]);
            Assert.AreEqual(-15, result[4, 1]);
            Assert.AreEqual(-9, result[4, 2]);
            Assert.AreEqual(7, result[4, 3]);
            Assert.AreEqual(1, result[4, 4]);
        }
    }
}