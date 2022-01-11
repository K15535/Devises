using System.Collections.Generic;
using LuccaDevises.Service;
using NUnit.Framework;

namespace LuccaDevises.Tests.Service
{
    public class ParsingServiceTests
    {
        private readonly string CORRECT_FILE_PATH = $"{TestContext.CurrentContext.TestDirectory}\\..\\..\\..\\TestFiles\\correct-file-one-path.txt";
        private IParsingService _parsingService;

        [SetUp]
        public void Setup()
        {
            _parsingService = new ParsingService();
        }

        [Test]
        public void ParsingService_Should_ParseCorrectFile()
        {
            // Arrange
            string filePath = CORRECT_FILE_PATH;
            List<string> expectedExchangeRatesLines = new List<string>()
            {
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            // Act
            // Assert
            Assert.DoesNotThrow(() => _parsingService.Parse(filePath));

            _parsingService.FillConversionGoalData(out string sourceCurrency, out string targetCurrency, out int amountToConvert);
            List<string> exchangeRatesLines = _parsingService.GetExchangeRatesLines();

            Assert.AreEqual("EUR", sourceCurrency);
            Assert.AreEqual("JPY", targetCurrency, "JPY");
            Assert.AreEqual(550, amountToConvert);
            Assert.AreEqual(expectedExchangeRatesLines, exchangeRatesLines);
        }
    }
}
