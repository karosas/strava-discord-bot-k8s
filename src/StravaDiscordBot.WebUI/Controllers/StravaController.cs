using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.WebUI.Clients.ParticipantApi;
using StravaDiscordBot.WebUI.Clients.ParticipantApi.Models;
using StravaDiscordBot.WebUI.Constants;

namespace StravaDiscordBot.WebUI.Controllers
{
    [Route("strava")]
    public class StravaController : ControllerBase
    {
        private readonly ILogger<StravaController> _logger;
        private readonly IStravaDiscordBotParticipantApi _participantApi;
        
        public StravaController(IStravaDiscordBotParticipantApi participantApi, ILogger<StravaController> logger)
        {
            _participantApi = participantApi;
            _logger = logger;
        }

        [HttpGet("callback/{leaderboardId}/{participantId}")]
        public async Task<IActionResult> StravaCallback(
            string leaderboardId,
            string participantId,
            [FromQuery(Name = "code")] string code,
            [FromQuery(Name = "scope")] string scope)
        {
            if (scope == null || !scope.Contains("activity:read", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInformation($"Insufficient scopes for {LogConstants.ParticipantId}", participantId);
                return Ok("Failed to authorize user, read activities permission is needed");
            }

            await _participantApi.FinishAuthenticationAsync(new FinishAuthenticationRequest(code, participantId, leaderboardId));
            return Ok();
        }
    }
}