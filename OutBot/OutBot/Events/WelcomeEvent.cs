using Discord.WebSocket;
using OutBot.Classes.Exceptions;

namespace OutBot.Events
{
    public class WelcomeEvent
    {
        private readonly ulong configuredGuildId;
        private readonly ulong configuredChannelId;

        public WelcomeEvent(ConfigManager configManager)
        {
            this.configuredGuildId = configManager.Config.GlobalSettings.GuildId;
            this.configuredChannelId = configManager.Config.WelcomeMessage.ChannelId;
        }

        public Task HandleEvent(SocketGuildUser user)
        {
            if (user.Guild.Id != this.configuredGuildId)
                return Task.CompletedTask;

            SocketTextChannel textChannel = this.getTextChannel(user.Guild);

            textChannel.SendMessageAsync($"<@{user.Id}> ist {user.Guild.Name} beigetreten!");

            return Task.CompletedTask;
        }

        private SocketTextChannel getTextChannel(SocketGuild guild)
        {
            SocketTextChannel textChannel = guild.TextChannels.Where(channel => channel.Id == this.configuredChannelId).FirstOrDefault() ?? throw new ConfigurationException("Unable to find the text channel for welcome message in the configurated guild");
            return textChannel;
        }
    }
}
