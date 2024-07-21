namespace StreamTools_API.Classes.Configuration;

/// <summary>
/// Config DTO used to parse appsettings values for cors settings
/// </summary>
public class CorsSettings
{
  /// <summary>
  /// Allowed origin Values for Access-Control-Allow-Origin header
  /// </summary>
  public required string[] AllowedOrigins { get; init; }

  /// <summary>
  /// Allow methods values for Access-Control-Allow-Methods header
  /// </summary>
  public required string[] AllowedMethods { get; init; }

  /// <summary>
  /// Allow headers values for Access-Control-Allow-Headers header
  /// </summary>
  public required string[] AllowedHeaders { get; init; }
}
