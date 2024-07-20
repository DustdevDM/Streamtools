using Discord;
using Discord.WebSocket;
using Ninject.Modules;

namespace OutBot.Classes.DependencyInjection
{
    /// <summary>
    /// Class to configure special cases in the dependency injection
    /// </summary>
    internal class DiModule : NinjectModule
    {
        /// <summary>
        /// Loads dependency injection bindings
        /// </summary>
        public override void Load()
        {
            this.Bind<DiscordSocketClient>().ToSelf().InSingletonScope();
            this.Bind<DiscordSocketConfig>().ToConstant(CreateBotConfig());
        }

        private static DiscordSocketConfig CreateBotConfig()
        {
            return new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
            };
        }
    }
}
