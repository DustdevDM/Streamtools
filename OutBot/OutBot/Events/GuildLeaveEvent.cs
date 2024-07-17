using Discord.WebSocket;
using OutBot.Classes.Exceptions;
using OutBot.Classes.Services;

namespace OutBot.Events
{
    public class GuildLeaveEvent
    {
        private readonly ulong configuredGuildId;
        private readonly ulong configuredChannelId;

        public GuildLeaveEvent(ConfigService configManager)
        {
            this.configuredGuildId = configManager.Config.GlobalSettings.GuildId;
            this.configuredChannelId = configManager.Config.WelcomeMessage.ChannelId;
        }

        public Task HandleLeaveEvent(SocketGuild guild, SocketUser user)
        {
            if (guild.Id !=  this.configuredGuildId)
                return Task.CompletedTask;

            SocketTextChannel textChannel = this.getTextChannel(guild);

            textChannel.SendMessageAsync($"`{user.Username}` hat {guild.Name} verlassen!");

            return Task.CompletedTask;
        }

        public Task HandleBannEvent(SocketUser user, SocketGuild guild)
        {
            if (guild.Id != this.configuredGuildId)
                return Task.CompletedTask;

            SocketTextChannel textChannel = this.getTextChannel(guild);


            textChannel.SendMessageAsync($"`{user.Username}` wurde aus {guild.Name} gebannt!");

            return Task.CompletedTask;
        }

        private SocketTextChannel getTextChannel(SocketGuild guild)
        {
            SocketTextChannel textChannel = guild.TextChannels.Where(channel => channel.Id == this.configuredChannelId).FirstOrDefault() ?? throw new ConfigurationException("Unable to find the text channel for welcome message in the configurated guild");
            return textChannel;
        }
    }
}
