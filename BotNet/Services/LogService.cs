using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotNet.Services
{
    public class LogService
    {
        public LogService(DiscordSocketClient client, CommandService command)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;
        }

        private Task LogAsync(LogMessage message)
        {
            if (message.Exception is CommandException cmdexception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Command - {message.Severity}] {cmdexception.Command.Aliases.First()} failed to execute in {cmdexception.Context.Channel}.");
                Console.WriteLine(cmdexception);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (message.Severity == LogSeverity.Debug)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[General - {message.Severity}] {message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.WriteLine($"[General - {message.Severity}] {message}");

            return Task.CompletedTask;
        }
    }
}