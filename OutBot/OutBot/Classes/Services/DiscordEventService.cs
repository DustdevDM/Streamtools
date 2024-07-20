using Discord.WebSocket;
using OutBot.Events;

namespace OutBot.Classes.Services
{
    /// <summary>
    /// Helper class to manage all discord socket related events
    /// </summary>
    internal abstract class DiscordEventService
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly ConfigService configService;
        private readonly WelcomeEvent welcomeEvent;
        private readonly GuildLeaveEvent guildLeaveEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordEventService"/> class.
        /// </summary>
        /// <param name="discordSocketClient">Instance of <see cref="discordSocketClient"/></param>
        /// <param name="configService">Instance of <see cref="ConfigService"/></param>
        /// <param name="welcomeEvent">Instance of <see cref="WelcomeEvent"/></param>
        /// <param name="guildLeaveEvent">Instance of <see cref="GuildLeaveEvent"/></param>
        internal DiscordEventService(DiscordSocketClient discordSocketClient, ConfigService configService, WelcomeEvent welcomeEvent, GuildLeaveEvent guildLeaveEvent)
        {
            this.discordSocketClient = discordSocketClient;
            this.configService = configService;
            this.welcomeEvent = welcomeEvent;
            this.guildLeaveEvent = guildLeaveEvent;
        }

        /// <summary>
        /// Adds all events subscriptions between the discord socket events and the OutBot event delegates
        /// </summary>
        internal void RegisterEvents()
        {
            if (this.configService.Config.WelcomeMessage.IsEnabled)
            {
                this.discordSocketClient.UserJoined += this.welcomeEvent.HandleEvent;
                this.discordSocketClient.UserLeft += this.guildLeaveEvent.HandleLeaveEvent;
                this.discordSocketClient.UserBanned += this.guildLeaveEvent.HandleBannEvent;
            }
        }
    }
}
