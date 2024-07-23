using Discord.WebSocket;
using OutBot.Classes.Exceptions;
using OutBot.Classes.Services;

namespace OutBot.Events
{
    /// <summary>
    /// Event handler to notify that members are joining the configured guild
    /// </summary>
    public class WelcomeEvent
    {
        private readonly ulong configuredGuildId;
        private readonly ulong configuredChannelId;
        private readonly ImageService imageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeEvent"/> class.
        /// </summary>
        /// <param name="configManager">Instance of <see cref="ConfigService"/></param>
        /// <param name="imageService">Instance of <see cref="ImageService"/></param>
        public WelcomeEvent(ConfigService configManager, ImageService imageService)
        {
            this.configuredGuildId = configManager.Config.GlobalSettings.GuildId;
            this.configuredChannelId = configManager.Config.WelcomeMessage.ChannelId;
            this.imageService = imageService;
        }

        /// <summary>
        /// Event delegate to handle joining guild users
        /// </summary>
        /// <param name="user"><see cref="SocketGuildUser"/> that joined a guild</param>
        /// <exception cref="ConfigurationException">Thrown if the configured text channel for welcome messages
        /// could not be found</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation</returns>
        public async Task HandleEvent(SocketGuildUser user)
        {
            if (user.Guild.Id != this.configuredGuildId)
                return;

            SocketTextChannel textChannel =
                user.Guild.TextChannels.FirstOrDefault(channel => channel.Id == this.configuredChannelId) ??
                throw new ConfigurationException("Unable to find configured text channel for the welcome message");

            try
            {
                Stream welcomeImage = await this.imageService.GenerateWelcomeImage(user);
                await textChannel.SendFileAsync(welcomeImage, $"welcome_{user.Id}.png", $"<@{user.Id}>");
            }
            catch
            {
                await textChannel.SendMessageAsync($"<@{user.Id}> ist {user.Guild.Name} beigetreten!");
            }
        }
    }
}
