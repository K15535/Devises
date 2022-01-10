using LuccaDevises.Model;

namespace LuccaDevises.Service
{
    public interface IExchangeRateService
    {
        /// <summary>
        /// Generate a list of <see cref="ExchangeRate"/> from the lines extracted from the input file.
        /// </summary>
        /// <param name="exchangeRatesLines"></param>
        /// <returns>A list of <see cref="ExchangeRate"/></returns>
        List<ExchangeRate> GetExchangeRatesFromFileData(List<string> exchangeRatesLines);
    }
}