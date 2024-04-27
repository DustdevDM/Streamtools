using Discord;
using Discord.WebSocket;
using OutBot.Events;
using System.Diagnostics;


namespace OutBot
{
    public class EventManager
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly WelcomeEvent welcomeEvent;
        private readonly GuildLeaveEvent guildLeaveEvent;

        public EventManager(DiscordSocketClient discordSocketClient, WelcomeEvent welcomeEvent, GuildLeaveEvent guildLeaveEvent)
        {
            this.discordSocketClient = discordSocketClient;
            this.welcomeEvent = welcomeEvent;
            this.guildLeaveEvent = guildLeaveEvent;
        }

        public void registerEvents()
        {
            this.discordSocketClient.UserJoined += this.welcomeEvent.HandleEvent;
            this.discordSocketClient.UserLeft += this.guildLeaveEvent.HandleLeaveEvent;
            this.discordSocketClient.UserBanned += this.guildLeaveEvent.HandleBannEvent;
        }
    }
}
