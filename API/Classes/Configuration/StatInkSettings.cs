namespace StreamTools_API.Classes.Configuration;

/// <summary>
/// Config DTO used to parse appsettings values for stat ink settings
/// </summary>
public class StatInkSettings
{
  /// <summary>
  /// API Token used to query the stat ink API
  /// </summary>
  public required string ApiToken { get; init; }

  /// <summary>
  /// Stat.ink username that will be the query goal to fetch data
  /// </summary>
  public required string StatisticsUsername { get; init; }
}
