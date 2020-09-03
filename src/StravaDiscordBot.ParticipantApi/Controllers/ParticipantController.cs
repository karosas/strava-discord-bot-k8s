using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StravaDiscordBot.ParticipantApi.Services;
using StravaDiscordBot.ParticipantApi.Storage.Entities;

namespace StravaDiscordBot.ParticipantApi.Controllers
{
    [ApiController]
    [Route("v1/leaderboard/{leaderboardId}/participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("", Name = "GetAll")]
        public async Task<ActionResult<IList<Participant>>> GetAllForLeaderboard(long leaderboardId)
        {
            return Ok(await _participantService.GetAll(leaderboardId)); // TODO ViewModels at some point probably
        }

        [HttpGet("{participantId}", Name = "Get")]
        public async Task<ActionResult<Participant>> Get(long leaderboardId, long participantId)
        {
            return Ok(await _participantService.GetOrDefault(leaderboardId, participantId));
        }

        [HttpDelete("{participantId}", Name = "Delete")]
        public async Task<ActionResult> Delete(long leaderboardId, long participantId)
        {
            var participant = await _participantService.GetOrDefault(leaderboardId, participantId);
            if (participant == null)
                return NotFound();

            await _participantService.Delete(participant);
            return Ok();
        }
    }
}