namespace OutBot.Classes.Exceptions
{
    /// <summary>
    /// <see cref="Exception"/> that is thrown when a configuration value is missing
    /// or containing invalid or illegal data
    /// </summary>
    internal class ConfigurationException : OutBotException
    {
        /// <inheritdoc cref="Exception"/>
        internal ConfigurationException(string message)
            : base(message)
        {
        }

        /// <inheritdoc cref="Exception"/>
        internal ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}