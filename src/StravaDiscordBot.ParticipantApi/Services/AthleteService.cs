using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.ParticipantApi.StravaClient.Api;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Services
{
    public interface IAthleteService
    {
        Task<DetailedAthlete> Get(StravaCredentials credentials);
    }

    public class AthleteService : IAthleteService
    {
        private readonly IAthletesApi _athletesApi;
        private readonly ILogger<AthleteService> _logger;
        private readonly IStravaAuthenticationService _stravaAuthenticationService;

        public AthleteService(IAthletesApi athletesApi,
            ILogger<AthleteService> logger,
            IStravaAuthenticationService stravaAuthenticationService)
        {
            _athletesApi = athletesApi;
            _logger = logger;
            _stravaAuthenticationService = stravaAuthenticationService;
        }

        public async Task<DetailedAthlete> Get(StravaCredentials credentials)
        {
            try
            {
                _logger.LogInformation($"Fetching athlete for strava id {credentials.StravaId}");

                _athletesApi.Configuration.AccessToken = credentials.AccessToken;

                var (policy, context) = _stravaAuthenticationService.GetUnauthorizedPolicy(credentials.StravaId);
                return await policy.ExecuteAsync(x => _athletesApi.GetLoggedInAthleteAsync(), context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to fetch athlete");
                throw;
            }
        }
    }
}