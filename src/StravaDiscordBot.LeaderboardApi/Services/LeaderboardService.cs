using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;
using StravaDiscordBot.LeaderboardApi.Models;
using StravaDiscordBot.LeaderboardApi.Models.Categories;
using StravaDiscordBot.LeaderboardApi.Storage;
using StravaDiscordBot.LeaderboardApi.Storage.Entities;
using StravaDiscordBot.Shared;

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
        private readonly IConsulHttpClient _consulHttpClient;
        private readonly ILogger<LeaderboardService> _logger;
        private readonly ICategoryService _categoryService;

        public LeaderboardService(
            LeaderboardContext dbContext,
            IConsulHttpClient consulHttpClient,
            ILogger<LeaderboardService> logger,
            ICategoryService categoryService)
        {
            _dbContext = dbContext;
            _consulHttpClient = consulHttpClient;
            _logger = logger;
            _categoryService = categoryService;
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
            var participants = await _consulHttpClient.GetAsync<IList<Participant>>(ServiceNames.ParticipantApi,
                $"/v1/leaderboard/{serverId}/participant");

            foreach (var participant in participants)
            {
                try
                {
                    var activities = await _consulHttpClient.GetAsync<List<SummaryActivityResponse>>(
                        ServiceNames.ParticipantApi,
                        $"/v1/leaderboard/{serverId}/participant/{participant.Id}/activity?from={start.ToString("s", CultureInfo.InvariantCulture)}");

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