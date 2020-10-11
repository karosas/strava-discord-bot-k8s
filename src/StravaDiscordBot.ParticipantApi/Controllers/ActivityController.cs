using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StravaDiscordBot.ParticipantApi.Models.Responses;
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
        private readonly IMapper _mapper;

        public ActivityController(IActivityService activityService, 
            IStravaCredentialsService stravaCredentialsService,
            IParticipantService participantService, IMapper mapper)
        {
            _activityService = activityService;
            _stravaCredentialsService = stravaCredentialsService;
            _participantService = participantService;
            _mapper = mapper;
        }

        [HttpGet("", Name = "GetAllActivitiesForPeriod")]
        [ProducesResponseType(typeof(IList<SummaryActivityResponse>), 200)]
        public async Task<ActionResult<IList<SummaryActivityResponse>>> GetForPeriod(ulong leaderboardId, ulong participantId, [FromQuery] DateTime? from)
        {
            if(from == null || from.Value == default)
                from = DateTime.UtcNow.AddDays(-7);
            
            var participant = await _participantService.GetOrDefault(leaderboardId, participantId);
            if (participant == null)
                return NotFound();

            var credentials = await _stravaCredentialsService.GetByStravaId(participant.StravaId);
            if (credentials == null)
                return NotFound();

            var activities = await _activityService.GetFrom(credentials, from.Value);
            
            return Ok(activities.Select(_mapper.Map<SummaryActivity, SummaryActivityResponse>));
        }
    }
}