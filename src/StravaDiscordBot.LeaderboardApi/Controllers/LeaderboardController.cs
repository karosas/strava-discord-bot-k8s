using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.LeaderboardApi.Models;
using StravaDiscordBot.LeaderboardApi.Models.Categories;
using StravaDiscordBot.LeaderboardApi.Models.ViewModels;
using StravaDiscordBot.LeaderboardApi.Services;
using StravaDiscordBot.LeaderboardApi.Storage.Entities;

namespace StravaDiscordBot.LeaderboardApi.Controllers
{
    [ApiController]
    [Route("v1/leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILogger<LeaderboardController> _logger;
        private readonly IMapper _mapper;

        public LeaderboardController(ILeaderboardService leaderboardService, ILogger<LeaderboardController> logger,
            IMapper mapper)
        {
            _leaderboardService = leaderboardService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("", Name = "GetAllLeaderboards")]
        [ProducesResponseType(typeof(IList<LeaderboardViewModel>), 200)]
        public ActionResult<IList<LeaderboardViewModel>> GetAll()
        {
            var leaderboards = _leaderboardService.GetAll();
            return Ok(leaderboards.Select(_mapper.Map<Leaderboard, LeaderboardViewModel>));
        }

        [HttpPost("", Name = "CreateLeaderboard")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Create([FromBody] LeaderboardViewModel viewModel)
        {
            var leaderboard = _mapper.Map<LeaderboardViewModel, Leaderboard>(viewModel);
            await _leaderboardService.Create(leaderboard);
            return Ok();
        }

        [HttpGet("{leaderboardId}", Name = "GetLeaderboard")]
        [ProducesResponseType(typeof(LeaderboardViewModel), 200)]
        public async Task<ActionResult<LeaderboardViewModel>> Get(ulong leaderboardId)
        {
            var leaderboard = await _leaderboardService.Get(leaderboardId);
            return Ok(_mapper.Map<Leaderboard, LeaderboardViewModel>(leaderboard));
        }

        [HttpGet("{leaderboardId}/result", Name = "GenerateLeaderboardResults")]
        [ProducesResponseType(typeof(LeaderboardResultViewModel), 200)]
        public async Task<ActionResult<LeaderboardResultViewModel>> GenerateLeaderboardResults(ulong leaderboardId,
            [FromQuery] DateTime? start)
        {
            start ??= DateTime.Now.AddDays(-7);

            var result = await _leaderboardService.GenerateLeaderboardResult(leaderboardId, start.Value,
                new RealRideCategory(), new VirtualRideCategory()); // TODO: Categories probably should be specified in the request

            return Ok(_mapper.Map<LeaderboardResult, LeaderboardResultViewModel>(result));
        }
    }
}