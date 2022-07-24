using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Modules;

namespace DiscordBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler (DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
            InstallCommandsAsync();
        }
        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: null);
        }
        private async Task HandleCommandAsync(SocketMessage messageParams)
        {
            Console.WriteLine("Got message "+messageParams.Content);
            var message = (SocketUserMessage)messageParams;
            if (message == null) return;
            int argPos = 0;
            if(!
                
                message.HasCharPrefix('!',ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser,ref argPos) ||
                message.Author.IsBot
                
            )
            {
                return;
            }
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}
