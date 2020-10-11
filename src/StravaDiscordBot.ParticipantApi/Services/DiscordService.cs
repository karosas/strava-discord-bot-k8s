using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.Shared;

namespace StravaDiscordBot.ParticipantApi.Services
{
    public interface IDiscordService
    {
        Task NotifyReloginNeeded(ulong participantId);
    }

    // TODO: at some point introduce some messaging broker and replace some/all of these requests with it
    // This can possibly introduce circular dependency where services keep calling each other
    public class DiscordService : IDiscordService
    {
        private readonly IConsulHttpClient _consulHttpClient;
        private readonly ILogger<DiscordService> _logger;

        public DiscordService(IConsulHttpClient consulHttpClient, ILogger<DiscordService> logger)
        {
            _consulHttpClient = consulHttpClient;
            _logger = logger;
        }
        public async Task NotifyReloginNeeded(ulong participantId)
        {
            try
            {
                await _consulHttpClient.PostAsync<object>(ServiceNames.DiscordApi, "/notify/relogin");
            }
            catch (ConsulRequestException e)
            {
                _logger.LogError(e, "Failed to to invoke discord api");
            }
        }
    }
}