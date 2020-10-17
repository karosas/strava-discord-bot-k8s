using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Models.Responses;
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
        private readonly ILogger<AthleteController> _logger;
        private readonly IMapper _mapper;

        public AthleteController(
            IAthleteService athleteService, 
            ILogger<AthleteController> logger,
            IParticipantService participantService, 
            IStravaCredentialsService stravaCredentialsService, IMapper mapper)
        {
            _athleteService = athleteService;
            _logger = logger;
            _participantService = participantService;
            _stravaCredentialsService = stravaCredentialsService;
            _mapper = mapper;
        }

        [HttpGet("", Name = "GetAthlete")]
        [ProducesResponseType(typeof(DetailedAthleteResponse), 200)]
        public async Task<ActionResult<DetailedAthleteResponse>> GetAthlete(string leaderboardId, string participantId)
        {
            var participant = await _participantService.GetOrDefault(ulong.Parse(leaderboardId), ulong.Parse(participantId));
            if (participant == null)
                return NotFound();

            var credentials = await _stravaCredentialsService.GetByStravaId(participant.StravaId);
            if (credentials == null)
                return NotFound();

            var athlete = await _athleteService.Get(credentials);

            return Ok(_mapper.Map<DetailedAthlete, DetailedAthleteResponse>(athlete));
        }
    }
}