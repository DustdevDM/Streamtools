using Discord.WebSocket;
using OutBot.Events;


namespace OutBot.Classes.Services
{
    public class DiscordEventService
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly ConfigService configManager;
        private readonly WelcomeEvent welcomeEvent;
        private readonly GuildLeaveEvent guildLeaveEvent;

        public DiscordEventService(DiscordSocketClient discordSocketClient, ConfigService configManager, WelcomeEvent welcomeEvent, GuildLeaveEvent guildLeaveEvent)
        {
            this.discordSocketClient = discordSocketClient;
            this.configManager = configManager;
            this.welcomeEvent = welcomeEvent;
            this.guildLeaveEvent = guildLeaveEvent;
        }

        public void registerEvents()
        {
            if (configManager.Config.WelcomeMessage.IsEnabled)
            {
                discordSocketClient.UserJoined += welcomeEvent.HandleEvent;
                discordSocketClient.UserLeft += guildLeaveEvent.HandleLeaveEvent;
                discordSocketClient.UserBanned += guildLeaveEvent.HandleBannEvent;
            }
        }
    }
}
