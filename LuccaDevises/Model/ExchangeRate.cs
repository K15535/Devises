using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Model
{
    public class ExchangeRate
    {
        public string sourceCurrency { get; set; }
        public string targetCurrency { get; set; }
        public decimal exchangeRateValue { get; set; }
    }
}
