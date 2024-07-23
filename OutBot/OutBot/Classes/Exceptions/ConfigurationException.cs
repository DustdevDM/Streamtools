namespace OutBot.Classes.Exceptions
{
    /// <summary>
    /// <see cref="Exception"/> that is thrown when a configuration value is missing
    /// or containing invalid or illegal data
    /// </summary>
    public class ConfigurationException : OutBotException
    {
        /// <inheritdoc cref="Exception"/>
        public ConfigurationException(string message)
            : base(message)
        {
        }

        /// <inheritdoc cref="Exception"/>
        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
