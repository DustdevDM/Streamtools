namespace Core.BusinessLogic.DTOs.Settings;

/// <summary>
/// Config DTO used to parse appsettings values for stat ink settings
/// </summary>
public class StatInkSettingsDto
{
  /// <summary>
  /// Stat.ink username that will be the query goal to fetch data
  /// </summary>
  public required string StatisticsUsername { get; init; }
}
