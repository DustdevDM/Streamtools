using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OutBot.Classes
{
    public class Config
    {
        [JsonPropertyName("discordToken")]
        public required string DiscordToken;

        [JsonPropertyName("globalSettings")]
        public required globalSettings GlobalSettings;

        [JsonPropertyName("welcomeMessage")]
        public required welcomeMessage WelcomeMessage;
    }
    
    public class globalSettings
    {
        [JsonPropertyName("guild")]
        public required ulong GuildId;
    }

    public class welcomeMessage
    {
        [JsonPropertyName("channel")]
        public required ulong ChannelId;

        [JsonPropertyName("enabled")]
        public required bool IsEnabled;
    }
}
