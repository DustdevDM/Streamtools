using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutBot.Events
{
    public class WelcomeEvent
    {
        public Task HandleEvent(SocketGuildUser user)
        {
            user.Guild.TextChannels.Where(x => x.Id == 1231643199605440572).First().SendMessageAsync($"<@{user.Id}> ist {user.Guild.Name} beigetreten!");

            return Task.CompletedTask;
        }
    }
}
