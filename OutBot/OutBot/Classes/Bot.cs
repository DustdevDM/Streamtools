using Discord;
using Discord.WebSocket;
using OutBot.Services;

namespace OutBot.Classes
{
    public class Bot
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly ConfigService configManager;
        private readonly DiscordEventService eventManager;

        public Bot(DiscordSocketClient discordSocketClient, ConfigService configManager, DiscordEventService eventManager)
        {
            this.discordSocketClient = discordSocketClient;
            this.configManager = configManager;
            this.eventManager = eventManager;
            this.discordSocketClient.Log += Log;
        }

        public async Task runBot() 
        {
            this.eventManager.registerEvents();

            await this.discordSocketClient.LoginAsync(TokenType.Bot, this.configManager.Config.DiscordToken);
            await this.discordSocketClient.StartAsync();

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
