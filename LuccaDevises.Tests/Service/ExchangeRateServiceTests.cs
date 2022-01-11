using System.Collections;
using System.Collections.Generic;
using LuccaDevises.Model;
using LuccaDevises.Service;
using NUnit.Framework;

namespace LuccaDevises.Tests.Service
{
    public class ExchangeRateServiceTests
    {
        private IExchangeRateService _exchangeRateService;

        [SetUp]
        public void Setup()
        {
            _exchangeRateService = new ExchangeRateService();
        }

        [Test]
        public void ExchangeRateService_Should_GenerateExchangeRateList()
        {
            // Arrange
            List<ExchangeRate> exchangeRates = null;
            List<ExchangeRate> expectedExchangeRates = new List<ExchangeRate>()
            {
                new ExchangeRate("AUD", "CHF", 0.9661m),
                new ExchangeRate("JPY", "KRW", 13.1151m),
                new ExchangeRate("EUR", "CHF", 1.2053m),
                new ExchangeRate("AUD", "JPY", 86.0305m),
                new ExchangeRate("EUR", "USD", 1.2989m),
                new ExchangeRate("JPY", "INR", 0.6571m)
            };

            List<string> exchangeRatesLines = new List<string>()
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
            Assert.DoesNotThrow(() => exchangeRates = _exchangeRateService.GetExchangeRatesFromFileData(exchangeRatesLines));

            CollectionAssert.AreEqual(expectedExchangeRates, exchangeRates, new ExchangeRateComparer());
        }

        public class ExchangeRateComparer : IComparer, IComparer<ExchangeRate>
        {
            public int Compare(ExchangeRate x, ExchangeRate y)
            {
                if (x.SourceCurrency == y.SourceCurrency
                    && x.TargetCurrency == y.TargetCurrency
                    && x.ExchangeRateValue == y.ExchangeRateValue)
                    return 0;

                return 1;
            }

            public int Compare(object? x, object? y)
            {
                x = (ExchangeRate)x;
                y = (ExchangeRate)y;

                return Compare(x, y);
            }
        }
    }
}
