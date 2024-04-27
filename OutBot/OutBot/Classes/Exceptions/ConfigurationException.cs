using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutBot.Classes.Exceptions
{
    public class ConfigurationException : OutBotException
    {
        public ConfigurationException(string message) : base(message)
        { }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}