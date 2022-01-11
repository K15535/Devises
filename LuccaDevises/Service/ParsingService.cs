using LuccaDevises.Exception;
using System.Text.RegularExpressions;

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
                throw new DataFormatException(message: $"Incorrect amount of lines in file : {fileLines.Count} < 3");

            if (!Regex.IsMatch(fileLines[0], @"\b[A-Z]{3};[0-9]+;[A-Z]{3}\b"))
                throw new DataFormatException(message: $"Incorrect conversion goal format : {fileLines[0]}");

            string[] conversionGoalDataArray = fileLines[0].Split(';');

            _sourceCurrency = conversionGoalDataArray[0];
            _targetCurrency = conversionGoalDataArray[2];
            if (int.TryParse(conversionGoalDataArray[1], out int amountToConvert))
                _amountToConvert = amountToConvert;
            else
                throw new DataFormatException(message: $"Incorrect amount to convert format : {amountToConvert}");

            if (!int.TryParse(fileLines[1], out int exchangeRatesQuantity))
                throw new DataFormatException(message: $"Incorrect amount of exchange rates format : {fileLines[1]}");

            _exchangeRatesLines = fileLines.Skip(2).ToList();
            if (_exchangeRatesLines.Count != exchangeRatesQuantity)
                throw new DataFormatException(message: $"Mismatch between amount of exchange rates [{exchangeRatesQuantity}] and exchange rates lines [{_exchangeRatesLines.Count}]");
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
