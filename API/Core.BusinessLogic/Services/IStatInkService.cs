using Core.BusinessLogic.Builder;
using Core.BusinessLogic.DTOs;

namespace Core.BusinessLogic.Services;

/// <summary>
/// Service to handle communication with the statink api
/// </summary>
public interface IStatInkService
{
  /// <summary>
  /// Returns a <see cref="List{T}"/> of match results based on the provided query builder
  /// </summary>
  public Task<List<StatInkMatchRecordsDTO>> GetStatInkMatchRecords(
    string username,
    IStatInkQueryBuilder statInkQueryBuilder,
    bool ignoreDisconnects);
}

