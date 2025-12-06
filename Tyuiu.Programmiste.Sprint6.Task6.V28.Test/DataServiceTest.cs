
using Tyuiu.Programmiste.Sprint6.Task6.V28.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task6.V28.Test
{
    [TestClass]
    public sealed class DataServiceTest
    {
        private string _testFilePath;

        [TestInitialize]
        public void TestInitialize()
        {
            _testFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void TestCollectTextFromFile_NormalCase()
        {
            // Arrange
            string[] testData = {
                "The quick brown fox jumps",
                "Hello world from C#",
                "This is a test line",
                "One two three four"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Maintenant les mots sont dans le même groupe, séparés par des espaces
            string expected = "fox from test three";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_WithEmptyLineGroups()
        {
            // Arrange - Simule le fichier de test officiel
            string[] testData = {
                "abc wFriYazZtHl def",
                "",
                "ghi eAezSXF jkl",
                "mno eqSGAic pqr",
                "stu wUdVEuNB vwx"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "wFriYazZtHl | eAezSXF eqSGAic wUdVEuNB";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_EmptyLines()
        {
            // Arrange
            string[] testData = {
                "First line with words",
                "",
                "   ",
                "Another line here",
                "\t\t",
                "Last line of text"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Avec la nouvelle logique de groupes
            string expected = "with | line of";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_LinesWithSingleWord()
        {
            // Arrange
            string[] testData = {
                "Hello",
                "Single",
                "Word",
                "Only one word per line"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "per";  // Avant-dernier mot de la seule ligne valide

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_MixedSpacesAndTabs()
        {
            // Arrange
            string[] testData = {
                "Word1\tWord2\tWord3\tWord4",
                "A  B  C  D  E",
                "One\ttwo\tthree\tfour\tfive"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "Word3 D four";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestCollectTextFromFile_FileNotFound()
        {
            // Arrange
            DataService dataService = new DataService();
            string nonExistentFile = Path.Combine(Path.GetTempPath(), $"NonExistentFile_{Guid.NewGuid()}.txt");

            // Act
            string result = dataService.CollectTextFromFile(nonExistentFile);
        }

        [TestMethod]
        public void TestCollectTextFromFile_AllLinesWithTwoWords()
        {
            // Arrange
            string[] testData = {
                "Hello World",
                "C# Programming",
                "Test Line",
                "Last Example"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "Hello C# Test Last";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_EmptyFile()
        {
            // Arrange
            File.WriteAllText(_testFilePath, string.Empty);

            DataService dataService = new DataService();

            string expected = string.Empty;

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_OnlyEmptyLines()
        {
            // Arrange
            string[] testData = {
                "",
                "   ",
                "\t",
                "\r\n"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = string.Empty;

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_TrailingSpaces()
        {
            // Arrange
            string[] testData = {
                "  First  line  with  spaces  ",
                "Second\tline\twith\ttabs\t",
                "  Third   line   mixed   spaces\tand\ttabs  "
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "with with and";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCollectTextFromFile_MultipleGroups()
        {
            // Arrange
            string[] testData = {
                "Group1 word1 extra",
                "Group1 word2 extra",
                "",
                "Group2 word3 extra",
                "Group2 word4 extra",
                "Group2 word5 extra",
                "",
                "Group3 word6 extra"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "word1 word2 | word3 word4 word5 | word6";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}