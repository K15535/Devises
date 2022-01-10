// See https://aka.ms/new-console-template for more information

using LuccaDevises.Builder;
using LuccaDevises.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO : Check args


            IEnumerable<string> fileContent = File.ReadLines(args[0]);

            string[] conversionGoalData = fileContent.ElementAt(0).Split(';');
            string sourceCurrencyCode = conversionGoalData[0];
            string targetCurrencyCode = conversionGoalData[2];
            int initialAmount = int.Parse(conversionGoalData[1]);
            int exchangeRatesQuantity = int.Parse(fileContent.ElementAt(1));

            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            List<string> currenciesConversions = fileContent.Skip(2).ToList();
            foreach (string currenciesConversion in currenciesConversions)
            {
                string[] conversionData = currenciesConversion.Split(';');
                exchangeRates.Add(new ExchangeRate
                {
                    sourceCurrency = conversionData[0],
                    targetCurrency = conversionData[1],
                    exchangeRateValue = Decimal.Parse(conversionData[2], CultureInfo.InvariantCulture)
                });
            }

            PathsBuilder pathsBuilder = new PathsBuilder(sourceCurrencyCode, targetCurrencyCode, exchangeRates);
            List<Stack<decimal>> possiblePaths = pathsBuilder.GetPaths();

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
