
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
                "The quick brown fox jumps",      // Avant-dernier: "fox"
                "Hello world from C#",           // Avant-dernier: "from" 
                "This is a test line",           // Avant-dernier: "test"
                "One two three four"             // Avant-dernier: "three"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = $"fox{Environment.NewLine}from{Environment.NewLine}test{Environment.NewLine}three";

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
                "First line with words",         // Avant-dernier: "with" (index 2)
                "",                              // Ignoré
                "   ",                           // Ignoré
                "Another line here",             // Avant-dernier: "line" (index 1)
                "\t\t",                          // Ignoré
                "Last line of text"              // Avant-dernier: "of" (index 2)
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = $"with{Environment.NewLine}line{Environment.NewLine}of";

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
                "Hello",                         // Ignoré (1 mot)
                "Single",                        // Ignoré (1 mot)
                "Word",                          // Ignoré (1 mot)
                "Only one word per line"         // 5 mots, avant-dernier: "per" (index 3)
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = "per";  // CORRECTION: Avant-dernier mot de "Only one word per line"

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
                "Word1\tWord2\tWord3\tWord4",    // Avant-dernier: "Word3"
                "A  B  C  D  E",                 // Avant-dernier: "D"
                "One\ttwo\tthree\tfour\tfive"    // Avant-dernier: "four"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = $"Word3{Environment.NewLine}D{Environment.NewLine}four";

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
                "Hello World",                   // 2 mots, avant-dernier: "Hello" (index 0)
                "C# Programming",                // Avant-dernier: "C#"
                "Test Line",                     // Avant-dernier: "Test"
                "Last Example"                   // Avant-dernier: "Last"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            string expected = $"Hello{Environment.NewLine}C#{Environment.NewLine}Test{Environment.NewLine}Last";

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
                "  First  line  with  spaces  ",  // Avant-dernier: "with"
                "Second\tline\twith\ttabs\t",     // Avant-dernier: "with"
                "  Third   line   mixed   spaces\tand\ttabs  "  // Avant-dernier: "and"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // CORRECTION: Avec la règle "avant-dernier mot"
            string expected = $"with{Environment.NewLine}with{Environment.NewLine}and";

            // Act
            string result = dataService.CollectTextFromFile(_testFilePath);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}