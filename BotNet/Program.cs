using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using BotNet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BotNet
{
    class Program
    {
        private DiscordSocketClient _socketClient;
        private CommandService _commandService;
        private CommandHandlerService _commandHandlerService;
        private IServiceProvider _servicesProvider;

        public static Task Main(string[] args) => new Program().RunBotAsync();

        public async Task RunBotAsync()
        {
            if (Config.IsDebugActivated)
            {
                _socketClient = new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug
                });
            }
            else
            {
                _socketClient = new DiscordSocketClient();
            }
               
            _commandService = new CommandService();

            _servicesProvider = InitializeServiceProvider();

            _commandHandlerService = new CommandHandlerService(_socketClient, _commandService, _servicesProvider);

            new LogService(ref _socketClient, ref _commandService);

            _socketClient.Ready += () =>
            {
                Console.WriteLine("BotNet is up !");
                return Task.CompletedTask;
            };

            await _commandHandlerService.InitializeCommandsAsync();
            // The token is stored in the user environment variables of Windows for security reasons.
            await _socketClient.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("BotNetToken", EnvironmentVariableTarget.User));
            await _socketClient.StartAsync();

            // Blocking this task until program is closed (In other words, it'll set an infinite time for this task
            // -> the program won't stop by himself).
            await Task.Delay(-1);
        }

        // Set a dependency 
        public IServiceProvider InitializeServiceProvider() => new ServiceCollection()
                // Singletons that will be stored in the container of the new dependency injector
                .AddSingleton(_socketClient)
                .AddSingleton(_commandService)
                // Service(s) that'll be able to retrieve the singletons by injection in their constructor
                .AddSingleton<CommandHandlerService>()
                .BuildServiceProvider();
    }
}
