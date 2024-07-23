#pragma warning disable SA1402
using Newtonsoft.Json;

namespace OutBot.Classes
{
    /// <summary>
    /// Object used for parsing data from configuration files
    /// </summary>
    public class Config
    {
        /// <summary>
        /// A discord bot authentication token
        /// </summary>
        [JsonProperty("discordToken")]
        public required string DiscordToken { get; init; }

        /// <summary>
        /// <inheritdoc cref="GlobalSettings"/>
        /// </summary>
        [JsonProperty("globalSettings")]
        public required GlobalSettings GlobalSettings { get; init; }

        /// <summary>
        /// <inheritdoc cref="WelcomeMessage"/>
        /// </summary>
        [JsonProperty("welcomeMessage")]
        public required WelcomeMessage WelcomeMessage { get; init; }
    }

    /// <summary>
    /// Collection of general settings
    /// </summary>
    public class GlobalSettings
    {
        /// <summary>
        /// Discord server id that the bot will lay primary focus to
        /// </summary>
        [JsonProperty("guild")]
        public required ulong GuildId { get; init; }
    }

    /// <summary>
    /// Collection of settings for welcome messages
    /// </summary>
    public class WelcomeMessage
    {
        /// <summary>
        /// determines if the welcome message feature is activated
        /// </summary>
        [JsonProperty("enabled")]
        public required bool IsEnabled { get; init; }

        /// <summary>
        /// The id of a discord channel that will be used to send welcome and leave messages
        /// </summary>
        [JsonProperty("channel")]
        public required ulong ChannelId { get; init; }
    }
}
#pragma warning restore SA1402
