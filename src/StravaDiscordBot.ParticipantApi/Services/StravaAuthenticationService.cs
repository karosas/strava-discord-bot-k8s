using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using StravaDiscordBot.ParticipantApi.Storage;
using StravaDiscordBot.ParticipantApi.StravaClient;
using StravaDiscordBot.ParticipantApi.StravaClient.Client;
using Policy = Consul.Policy;

namespace StravaDiscordBot.ParticipantApi.Services
{
    public interface IStravaAuthenticationService
    {
        string GenerateOAuthUrl(Uri redirectUrl);
        Task<StravaOauthResponse> ExchangeCodeAsync(string code);
        Task<StravaOauthResponse> RefreshAccessTokenAsync(string refreshToken);
        (AsyncRetryPolicy policy, Context context) GetUnauthorizedPolicy(long stravaId);
    }

    public class StravaAuthenticationService : IStravaAuthenticationService
    {
        public const string StravaIdContextKey = "strava-id";
        private readonly ILogger<StravaAuthenticationService> _logger;
        private readonly IOptionsMonitor<ParticipantApiRootOptions> _options;
        private readonly IDiscordService _discordService;
        private readonly IStravaCredentialsService _stravaCredentialsService;
        private readonly IParticipantService _participantService;

        public StravaAuthenticationService(ILogger<StravaAuthenticationService> logger,
            IOptionsMonitor<ParticipantApiRootOptions> options, 
            IDiscordService discordService, IStravaCredentialsService stravaCredentialsService, IParticipantService participantService)
        {
            _logger = logger;
            _options = options;
            _discordService = discordService;
            _stravaCredentialsService = stravaCredentialsService;
            _participantService = participantService;
        }

        public string GenerateOAuthUrl(Uri redirectUrl)
        {
            return QueryHelpers.AddQueryString("http://www.strava.com/oauth/authorize",
                new Dictionary<string, string>
                {
                    {"client_id", _options.CurrentValue.Strava.ClientId},
                    {"response_type", "code"},
                    {"redirect_uri", redirectUrl.ToString()},
                    {"approval_prompt", "force"},
                    {"scope", "read,activity:read,activity:read_all,profile:read_all,"}
                });
        }

        public async Task<StravaOauthResponse> ExchangeCodeAsync(string code)
        {
            _logger.LogInformation("Exchanging strava code");
            return await PostAsync<StravaOauthResponse>(QueryHelpers.AddQueryString(
                "https://www.strava.com/oauth/token",
                new Dictionary<string, string>
                {
                    {"client_id", _options.CurrentValue.Strava.ClientId},
                    {"client_secret", _options.CurrentValue.Strava.ClientSecret},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                }
            ));
        }

        public async Task<StravaOauthResponse> RefreshAccessTokenAsync(string refreshToken)
        {
            _logger.LogInformation("Refreshing access token");
            return await PostAsync<StravaOauthResponse>(QueryHelpers.AddQueryString(
                "https://www.strava.com/oauth/token",
                new Dictionary<string, string>
                {
                    {"client_id", _options.CurrentValue.Strava.ClientId},
                    {"client_secret", _options.CurrentValue.Strava.ClientSecret},
                    {"refresh_token", refreshToken},
                    {"grant_type", "refresh_token"}
                }));
        }

        public (AsyncRetryPolicy policy, Context context) GetUnauthorizedPolicy(long stravaId)
        {
            var pollyContext = new Context();
            pollyContext[StravaIdContextKey] = stravaId;
            return (Polly.Policy
                    .Handle<ApiException>(ex => ex.ErrorCode == 401)
                    .RetryAsync(1, OnUnauthorizedRetry),
                pollyContext);
        }

        private async Task OnUnauthorizedRetry(Exception e, int retryAttempt, Context context)
        {
            _logger.LogInformation("OnUnauthorizedRetry");
            // Try to refresh access token
            if (retryAttempt == 1 && context.ContainsKey(StravaIdContextKey) &&
                context[StravaIdContextKey] is long stravaId)
            {
                _logger.LogInformation("First retry attempt, trying to refresh access token");
                var credentials = await _stravaCredentialsService.GetByStravaId(stravaId);
                if (credentials == null)
                {
                    _logger.LogError("Couldn't find credentials to refresh");
                    // Or throw?
                    return;
                }

                try
                {
                    var refreshResult = await RefreshAccessTokenAsync(credentials.RefreshToken);
                    await _stravaCredentialsService.UpsertTokens(stravaId, refreshResult);
                    return;
                }
                catch (ApiException ex)
                {
                    _logger.LogWarning(ex, "Refreshing access token failed, DM'ing user to re-join leaderboard");
                    var participant = await _participantService.GetByStravaOrDefault(stravaId);
                    if(participant != null)
                        await _discordService.NotifyReloginNeeded(participant.Id);

                    throw;
                }
            }

            _logger.LogWarning("Couldn't find stravaId inside Polly context");
        }

        private async Task<T> PostAsync<T>(string url)
        {
            using var http = new HttpClient();

            var response = await http.PostAsync(url, null).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            _logger.LogInformation(
                $"Call to {GetUrlSuffixWithoutQuery(url)} - {response.StatusCode} Status code");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(responseContent);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new ApiException((int) response.StatusCode, "Access token expired");

            _logger.LogError($"Failed call to strava - {response.StatusCode}");
            _logger.LogError(responseContent);
            throw new ApiException((int) response.StatusCode, "Unknown error");
        }

        private static string GetUrlSuffixWithoutQuery(string urlSuffix)
        {
            return urlSuffix.Contains("?") ? urlSuffix.Split("?")[0] : urlSuffix;
        }
    }
}