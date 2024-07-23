using System.Runtime.Serialization;

namespace Core.BusinessLogic.Enums;

public enum StatInkQueryResult
{
  Any,

  [EnumMember(Value = "win")]
  Victory,

  [EnumMember(Value = "lose")]
  Defeat,

  [EnumMember(Value = "exempted_lose")]
  DefeatExempted,

  [EnumMember(Value = "draw")]
  Draw,

  [EnumMember(Value = "~not_win")]
  NotWinning,

  [EnumMember(Value = "~win_lose")]
  VictoryOrDefeat,

  [EnumMember(Value = "~lose")]
  ConsiderToBeDefeated,

  [EnumMember(Value = "~not_draw")]
  NotDraws,

  [EnumMember(Value = "~unknown")]
  UnknownResult

}
