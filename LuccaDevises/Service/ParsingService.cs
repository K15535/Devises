using LuccaDevises.Exception;

namespace LuccaDevises.Service
{
    public class ParsingService : IParsingService
    {
        private List<string> _exchangeRatesLines { get; set; }
        private string _sourceCurrency { get; set; }
        private string _targetCurrency { get; set; }
        private int _amountToConvert { get; set; }

        /// <summary>
        /// Load and parse the file content.
        /// </summary>
        /// <param name="filepath">The file to parse path</param>
        public void Parse(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException();

            List<string> fileLines = File.ReadLines(filepath).ToList();

            if (fileLines.Count <= 2)
                throw new FileMissingLinesException();

            string[] conversionGoalDataArray = fileLines[0].Split(';');
            // TODO : check if conversionGoalDataArray length == 3

            // TODO : check if conversionGoalDataArray[0] && conversionGoalDataArray[2] are LLL
            _sourceCurrency = conversionGoalDataArray[0];
            _targetCurrency = conversionGoalDataArray[2];

            if (int.TryParse(conversionGoalDataArray[1], out int amountToConvert))
            {
                _amountToConvert = amountToConvert;
            }
            else
            {
                // throw
            }

            if (!int.TryParse(fileLines[1], out int exchangeRatesQuantity))
            {
                // throw
            }

            _exchangeRatesLines = fileLines.Skip(2).ToList();

            if (_exchangeRatesLines.Count != exchangeRatesQuantity)
            {
                // throw
            }
        }

        /// <summary>
        /// Fill the data passed as parameter from the parsed file data.
        /// </summary>
        /// <param name="sourceCurrency">The source currency to get</param>
        /// <param name="targetCurrency">the target currency to get</param>
        /// <param name="amountToConvert">The amount of money to get</param>
        public void FillConversionGoalData(out string sourceCurrency, out string targetCurrency, out int amountToConvert)
        {
            sourceCurrency = _sourceCurrency;
            targetCurrency = _targetCurrency;
            amountToConvert = _amountToConvert;
        }

        /// <summary>
        /// Get the exchange rates lines from the parsed file data.
        /// </summary>
        /// <returns>The exchange rates lines</returns>
        public List<string> GetExchangeRatesLines()
        {
            return _exchangeRatesLines;
        }
    }
}
