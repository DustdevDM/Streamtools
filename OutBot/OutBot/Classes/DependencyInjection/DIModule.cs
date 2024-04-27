using Discord.WebSocket;
using Ninject.Activation;
using Ninject.Modules;
using OutBot.Services;

namespace OutBot.Classes.DependencyInjection
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ConfigService>().ToSelf();
            Bind<Bot>().ToSelf();
            Bind<DiscordSocketClient>().ToSelf().InSingletonScope();
            Bind<DiscordSocketConfig>().ToMethod((IContext context) => 
            {
                return new DiscordSocketConfig()
                {
                    GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.GuildMembers
                }; 
                
            });
        }
    }
}
