using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutBot.Classes.Exceptions
{
    public class OutBotException : Exception
    {
        public OutBotException(string message) : base(message)
        {}

        public OutBotException(string message, Exception innerException) : base(message, innerException)
        {}
    }
}
