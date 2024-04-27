using Newtonsoft.Json;

namespace OutBot.Classes
{
    public class Config
    {
        [JsonProperty("discordToken")]
        public required string DiscordToken;

        [JsonProperty("globalSettings")]
        public required globalSettings GlobalSettings;

        [JsonProperty("welcomeMessage")]
        public required welcomeMessage WelcomeMessage;
    }
    
    public class globalSettings
    {
        [JsonProperty("guild")]
        public required ulong GuildId;
    }

    public class welcomeMessage
    {
        [JsonProperty("enabled")]
        public required bool IsEnabled;

        [JsonProperty("channel")]
        public required ulong ChannelId;
    }
}
