using LuccaDevises.Model;
using LuccaDevises.Service;

namespace LuccaDevises
{
    public class App
    {
        private readonly IPathService _pathService;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IParsingService _parsingService;

        public App(IPathService pathService,
                   IExchangeRateService exchangeRateService,
                   IParsingService parsingService)
        {
            _pathService = pathService;
            _exchangeRateService = exchangeRateService;
            _parsingService = parsingService;
        }

        public void Run(string filepath)
        {
            _parsingService.Parse(filepath);
            _parsingService.FillConversionGoalData(out string sourceCurrency, out string targetCurrency, out int amountToConvert);
            List<string> exchangeRatesLines = _parsingService.GetExchangeRatesLines();

            List<ExchangeRate> exchangeRates = _exchangeRateService.GetExchangeRatesFromFileData(exchangeRatesLines);

            _pathService.Initialize(sourceCurrency, targetCurrency, exchangeRates);
            Stack<decimal>? shortestPath = _pathService.GetShortestPath();

            if (shortestPath != null)
            {
                ProcessResult(amountToConvert, shortestPath);
            }
            else
            {
                // throw
            }
        }

        /// <summary>
        /// Calculate the result and write it into the console.
        /// </summary>
        /// <param name="amountToConvert"></param>
        /// <param name="shortestPath"></param>
        private static void ProcessResult(int amountToConvert, Stack<decimal> shortestPath)
        {
            decimal res = amountToConvert;

            while (shortestPath.Count > 0)
            {
                res *= shortestPath.Peek();

                shortestPath.Pop();
            }

            Console.WriteLine(Math.Round(res, 0));
        }
    }
}
