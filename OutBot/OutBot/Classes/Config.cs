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
    }
}
