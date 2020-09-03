using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StravaDiscordBot.ParticipantApi.Services;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Controllers
{
    [ApiController]
    [Route("v1/leaderboard/{leaderboardId}/participant/{participantId}/activity")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IStravaCredentialsService _stravaCredentialsService;
        private readonly IParticipantService _participantService;

        public ActivityController(IActivityService activityService, 
            IStravaCredentialsService stravaCredentialsService,
            IParticipantService participantService)
        {
            _activityService = activityService;
            _stravaCredentialsService = stravaCredentialsService;
            _participantService = participantService;
        }

        [HttpGet("", Name = "GetAllActivitiesForPeriod")]
        public async Task<ActionResult<IList<SummaryActivity>>> GetForPeriod(long leaderboardId, long participantId, [FromQuery] DateTime? from)
        {
            if(from == null || from.Value == default)
                from = DateTime.UtcNow.AddDays(-7);
            
            var participant = await _participantService.GetOrDefault(leaderboardId, participantId);
            if (participant == null)
                return NotFound();

            var credentials = await _stravaCredentialsService.GetByStravaId(participant.StravaId);
            if (credentials == null)
                return NotFound();

            return Ok(await _activityService.GetFrom(credentials, from.Value));
        }
    }
}