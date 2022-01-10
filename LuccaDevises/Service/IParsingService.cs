
namespace LuccaDevises.Service
{
    public interface IParsingService
    {
        /// <summary>
        /// Fill the data passed as parameter from the parsed file data.
        /// </summary>
        /// <param name="sourceCurrency">The source currency to get</param>
        /// <param name="targetCurrency">the target currency to get</param>
        /// <param name="amountToConvert">The amount of money to get</param>
        void FillConversionGoalData(out string sourceCurrency, out string targetCurrency, out int amountToConvert);

        /// <summary>
        /// Get the exchange rates lines from the parsed file data.
        /// </summary>
        /// <returns>The exchange rates lines</returns>
        List<string> GetExchangeRatesLines();

        /// <summary>
        /// Load and parse the file content.
        /// </summary>
        /// <param name="filepath">The file to parse path</param>
        void Parse(string filepath);
    }
}