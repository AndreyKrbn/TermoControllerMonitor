using System;
//using System.Linq;

namespace SerialPortChating
{
    class ReadTimeOutExaption : Exception
    {
        public ReadTimeOutExaption(string message) : base(message) { }
    }
}
