using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace StravaDiscordBot.DiscordApi.DiscordControllers
{
    public class MetaDiscordController : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly ILogger<MetaDiscordController> _logger;

        public MetaDiscordController(CommandService commandService, ILogger<MetaDiscordController> logger)
        {
            _commandService = commandService;
            _logger = logger;
        }
        
        [Command("help")]
        [Summary("Lists available commands")]
        public async Task Help()
        {
            using (Context.Channel.EnterTypingState())
            {
                try
                {
                    var commands = _commandService.Commands.ToList();
                    var embedBuilder = new EmbedBuilder();

                    foreach (var command in commands)
                    {
                        var embedFieldText = command.Summary ?? "No description available\n";
                        var commandName = command.Aliases.FirstOrDefault() ?? "unknown";
                        var args = string.Join(' ', command.Parameters.Select(x => $":{x.Name}"));
                        embedBuilder.AddField($"{commandName} {args}", embedFieldText);
                    }

                    await ReplyAsync("Here's a list of commands and their description: ", false, embedBuilder.Build());
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Help failed");
                }
            }
        }
    }
}