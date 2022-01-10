using LuccaDevises.Model;

namespace LuccaDevises.Service
{
    public interface IPathService
    {
        /// <summary>
        /// Initialize the <see cref="PathService"/>.
        /// </summary>
        /// <param name="sourceCurrency">The currency to convert</param>
        /// <param name="targetCurrency">The targeted currency</param>
        /// <param name="exchangeRates">A list of <see cref="ExchangeRate"/></param>
        void Initialize(string sourceCurrency, string targetCurrency, List<ExchangeRate> exchangeRates);

        /// <summary>
        /// Get the shortest exchange rates path to convert the <see cref="_sourceCurrency"/> into the <see cref="_targetCurrency"/> as a Stack of decimal.
        /// </summary>
        /// <returns>A stack of decimal values, each decimal value being an exchange rate. Null if no path found.</returns>
        Stack<decimal>? GetShortestPath();
    }
}