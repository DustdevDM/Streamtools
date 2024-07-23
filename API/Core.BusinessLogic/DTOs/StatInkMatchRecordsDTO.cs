using System.Runtime.Serialization;
using Core.BusinessLogic.Enums;
using Newtonsoft.Json;

namespace Core.BusinessLogic.DTOs
{
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

    /// <summary>
    /// Interface representing TimeObjects used by the statink API
    /// </summary>
    public interface ITimeObject
    {
        /// <summary>
        /// Time in unix-seconds
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Datetime object
        /// </summary>
        public DateTime Iso8601 { get; set; }
    }

    /// <summary>
    /// Enum which represents the final result of a match
    /// </summary>
    public enum MatchResult
    {
        /// <summary>
        /// Result Type when a match has been won
        /// </summary>
        [JsonProperty("win")]
        Win,

        /// <summary>
        /// Result type when a match has been lost
        /// </summary>
        [JsonProperty("lose")]
        Lose,

        /// <summary>
        /// Result type when a match has been stopped early due to disconnects
        /// </summary>
        [JsonProperty("draw")]
        Draw,

        /// <summary>
        /// Result type when a match has been lost due to disconnects in a ranked match
        /// </summary>
        [JsonProperty("exempted_lose")]
        [EnumMember(Value = "exempted_lose")]
        ExemptedLose,
    }

    /// <summary>
    /// Response Object to parse relevant data from the stat.ink api
    /// </summary>
    public class StatInkMatchRecordsDTO
    {
        /// <summary>
        /// ID of a splatoon match data record
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Instance of <see cref="LobbyData"/>
        /// </summary>
        [JsonProperty("lobby")]
        public required LobbyData LobbyData { get; set; }

        /// <summary>
        /// Instance of <see cref="MatchResult"/>
        /// </summary>
        [JsonProperty("result")]
        public required StatInkQueryResult Result { get; set; }

        /// <summary>
        /// Start date of a splatoon match
        /// </summary>
        [JsonProperty("start_at")]
        public required StartAt StartDate { get; set; }

        /// <summary>
        /// Players of the own team
        /// </summary>
        [JsonProperty("our_team_members")]
        public required List<Player> GoodGuys { get; set; }

        /// <summary>
        /// Players of the opposing team
        /// </summary>
        [JsonProperty("their_team_members")]
        public required List<Player> BadGuys { get; set; }

        /// <summary>
        /// Team color of the own team
        /// </summary>
        [JsonProperty("our_team_color")]
        public required string GoodGuysTeamColor { get; set; }
    }

    /// <summary>
    /// Player object
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Is the entry the Player itself
        /// </summary>
        [JsonProperty("me")]
        public bool? IsMe { get; set; }

        /// <summary>
        /// Top ranking within the team
        /// </summary>
        [JsonProperty("rank_in_team")]
        public int? TeamRank { get; set; }

        /// <summary>
        /// Name of the Player
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Nicht eindeutige Tag Number
        /// </summary>
        [JsonProperty("number")]
        public string? Diskriminator { get; set; }

        /// <summary>
        /// Title of the Splashtag Banner
        /// </summary>
        [JsonProperty("splashtag_title")]
        public string? SplashtagTitle { get; set; }

        /// <summary>
        /// Number of times the player killed someone in the match
        /// </summary>
        [JsonProperty("kill")]
        public int? KillCount { get; set; }

        /// <summary>
        /// Number of times the player supported with a kill in the match
        /// </summary>
        [JsonProperty("assist")]
        public int? AssistCount { get; set; }

        /// <summary>
        /// Number of times the player died in the match
        /// </summary>
        [JsonProperty("death")]
        public int? DeathCount { get; set; }

        /// <summary>
        /// Number of times the player used his special in the match
        /// </summary>
        [JsonProperty("special")]
        public int? SpecialUsedCount { get; set; }

        /// <summary>
        /// Number of terrain colored by the player
        /// </summary>
        [JsonProperty("inked")]
        public int? InkedPoints { get; set; }

        /// <summary>
        /// Indicates if the player disconnected during the match
        /// </summary>
        [JsonProperty("disconnected")]
        public bool IsDisconnected { get; set; }
    }

    /// <summary>
    /// Basic lobby and matchmaking information of a match
    /// </summary>
    public class LobbyData
    {
        /// <summary>
        /// The lobbytype of a match
        /// </summary>
        [JsonProperty("key")]
        public required string Key { get; set; }
    }

    /// <summary>
    /// Time at which a match started
    /// </summary>
    public class StartAt : ITimeObject
    {
        /// <inheritdoc/>
        [JsonProperty("time")]
        public int Time { get; set; }

        /// <inheritdoc/>
        [JsonProperty("iso8601")]
        public DateTime Iso8601 { get; set; }
    }

#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name
}
