using Core.BusinessLogic.Enums;

namespace Core.BusinessLogic.Builder;

public interface IStatInkQueryBuilder
{
  public Dictionary<string, string?> Build();

  public IStatInkQueryBuilder FullQuery();

  public IStatInkQueryBuilder WithLobbyFilter(StatInkQueryLobby statInkQueryLobby);

  public IStatInkQueryBuilder WithModeFilter(StatInkQueryMode statInkQueryMode);

  public IStatInkQueryBuilder WithResultFilter(StatInkQueryResult statInkQueryResult);

  public IStatInkQueryBuilder WithMatchEndingReasonFilter(StatInkQueryMatchEndingReason statInkQueryMatchEndingReason);

  public IStatInkQueryBuilder FromDate(DateTimeOffset dateTime);

  public IStatInkQueryBuilder TillDate(DateTimeOffset dateTime);
}
