namespace Core.BusinessLogic.Exceptions;

public class StatInkUserNotFoundException : StatInkException
{
  /// <inheritdoc cref="Exception"/>
  public StatInkUserNotFoundException(string message) : base(message)
  {
  }

  /// <inheritdoc cref="Exception"/>
  public StatInkUserNotFoundException(string message, Exception innerException) : base(message, innerException)
  {
  }
}
