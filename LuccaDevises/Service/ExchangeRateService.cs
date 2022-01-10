using LuccaDevises.Model;
using System.Globalization;

namespace LuccaDevises.Service
{
    public class ExchangeRateService : IExchangeRateService
    {
        /// <summary>
        /// Generate a list of <see cref="ExchangeRate"/> from the lines extracted from the input file.
        /// </summary>
        /// <param name="exchangeRatesLines"></param>
        /// <returns>A list of <see cref="ExchangeRate"/></returns>
        public List<ExchangeRate> GetExchangeRatesFromFileData(List<string> exchangeRatesLines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            foreach (string exchangeRateLine in exchangeRatesLines)
            {
                string[] conversionData = exchangeRateLine.Split(';');
                // TODO : Check length == 3
                // TODO : Check format LLL;LLL;N.4
                // Throw exceptions

                exchangeRates.Add(new ExchangeRate(conversionData[0], conversionData[1], Decimal.Parse(conversionData[2], CultureInfo.InvariantCulture)));
            }

            return exchangeRates;
        }
    }
}
