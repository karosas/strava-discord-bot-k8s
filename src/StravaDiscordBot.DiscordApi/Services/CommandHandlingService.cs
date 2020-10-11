using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using StravaDiscordBot.DiscordApi.DiscordControllers;

namespace StravaDiscordBot.DiscordApi.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlingService(CommandService commandService,
            DiscordSocketClient discordSocketClient,
            IServiceProvider serviceProvider)
        {
            _commandService = commandService;
            _discordSocketClient = discordSocketClient;
            _serviceProvider = serviceProvider;
        }

        public async Task InstallCommandsAsync()
        {
            _discordSocketClient.MessageReceived += HandleCommandAsync;

            await _commandService.AddModuleAsync<ParticipantDiscordController>(_serviceProvider);
            await _commandService.AddModuleAsync<MetaDiscordController>(_serviceProvider);
            //await _commandService.AddModuleAsync<AthleteDiscordController>(_serviceProvider);
        }

        private async Task HandleCommandAsync(SocketMessage rawMessage)
        {
            // Process only user messages (Ignoring system messages)
            var message = rawMessage as SocketUserMessage;
            if (message == null)
                return;

            // For prefix tracking so we could extract actual command
            var argPos = 0;

            // Ignore if message is not mentioning our bot or if message is made by bot
            if (!message.HasMentionPrefix(_discordSocketClient.CurrentUser, ref argPos) || message.Author.IsBot)
                return;

            // Create context based on message received
            var context = new SocketCommandContext(_discordSocketClient, message);

            // Pass the command context down to command service
            // and let it figure out which controller is listening to it
            await _commandService.ExecuteAsync(
                context,
                argPos,
                _serviceProvider);
        }
    }
}