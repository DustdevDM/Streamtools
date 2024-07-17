using Discord.WebSocket;
using OutBot.Classes.Exceptions;
using OutBot.Classes.Services;

namespace OutBot.Events
{
    public class WelcomeEvent
    {
        private readonly ulong configuredGuildId;
        private readonly ulong configuredChannelId;
        private readonly ImageService imageService;

        public WelcomeEvent(ConfigService configManager, ImageService imageService)
        {
            this.configuredGuildId = configManager.Config.GlobalSettings.GuildId;
            this.configuredChannelId = configManager.Config.WelcomeMessage.ChannelId;
            this.imageService = imageService;
        }

        public async Task HandleEvent(SocketGuildUser user)
        {
            if (user.Guild.Id != this.configuredGuildId)
                return;

            SocketTextChannel textChannel = this.getTextChannel(user.Guild);

            var welcomeImage = await this.imageService.GenerateWelcomeImage(user);
            await textChannel.SendFileAsync(welcomeImage, $"welcome_{user.Id}.png", $"<@{user.Id}>");

            return;
        }

        private SocketTextChannel getTextChannel(SocketGuild guild)
        {
            SocketTextChannel textChannel = guild.TextChannels.Where(channel => channel.Id == this.configuredChannelId).FirstOrDefault() ?? throw new ConfigurationException("Unable to find the text channel for welcome message in the configurated guild");
            return textChannel;
        }
    }
}
