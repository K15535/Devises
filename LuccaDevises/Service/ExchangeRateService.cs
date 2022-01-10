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
        /// <param name="exchangeRatesLines"></param>
        /// <returns>A list of <see cref="ExchangeRate"/></returns>
        public List<ExchangeRate> GetExchangeRatesFromFileData(List<string> exchangeRatesLines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            foreach (string exchangeRateLine in exchangeRatesLines)
            {
                string[] exchangeRateData = exchangeRateLine.Split(';');

                if (!IsExchangeRateFormatCorrect(exchangeRateData))
                    throw new IncorrectExchangeRateDataFormatException(message: $"Incorrect exchange rate format for {exchangeRateLine}");

                exchangeRates.Add(new ExchangeRate(exchangeRateData[0], exchangeRateData[1], Decimal.Parse(exchangeRateData[2], CultureInfo.InvariantCulture)));
            }

            return exchangeRates;
        }

        /// <summary>
        /// Check if the given exchange rate line is formatted like LLL;LLL;N.NNNN
        /// </summary>
        /// <param name="exchangeRateData">An array containing the exchange rate line data</param>
        /// <exception cref="IncorrectExchangeRateDataFormatException">If the exchange rate is not well formatted</exception>
        private static bool IsExchangeRateFormatCorrect(string[] exchangeRateData)
        {
            return exchangeRateData.Length == 3
                && Regex.IsMatch(exchangeRateData[0], @"\b[a-zA-Z]{3}\b")
                && Regex.IsMatch(exchangeRateData[1], @"\b[a-zA-Z]{3}\b")
                && decimal.TryParse(exchangeRateData[2], NumberStyles.Float, CultureInfo.InvariantCulture, out decimal value)
                && ExchangeRateNumberIsWellFormatted(exchangeRateData[2]);
        }

        /// <summary>
        /// Check the exchange rate format : N.NNNN
        /// </summary>
        /// <param name="exchangeRate">The exchange rate value extracted from the line</param>
        /// <returns>True if well formatted</returns>
        private static bool ExchangeRateNumberIsWellFormatted(string exchangeRate)
        {
            if (!exchangeRate.Contains('.'))
            {
                return false;
            }
            else
            {
                string[] splittedDecimal = exchangeRate.Split('.');

                if (splittedDecimal.Length != 2 || splittedDecimal[1].Length != 4)
                    return false;
            }

            return true;
        }
    }
}
