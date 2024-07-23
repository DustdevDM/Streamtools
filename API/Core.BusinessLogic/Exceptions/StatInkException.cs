namespace Core.BusinessLogic.Exceptions;

public class StatInkException : StreamToolsApiException
{
  /// <inheritdoc cref="Exception"/>
  public StatInkException(string message) : base(message)
  {
  }

  /// <inheritdoc cref="Exception"/>
  public StatInkException(string message, Exception innerException) : base(message, innerException)
  {
  }
}
