using Discord;
using Discord.WebSocket;
using OutBot.Classes.Services;

namespace OutBot.Classes
{
    public class Bot
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly ConfigService configManager;
        private readonly DiscordEventService eventManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="discordSocketClient">Instance of <see cref="discordSocketClient"/></param>
        /// <param name="configManager">Instance of <see cref="ConfigService"/></param>
        /// <param name="eventManager">Instance of <see cref="DiscordEventService"/></param>
        public Bot(DiscordSocketClient discordSocketClient, ConfigService configManager, DiscordEventService eventManager)
        {
            this.discordSocketClient = discordSocketClient;
            this.configManager = configManager;
            this.eventManager = eventManager;
            this.discordSocketClient.Log += Log;
        }

        /// <summary>
        /// Executes the discord bot
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task RunBot()
        {
            this.eventManager.RegisterEvents();

            await this.discordSocketClient.LoginAsync(TokenType.Bot, this.configManager.Config.DiscordToken);
            await this.discordSocketClient.StartAsync();

            await this.discordSocketClient.SetCustomStatusAsync($"running {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name} v.{System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
