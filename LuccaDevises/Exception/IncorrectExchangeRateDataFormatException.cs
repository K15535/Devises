using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Exception
{
    public class IncorrectExchangeRateDataFormatException : System.Exception
    {
        public IncorrectExchangeRateDataFormatException(string message) : base(message) { }
    }
}
