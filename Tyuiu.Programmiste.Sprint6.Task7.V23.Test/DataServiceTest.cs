
using Tyuiu.Programmiste.Sprint6.Task7.V23.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task7.V23.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        [TestMethod]
        public void ValidGetMatrix()
        {
            DataService ds = new DataService();

            // Créer un fichier de test temporaire
            string path = @"C:\temp\test_matrix.csv";

            // Contenu de test
            string testContent = "5;3;1\n2;4;0\n6;7;1\n8;9;3";
            File.WriteAllText(path, testContent);

            // Appeler la méthode à tester
            int[,] result = ds.GetMatrix(path);

            // Vérifications
            Assert.AreEqual(4, result.GetLength(0)); // 4 lignes
            Assert.AreEqual(3, result.GetLength(1)); // 3 colonnes

            // Vérifier que la dernière colonne a été modifiée correctement
            // Les valeurs 0 et 1 doivent devenir 2
            Assert.AreEqual(1, result[0, 2]); // 1 reste 1 (>=2)
            Assert.AreEqual(2, result[1, 2]); // 0 devient 2
            Assert.AreEqual(2, result[2, 2]); // 1 devient 2
            Assert.AreEqual(3, result[3, 2]); // 3 reste 3

            // Nettoyer
            File.Delete(path);
        }

        [TestMethod]
        public void ValidSaveMatrixToFile()
        {
            DataService ds = new DataService();

            // Créer une matrice de test
            int[,] testMatrix = new int[,]
            {
                { 5, 3, 2 },
                { 2, 4, 2 },
                { 6, 7, 2 }
            };

            string savePath = @"C:\temp\saved_matrix.csv";

            // Sauvegarder la matrice
            ds.SaveMatrixToFile(testMatrix, savePath);

            // Vérifier que le fichier existe
            Assert.IsTrue(File.Exists(savePath));

            // Lire le fichier et vérifier le contenu
            string[] lines = File.ReadAllLines(savePath);
            Assert.AreEqual("5;3;2", lines[0]);
            Assert.AreEqual("2;4;2", lines[1]);
            Assert.AreEqual("6;7;2", lines[2]);

            // Nettoyer
            File.Delete(savePath);
        }
    }
}
