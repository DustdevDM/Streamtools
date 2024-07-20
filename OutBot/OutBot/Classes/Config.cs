#pragma warning disable SA1402
using Newtonsoft.Json;

namespace OutBot.Classes
{
    /// <summary>
    /// Object used for parsing data from configuration files
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// A discord bot authentication token
        /// </summary>
        [JsonProperty("discordToken")]
        internal required string DiscordToken { get; init; }

        /// <summary>
        /// <inheritdoc cref="GlobalSettings"/>
        /// </summary>
        [JsonProperty("globalSettings")]
        internal required GlobalSettings GlobalSettings { get; init; }

        /// <summary>
        /// <inheritdoc cref="WelcomeMessage"/>
        /// </summary>
        [JsonProperty("welcomeMessage")]
        internal required WelcomeMessage WelcomeMessage { get; init; }
    }

    /// <summary>
    /// Collection of general settings
    /// </summary>
    internal class GlobalSettings
    {
        /// <summary>
        /// Discord server id that the bot will lay primary focus to
        /// </summary>
        [JsonProperty("guild")]
        internal required ulong GuildId { get; init; }
    }

    /// <summary>
    /// Collection of settings for welcome messages
    /// </summary>
    internal class WelcomeMessage
    {
        /// <summary>
        /// determines if the welcome message feature is activated
        /// </summary>
        [JsonProperty("enabled")]
        internal required bool IsEnabled { get; init; }

        /// <summary>
        /// The id of a discord channel that will be used to send welcome and leave messages
        /// </summary>
        [JsonProperty("channel")]
        internal required ulong ChannelId { get; init; }
    }
}
#pragma warning restore SA1402
