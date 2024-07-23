using Core.BusinessLogic.Enums;

namespace Core.BusinessLogic.Builder;

public class StatInkQueryBuilder : IStatInkQueryBuilder
{
  private readonly Dictionary<string, string?> queryParameter = new();

  public Dictionary<string, string?> Build()
  {
    if (this.queryParameter.Any(x => x.Key.Contains("term_")))
      this.queryParameter.Add("f[term]", "term");

    return queryParameter;
  }

  public IStatInkQueryBuilder FullQuery()
  {
    this.queryParameter.Add("full", "1");
    return this;
  }
  public IStatInkQueryBuilder WithLobbyFilter(StatInkQueryLobby statInkQueryLobby)
  {
    this.queryParameter.Add("f[lobby]", this.ConvertLobby(statInkQueryLobby));
    return this;
  }

  public IStatInkQueryBuilder WithModeFilter(StatInkQueryMode statInkQueryMode)
  {
    this.queryParameter.Add("f[rule]", ConvertMode(statInkQueryMode));
    return this;
  }

  public IStatInkQueryBuilder WithResultFilter(StatInkQueryResult statInkQueryResult)
  {
    this.queryParameter.Add("f[result]", this.ConvertResult(statInkQueryResult));
    return this;
  }

  public IStatInkQueryBuilder FromDate(DateTimeOffset dateTime)
  {
    this.queryParameter.Add("f[term_from]", $"@{dateTime.ToUnixTimeSeconds().ToString()}");
    return this;
  }

  public IStatInkQueryBuilder TillDate(DateTimeOffset dateTime)
  {
    this.queryParameter.Add("f[term_to]", $"@{dateTime.ToUnixTimeSeconds().ToString()}");
    return this;
  }

  public IStatInkQueryBuilder WithMatchEndingReasonFilter(StatInkQueryMatchEndingReason statInkQueryMatchEndingReason)
  {
    this.queryParameter.Add("f[knockout]", this.ConvertMatchEndingReason(statInkQueryMatchEndingReason));
    return this;
  }

  private string ConvertLobby(StatInkQueryLobby statInkQueryLobby)
  {
    switch (statInkQueryLobby)
    {
      case StatInkQueryLobby.ExceptPrivate:
        return "!private";
      case StatInkQueryLobby.RegularBattle:
        return "regular";
      case StatInkQueryLobby.AnarchyBattleAny:
        return "@bankara";
      case StatInkQueryLobby.AnarchyBattleOpen:
        return "bankara_open";
      case StatInkQueryLobby.AnarchyBattleSeries:
        return "bankara_challenge";
      case StatInkQueryLobby.XBattle:
        return "xmatch";
      case StatInkQueryLobby.Challenge:
        return "event";
      case StatInkQueryLobby.SplatfestAny:
        return "@splatfest";
      case StatInkQueryLobby.SplatfestOpen:
        return "splatfest_open";
      case StatInkQueryLobby.SplatfestPro:
        return "splatfest_challenge";
      case StatInkQueryLobby.PrivateBattle:
        return "private";
      case  StatInkQueryLobby.Any:
      default:
        return string.Empty;
    }
  }

  private string ConvertMode(StatInkQueryMode statInkQueryMode)
  {
    switch (statInkQueryMode)
    {
      case StatInkQueryMode.TurfWar:
        return "nawabari";
      case StatInkQueryMode.AnyRanked:
        return "@gachi";
      case StatInkQueryMode.SplatZones:
        return "area";
      case StatInkQueryMode.TowerControl:
        return "yagura";
      case StatInkQueryMode.Rainmaker:
        return "hoko";
      case StatInkQueryMode.CalmBlitz:
        return "asari";
      case StatInkQueryMode.TriColorTurfWar:
        return "tricolor";
      case StatInkQueryMode.Any:
      default:
        return string.Empty;
    }
  }

  private string ConvertResult(StatInkQueryResult statInkQueryResult)
  {
    switch (statInkQueryResult)
    {
      case StatInkQueryResult.Victory:
        return "win";
      case StatInkQueryResult.Defeat:
        return "lose";
      case StatInkQueryResult.DefeatExempted:
        return "exempted_lose";
      case StatInkQueryResult.Draw:
        return "draw";
      case StatInkQueryResult.NotWinning:
        return "~not_win";
      case StatInkQueryResult.VictoryOrDefeat:
        return "~win_lose";
      case StatInkQueryResult.ConsiderToBeDefeated:
        return "~lose";
      case StatInkQueryResult.NotDraws:
        return "~not_draw";
      case StatInkQueryResult.UnknownResult:
        return "~unknown";
      case StatInkQueryResult.Any:
      default:
        return string.Empty;
    }
  }

  private string ConvertMatchEndingReason(StatInkQueryMatchEndingReason statInkQueryMatchEndingReason)
  {
    switch (statInkQueryMatchEndingReason)
    {
      case StatInkQueryMatchEndingReason.Knockout:
        return "yes";
      case StatInkQueryMatchEndingReason.TimeIsUp:
        return "no";
      case StatInkQueryMatchEndingReason.Any:
      default:
        return string.Empty;
    }
  }
}
