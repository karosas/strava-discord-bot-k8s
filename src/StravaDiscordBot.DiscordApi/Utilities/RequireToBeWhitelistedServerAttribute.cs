using System;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StravaDiscordBot.DiscordApi.Utilities
{
    public class RequireToBeWhitelistedServerAttribute : PreconditionAttribute
    {
        // Override the CheckPermissions method
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command,
            IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<RequireToBeWhitelistedServerAttribute>>();
            if (context is SocketCommandContext socketCommandContext && socketCommandContext.IsPrivate)
            {
                logger.LogInformation($"Tried to execute server command in DM");
                return Task.FromResult(PreconditionResult.FromError("Not a server"));
            }

            var serverId = context?.Guild?.Id;
            if (serverId == null)
            {
                logger.LogWarning("ServerId null");
                return Task.FromResult(PreconditionResult.FromError("Not a whitelisted server"));
            }

            // TODO: Call future leaderboard API
            /*var result = Leaderboards.Any(x => x.ServerId == serverId.ToString())
                ? PreconditionResult.FromSuccess()
                : PreconditionResult.FromError("Not a whitelisted server");*/
            var result = PreconditionResult.FromError("Not implemented");
            logger.LogError("Whitelisted server requirements success - {require_to_be_whitelisted_server_result}",
                result.IsSuccess);
            return Task.FromResult(result);
        }
    }
}