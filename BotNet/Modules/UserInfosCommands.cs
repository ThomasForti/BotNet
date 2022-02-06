using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNet.Modules
{
    class UserInfosCommands : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _socketClient;

        public UserInfosCommands(CommandService commandService, DiscordSocketClient socketClient)
        {
            _commandService = commandService;
            _socketClient = socketClient;
        }

        [Command("me")]
        [Summary("Retourne les informations disponibles de l'utilisateur.")]
        public async Task UserInfosCommand()
        {
            await ReplyAsync($"Tu es {_socketClient.CurrentUser}.");
        }
    }
}
