using Discord;
using Discord.WebSocket;

namespace OutBot
{
    public class Bot
    {
        private readonly DiscordSocketClient discordSocketClient;
        private readonly ConfigManager configManager;
        private readonly EventManager eventManager;

        public Bot(DiscordSocketClient discordSocketClient, ConfigManager configManager, EventManager eventManager)
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
