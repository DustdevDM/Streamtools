namespace Core.BusinessLogic.Exceptions;

/// <summary>
/// Parent <see cref="Exception"/> of all exception types owned by StreamToolsAPI
/// </summary>
public class StreamToolsApiException : Exception
{
  /// <inheritdoc cref="Exception"/>
  public StreamToolsApiException(string message)
    : base(message)
  {
  }

  /// <inheritdoc cref="Exception"/>
  public StreamToolsApiException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
