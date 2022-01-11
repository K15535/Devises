using System.Collections.Generic;
using LuccaDevises.Model;
using LuccaDevises.Service;
using NUnit.Framework;

namespace LuccaDevises.Tests.Service
{
    public class PathServiceTests
    {
        private IPathService _pathService;

        [SetUp]
        public void Setup()
        {
            _pathService = new PathService();
        }

        [Test]
        public void PathService_Should_FindShortestPath()
        {
            // Arrange
            Stack<decimal>? shortestPath = null;
            string sourceCurrency = "EUR";
            string targetCurrency = "JPY";
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>()
            {
                new ExchangeRate("AUD", "CHF", 0.9661m),
                new ExchangeRate("JPY", "KRW", 13.1151m),
                new ExchangeRate("EUR", "CHF", 1.2053m),
                new ExchangeRate("AUD", "JPY", 86.0305m),
                new ExchangeRate("EUR", "USD", 1.2989m),
                new ExchangeRate("JPY", "INR", 0.6571m)
            };

            Stack<decimal> expectedShortestPath = new Stack<decimal>();
            expectedShortestPath.Push(86.0305m);
            expectedShortestPath.Push(1.0351m);
            expectedShortestPath.Push(1.2053m);

            _pathService.Initialize(sourceCurrency, targetCurrency, exchangeRates);
            
            // Act
            // Assert
            Assert.DoesNotThrow(() => shortestPath = _pathService.GetShortestPath());
            Assert.AreEqual(expectedShortestPath, shortestPath);
        }
    }
}
