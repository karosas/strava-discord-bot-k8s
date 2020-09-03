using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Services;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Controllers
{
    [ApiController]
    [Route("v1/leaderboard/{leaderboardId}/participant/{participantId}/athlete")]
    public class AthleteController : ControllerBase
    {
        private readonly IAthleteService _athleteService;
        private readonly IParticipantService _participantService;
        private readonly IStravaCredentialsService _stravaCredentialsService;
        private ILogger<AthleteController> _logger;

        public AthleteController(
            IAthleteService athleteService, 
            ILogger<AthleteController> logger,
            IParticipantService participantService, 
            IStravaCredentialsService stravaCredentialsService)
        {
            _athleteService = athleteService;
            _logger = logger;
            _participantService = participantService;
            _stravaCredentialsService = stravaCredentialsService;
        }

        [HttpGet("", Name = "GetAthlete")]
        public async Task<ActionResult<DetailedAthlete>> GetAthlete(long leaderboardId, long participantId)
        {
            var participant = await _participantService.GetOrDefault(leaderboardId, participantId);
            if (participant == null)
                return NotFound();

            var credentials = await _stravaCredentialsService.GetByStravaId(participant.StravaId);
            if (credentials == null)
                return NotFound();

            return await _athleteService.Get(credentials);
        }
    }
}