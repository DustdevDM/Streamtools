namespace StreamTools_API.Classes.Exceptions;

/// <summary>
/// Exception that is thrown when configuration values are missing or illegal
/// </summary>
public class ConfigurationException : StreamToolsApiException
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
