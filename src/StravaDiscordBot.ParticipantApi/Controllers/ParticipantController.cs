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
        [ProducesResponseType(typeof(IList<Participant>), 200)]
        public async Task<ActionResult<IList<Participant>>> GetAllForLeaderboard(ulong leaderboardId)
        {
            return Ok(await _participantService.GetAll(leaderboardId)); // TODO ViewModels at some point probably
        }

        [HttpGet("{participantId}", Name = "Get")]
        [ProducesResponseType(typeof(Participant), 200)]

        public async Task<ActionResult<Participant>> Get(ulong leaderboardId, ulong participantId)
        {
            return Ok(await _participantService.GetOrDefault(leaderboardId, participantId));
        }

        [HttpDelete("{participantId}", Name = "Delete")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Delete(ulong leaderboardId, ulong participantId)
        {
            var participant = await _participantService.GetOrDefault(leaderboardId, participantId);
            if (participant == null)
                return NotFound();

            await _participantService.Delete(participant);
            return Ok();
        }
    }
}