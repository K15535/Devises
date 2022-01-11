using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Exception
{
    public class DataFormatException : System.Exception
    {
        public DataFormatException(string message) : base(message) { }
    }
}
