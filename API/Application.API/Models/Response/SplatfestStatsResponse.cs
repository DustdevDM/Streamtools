namespace Application.API.Models.Response;

/// <summary>
/// DTO that represents splatfest related data pulled from splatfest
/// </summary>
public class SplatfestStatsResponse(string teamName, string teamColor, int collectedCloudOpen, int collectedCloudPro)
{
  /// <summary>
  /// The name of the voted splatfest team
  /// </summary>
  public string TeamName { get; set; } = teamName;

  /// <summary>
  /// The color of the voted splatfest team
  /// </summary>
  public string TeamColor { get; set; } = teamColor;

  /// <summary>
  /// Collected cloud value during this splatfest in open mode
  /// </summary>
  public int CollectedCloudOpen { get; set; } = collectedCloudOpen;

  /// <summary>
  /// Collected cloud value during this splatfest in challenge mode
  /// </summary>
  public int CollectedCloudPro { get; set; } = collectedCloudPro;
}
