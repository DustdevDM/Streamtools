namespace OutBot.Classes.Exceptions
{
    /// <summary>
    /// <see cref="Exception"/> that is thrown when a configuration value is missing
    /// or containing invalid or illegal data
    /// </summary>
    internal class OutBotException : Exception
    {
        /// <inheritdoc cref="Exception"/>
        internal OutBotException(string message)
            : base(message)
        {
        }

        /// <inheritdoc cref="Exception"/>
        internal OutBotException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
