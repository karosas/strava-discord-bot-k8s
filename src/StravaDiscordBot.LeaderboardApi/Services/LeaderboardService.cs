using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi;
using StravaDiscordBot.LeaderboardApi.Models;
using StravaDiscordBot.LeaderboardApi.Models.Categories;
using StravaDiscordBot.LeaderboardApi.Storage;
using StravaDiscordBot.LeaderboardApi.Storage.Entities;

namespace StravaDiscordBot.LeaderboardApi.Services
{
    public interface ILeaderboardService
    {
        Task Create(Leaderboard leaderboard);
        Task<Leaderboard> Get(ulong id);
        IList<Leaderboard> GetAll();
        Task<LeaderboardResult> GenerateLeaderboardResult(ulong serverId, DateTime start, params ICategory[] categories);
    }

    public class LeaderboardService : ILeaderboardService
    {
        private readonly LeaderboardContext _dbContext;
        private readonly ILogger<LeaderboardService> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IStravaDiscordBotParticipantApi _participantApi;

        public LeaderboardService(
            LeaderboardContext dbContext,
            ILogger<LeaderboardService> logger,
            ICategoryService categoryService, IStravaDiscordBotParticipantApi participantApi)
        {
            _dbContext = dbContext;
            _logger = logger;
            _categoryService = categoryService;
            _participantApi = participantApi;
        }

        public async Task Create(Leaderboard leaderboard)
        {
            await _dbContext.Leaderboards.AddAsync(leaderboard);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Leaderboard> Get(ulong id)
        {
            return await _dbContext.Leaderboards.FindAsync(id);
        }

        public IList<Leaderboard> GetAll()
        {
            return _dbContext.Leaderboards.ToList();
        }

        public async Task<LeaderboardResult> GenerateLeaderboardResult(ulong serverId, DateTime start,
            params ICategory[] categories)
        {
            var participantsWithActivities = new List<ParticipantWithActivities>();
            var participants = await _participantApi.GetAllAsync(serverId.ToString());

            foreach (var participant in participants)
            {
                try
                {
                    var activities = await _participantApi.GetAllActivitiesForPeriodAsync(serverId.ToString(),
                        participant.Id.ToString(), fromParameter: start);

                    participantsWithActivities.Add(new ParticipantWithActivities
                    {
                        Participant = participant,
                        Activities = activities
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed to fetch activities for {participant.Id}");
                }
            }

            var categoryResults = categories
                .Select(category => _categoryService.GetTopResults(category, participantsWithActivities))
                .ToList();

            return new LeaderboardResult
            {
                CategoryResults = categoryResults
            };
        }
    }
}