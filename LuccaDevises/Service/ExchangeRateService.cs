using LuccaDevises.Model;
using LuccaDevises.Exception;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LuccaDevises.Service
{
    public class ExchangeRateService : IExchangeRateService
    {
        /// <summary>
        /// Generate a list of <see cref="ExchangeRate"/> from the lines extracted from the input file.
        /// </summary>
        /// <param name="exchangeRatesLines">Exchange rate line extracted from the file, format LLL;LLL;N.NNNN</param>
        /// <returns>A list of <see cref="ExchangeRate"/>A list of object describing an exchange rate</returns>
        public List<ExchangeRate> GetExchangeRatesFromFileData(List<string> exchangeRatesLines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            foreach (string exchangeRateLine in exchangeRatesLines)
            {
                string[] exchangeRateData = exchangeRateLine.Split(';');

                if (!Regex.IsMatch(exchangeRateLine, @"\b[A-Z]{3};[A-Z]{3};[0-9]+\.[0-9]{4}\b"))
                    throw new DataFormatException(message: $"Incorrect exchange rate format : {exchangeRateLine}");

                if (!decimal.TryParse(exchangeRateData[2], NumberStyles.Float, CultureInfo.InvariantCulture, out decimal exchangeRateValue))
                    throw new DataFormatException(message: $"Incorrect exchange rate value : {exchangeRateLine}");

                exchangeRates.Add(new ExchangeRate(exchangeRateData[0], exchangeRateData[1], exchangeRateValue));
            }

            return exchangeRates;
        }
    }
}
