using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutBot.Events
{
    public class GuildLeaveEvent
    {
        public Task HandleLeaveEvent(SocketGuild guild, SocketUser user)
        {
            guild.TextChannels.Where(x => x.Id == 1231643199605440572).First().SendMessageAsync($"`{user.Username}` hat {guild.Name} verlassen!");

            return Task.CompletedTask;
        }

        public Task HandleBannEvent(SocketUser user, SocketGuild guild)
        {
            guild.TextChannels.Where(x => x.Id == 1231643199605440572).First().SendMessageAsync($"`{user.Username}` wurde aus {guild.Name} gebannt!");

            return Task.CompletedTask;
        }
    }
}
