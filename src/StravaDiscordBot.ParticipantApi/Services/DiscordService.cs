using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Clients.DiscordApi;

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
        private readonly IStravaDiscordBotDiscordApi _discordApi;
        private readonly ILogger<DiscordService> _logger;

        public DiscordService(ILogger<DiscordService> logger, IStravaDiscordBotDiscordApi discordApi)
        {
            _logger = logger;
            _discordApi = discordApi;
        }
        public async Task NotifyReloginNeeded(ulong participantId)
        {
            // TODO: Actually implement this_discordApi.SendDMAsync()
        }
    }
}