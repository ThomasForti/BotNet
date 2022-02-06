using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace BotNet.Modules
{
    public class BotStatusCommands : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _socketClient;

        public BotStatusCommands(CommandService commandService, DiscordSocketClient socketClient)
        {
            _commandService = commandService;
            _socketClient = socketClient;
        }

        [Command("ping")]
        [Summary("Retourne la latence moyenne.")]
        public async Task PingCommand()
        {
            await ReplyAsync($"My ping is {_socketClient.Latency} ms.");
        }

        [Command("help")]
        [Summary("Retourne la liste de toutes les commandes disponibles.")]
        public async Task HelpCommand()
        {
            List<CommandInfo> commands = _commandService.Commands.ToList();
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Title = "Manuel des commandes",
                ImageUrl = "https://c.tenor.com/LdAr7ZnMsaMAAAAd/yes-sir-yes-boss.gif",
                Color = Color.Blue,
                Footer = new EmbedFooterBuilder().WithText($"Généré par votre serviteur : {Context.Client.CurrentUser.Username}"),
                Timestamp = DateTime.Now,
            };

            foreach (CommandInfo command in commands)
            {
                // Get the command Summary attribute information
                string embedFieldText = command.Summary ?? "No description available\n";
                embedBuilder.AddField(command.Name, embedFieldText);
            }

            await ReplyAsync("Ravi de pouvoir t'aider :nerd:", false, embedBuilder.Build());

        }
    }
}
