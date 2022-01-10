using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Model
{
    public class ExchangeRate
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal ExchangeRateValue { get; set; }

        public ExchangeRate(string sourceCurrency, string targetCurrency, decimal exchangeRateValue)
        {
            this.SourceCurrency = sourceCurrency;
            this.TargetCurrency = targetCurrency;
            this.ExchangeRateValue = exchangeRateValue;
        }
    }
}
