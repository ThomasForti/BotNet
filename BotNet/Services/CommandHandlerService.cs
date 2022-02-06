using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace BotNet.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _socketClient;
        private readonly CommandService _commandsService;
        private readonly IServiceProvider _servicesProvider;

        public CommandHandlerService(
            DiscordSocketClient client, 
            CommandService commands, 
            IServiceProvider services
            )
        {
            _commandsService = commands;
            _socketClient = client;
            _servicesProvider = services;
        }

        public async Task InitializeCommandsAsync()
        {
            _socketClient.MessageReceived += HandleCommandAsync;
            await _commandsService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _servicesProvider);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(Config.CommandPrefix, ref argPos) ||
                message.HasMentionPrefix(_socketClient.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
            {
                return;
            }

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_socketClient, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            var result = await _commandsService.ExecuteAsync(context: context, argPos: argPos, services: _servicesProvider);


            // Error handling.
            if (!result.IsSuccess)
            {
                // AJOUTER UN FICHIER DE LOG : result.ErrorReason
                await context.Channel.SendMessageAsync(" Une erreur est survenue... :smiling_face_with_tear:");
            }
        }
    }
}
