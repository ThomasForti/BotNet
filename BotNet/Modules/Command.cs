using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;


namespace BotNet.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Return the ping of the bot")]
        public async Task PingAsync()
        {
            var client = new DiscordSocketClient();
            await ReplyAsync($"My ping is {client.Latency} ms.");
        }
    }
}
