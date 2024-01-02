using Newtonsoft.Json;

namespace StreamTools_API.Classes
{
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type

    /// <summary>
    /// Interface representing TimeObjects used by the stat.ink API
    /// </summary>
    public interface ITimeObject
    {
        /// <summary>
        /// Time in unix-seconds
        /// </summary>
        public int Timedasd { get; set; }

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
        /// Result type when a match has been lost due to disconnects in an unranked match
        /// </summary>
        [JsonProperty("draw")]
        Draw,

        /// <summary>
        /// Result type when a match has been lost due to disconnects in an ranked match
        /// </summary>
        [JsonProperty("exempted_lose")]
        Exempted_lose,
    }

    /// <summary>
    /// Response Object to parse relevant data from the stat.ink api
    /// </summary>
    public class StatInkMatchRecords
    {
        /// <summary>
        /// ID of a splatoon match data record
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Result of a splatoon match
        /// </summary>
        [JsonProperty("result")]
        public MatchResult? Result { get; set; }

        /// <summary>
        /// Startdate of a splatoon match
        /// </summary>
        [JsonProperty("start_at")]
        public StartAt? StartDate { get; set; }
    }

    /// <summary>
    /// Time at which a match started
    /// </summary>
    public class StartAt : ITimeObject
    {
        /// <inheritdoc/>
        [JsonProperty("time")]
        public int Timedasd { get; set; }

        /// <inheritdoc/>
        [JsonProperty("iso8601")]
        public DateTime Iso8601 { get; set; }
    }

#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name
}
