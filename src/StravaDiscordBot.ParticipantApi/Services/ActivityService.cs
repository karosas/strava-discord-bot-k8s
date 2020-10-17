using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Extensions;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.ParticipantApi.StravaClient.Api;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Services
{
    public interface IActivityService
    {
        Task<IList<SummaryActivity>> GetFrom(StravaCredentials credentials, DateTime after);
    }
    public class ActivityService : IActivityService
    {
        private readonly IActivitiesApi _activitiesApi;
        private readonly ILogger<ActivityService> _logger;

        public ActivityService(IActivitiesApi activitiesApi, ILogger<ActivityService> logger)
        {
            _activitiesApi = activitiesApi;
            _logger = logger;
        }

        async Task<IList<SummaryActivity>> IActivityService.GetFrom(StravaCredentials credentials, DateTime after)
        {
            _logger.LogInformation($"Fetching activities for strava {credentials.StravaId}");

            _activitiesApi.Configuration.AccessToken = credentials.AccessToken;
            return await _activitiesApi.GetLoggedInAthleteActivitiesAsync(after: (int) after.GetEpochTimestamp(),
                perPage: 1000);
        }
    }
}