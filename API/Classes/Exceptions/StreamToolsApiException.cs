namespace StreamTools_API.Classes.Exceptions;

/// <summary>
/// Parent <see cref="Exception"/> of all exceptiontypes owned by StreamToolsAPI
/// </summary>
public abstract class StreamToolsApiException : Exception
{
  /// <inheritdoc cref="Exception"/>
  internal StreamToolsApiException(string message)
    : base(message)
  {
  }

  /// <inheritdoc cref="Exception"/>
  internal StreamToolsApiException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
