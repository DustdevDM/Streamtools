using Discord;
using Discord.WebSocket;
using Ninject.Modules;
using OutBot.Classes.Services;

namespace OutBot.Classes.DependencyInjection
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DiscordSocketClient>().ToSelf().InSingletonScope();
            Bind<DiscordSocketConfig>().ToConstant(this.createBotConfig());
        }

        private DiscordSocketConfig createBotConfig()
        {
            return new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers
            };
        }
    }
}
