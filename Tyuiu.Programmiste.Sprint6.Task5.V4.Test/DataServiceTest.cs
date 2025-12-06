
using System.Globalization;
using Tyuiu.Programmiste.Sprint6.Task5.V4.Lib;
namespace Tyuiu.Programmiste.Sprint6.Task5.V4.Test
{
    [TestClass]
    public class DataServiceTest
    {
        private string _testFilePath;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set invariant culture for consistent number parsing in tests
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // Create a temporary test file for each test
            _testFilePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up test file after each test
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void TestLoadFromDataFile_NormalCase()
        {
            // Arrange
            string[] testData = {
                "12.5",
                "25",
                "37.8",
                "42.1234",
                "55"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(12.5, result[0], 0.001);
            Assert.AreEqual(25, result[1], 0.001);
            Assert.AreEqual(37.8, result[2], 0.001);
            Assert.AreEqual(42.123, result[3], 0.001); // Rounded to 3 decimals
            Assert.AreEqual(55, result[4], 0.001);
        }

        [TestMethod]
        public void TestLoadFromDataFile_Rounding()
        {
            // Arrange
            string[] testData = {
                "1.2345",    // Should round to 1.235 (AwayFromZero)
                "2.3456",    // Should round to 2.346 (AwayFromZero)
                "3.4567"     // Should round to 3.457 (AwayFromZero)
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert - using AwayFromZero rounding
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1.235, result[0], 0.0001); // With AwayFromZero
            Assert.AreEqual(2.346, result[1], 0.0001);
            Assert.AreEqual(3.457, result[2], 0.0001);
        }

        [TestMethod]
        public void TestLoadFromDataFile_WithEmptyLines()
        {
            // Arrange
            string[] testData = {
                "10.5",
                "",
                "20.75",
                "   ",
                "30.123"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(3, result.Length); // Only 3 valid numbers
            Assert.AreEqual(10.5, result[0], 0.001);
            Assert.AreEqual(20.75, result[1], 0.001);
            Assert.AreEqual(30.123, result[2], 0.001);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestLoadFromDataFile_FileNotFound()
        {
            // Arrange
            DataService dataService = new DataService();
            string nonExistentFile = Path.Combine(Path.GetTempPath(), "NonExistentFile_" + Guid.NewGuid() + ".txt");

            // Act
            double[] result = dataService.LoadFromDataFile(nonExistentFile);
        }

        [TestMethod]
        public void TestLoadFromDataFile_InvalidFormat()
        {
            // Arrange
            string[] testData = {
                "10.5",
                "abc",  // Invalid number
                "20.75"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act & Assert
            try
            {
                double[] result = dataService.LoadFromDataFile(_testFilePath);
                Assert.Fail("Expected FormatException was not thrown");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid number format"));
            }
        }

        [TestMethod]
        public void TestLoadFromDataFile_IntegerValues()
        {
            // Arrange
            string[] testData = {
                "100",
                "200",
                "300"
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(100, result[0], 0.001);
            Assert.AreEqual(200, result[1], 0.001);
            Assert.AreEqual(300, result[2], 0.001);
        }

        [TestMethod]
        public void TestLoadFromDataFile_MixedValues()
        {
            // Arrange
            string[] testData = {
                "123.456789",  // Will be rounded to 123.457 (AwayFromZero)
                "987.654321",  // Will be rounded to 987.654 (AwayFromZero)
                "555.555555"   // Will be rounded to 555.556 (AwayFromZero)
            };
            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(123.457, result[0], 0.0001);
            Assert.AreEqual(987.654, result[1], 0.0001);
            Assert.AreEqual(555.556, result[2], 0.0001);
        }

        [TestMethod]
        public void TestLoadFromDataFile_DifferentDecimalFormats()
        {
            // Test with different decimal separators
            string[] testData = {
                "123,456",      // Comma as decimal separator
                "789.123",      // Dot as decimal separator
                "1000.50",      // Without spaces - should work
                "2,000.75"      // Comma as thousand separator, dot as decimal
            };

            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(123.456, result[0], 0.001);
            Assert.AreEqual(789.123, result[1], 0.001);
            Assert.AreEqual(1000.50, result[2], 0.001);
            Assert.AreEqual(2000.75, result[3], 0.001);
        }

        [TestMethod]
        public void TestLoadFromDataFile_SpaceAsThousandSeparator()
        {
            // Test with space as thousand separator (like "1 000.50")
            string[] testData = {
                "1 000.50",     // Space as thousand separator
                "2 500.75",     // Space as thousand separator
                "10 000.25"     // Space as thousand separator
            };

            File.WriteAllLines(_testFilePath, testData);

            DataService dataService = new DataService();

            // Act
            double[] result = dataService.LoadFromDataFile(_testFilePath);

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1000.50, result[0], 0.001);
            Assert.AreEqual(2500.75, result[1], 0.001);
            Assert.AreEqual(10000.25, result[2], 0.001);
        }
    }
}
