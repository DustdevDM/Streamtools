using Core.BusinessLogic.Builder;

namespace Core.BusinessLogic.Services;

/// <summary>
/// Service to handle communication with the statink api
/// </summary>
public interface IStatInkService
{
  /// <summary>
  /// Calculates the win to lose ratio by querying battle log data
  /// </summary>
  public Task<(double, double, double)> CalculateWinLooseRate(
    IStatInkQueryBuilder statInkQueryBuilder,
    bool ignoreDisconnects);
}
