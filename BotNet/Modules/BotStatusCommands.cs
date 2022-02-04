using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace BotNet.Modules
{
    public class BotStatusCommands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Return the ping of the bot")]
        public async Task PingAsync()
        {
            var client = new DiscordSocketClient();
            await ReplyAsync($"My ping is {client.Latency} ms.");   //TODO: SUSPICIOUS 0 ms RESPONSE
        }

        [Command("help")]
        [Summary("Return the list of the availables commands")]
        public async Task HelpAsync()
        {
            await ReplyAsync(); //TODO: GATHER ALL COMMANDS WITH THEIR SUMMARY
        }
    }
}
