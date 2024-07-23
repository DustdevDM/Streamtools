using Discord.WebSocket;
using OutBot.Classes.Exceptions;
using OutBot.Classes.Services;

namespace OutBot.Events
{
    /// <summary>
    /// Event handler to notify that members are leaving the configured guild
    /// </summary>
    public class GuildLeaveEvent
    {
        private readonly ulong configuredGuildId;
        private readonly ulong configuredChannelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuildLeaveEvent"/> class.
        /// </summary>
        /// <param name="configManager">Instance of <see cref="ConfigService"/></param>
        public GuildLeaveEvent(ConfigService configManager)
        {
            this.configuredGuildId = configManager.Config.GlobalSettings.GuildId;
            this.configuredChannelId = configManager.Config.WelcomeMessage.ChannelId;
        }

        /// <summary>
        /// Event delegate to handle leaving guild users
        /// </summary>
        /// <param name="guild"><see cref="SocketGuild"/> that the user left</param>
        /// <param name="user"><see cref="SocketUser"/> that left the guild</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation</returns>
        /// <exception cref="ConfigurationException">Thrown if the configured text channel for welcome messages
        /// could not be found</exception>
        public Task HandleLeaveEvent(SocketGuild guild, SocketUser user)
        {
            if (guild.Id != this.configuredGuildId)
                return Task.CompletedTask;

            SocketTextChannel textChannel =
                guild.TextChannels.FirstOrDefault(channel => channel.Id == this.configuredChannelId) ??
                throw new ConfigurationException("Unable to find configured text channel for the welcome message");

            textChannel.SendMessageAsync($"`{user.Username}` hat {guild.Name} verlassen!");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Event delegate to handle banned guild users
        /// </summary>
        /// <param name="user"><see cref="SocketUser"/> that was banned form the guild</param>
        /// <param name="guild"><see cref="SocketGuild"/> that the user was banned from</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation</returns>
        /// <exception cref="ConfigurationException">Thrown if the configured text channel for welcome messages
        /// could not be found</exception>
        public Task HandleBannEvent(SocketUser user, SocketGuild guild)
        {
            if (guild.Id != this.configuredGuildId)
                return Task.CompletedTask;

            SocketTextChannel textChannel =
                guild.TextChannels.FirstOrDefault(channel => channel.Id == this.configuredChannelId) ??
                throw new ConfigurationException("Unable to find configured text channel for the welcome message");

            textChannel.SendMessageAsync($"`{user.Username}` wurde aus {guild.Name} gebannt!");

            return Task.CompletedTask;
        }
    }
}
