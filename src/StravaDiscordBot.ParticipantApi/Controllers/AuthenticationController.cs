using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Models.Requests;
using StravaDiscordBot.ParticipantApi.Models.Responses;
using StravaDiscordBot.ParticipantApi.Services;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.ParticipantApi.StravaClient;
using StravaDiscordBot.ParticipantApi.StravaClient.Client;

namespace StravaDiscordBot.ParticipantApi.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IStravaAuthenticationService _stravaAuthenticationService;
        private readonly IStravaCredentialsService _stravaCredentialsService;
        private readonly IParticipantService _participantService;
        private readonly IAthleteService _athleteService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IStravaAuthenticationService stravaAuthenticationService,
            IStravaCredentialsService stravaCredentialsService,
            IParticipantService participantService,
            IAthleteService athleteService,
            ILogger<AuthenticationController> logger)
        {
            _stravaAuthenticationService = stravaAuthenticationService;
            _stravaCredentialsService = stravaCredentialsService;
            _participantService = participantService;
            _athleteService = athleteService;
            _logger = logger;
        }

        [HttpPost("start", Name = "StartAuthentication")]
        public ActionResult<StravaOauthResponse> StartAuthentication(StartAuthenticationRequest request)
        {
            return Ok(new StartAuthenticationResponse
            {
                OAuthUrl = _stravaAuthenticationService.GenerateOAuthUrl(request.RedirectUrl)
            });
        }

        [HttpPost("finish", Name = "FinishAuthentication")]
        public async Task<ActionResult> FinishAuthentication(FinishAuthenticationRequest request)
        {
            try
            {
                var exchangeResult = await _stravaAuthenticationService.ExchangeCodeAsync(request.Code);

                var credentials = new StravaCredentials();
                credentials.UpdateWithNewTokens(exchangeResult);

                var athlete = await _athleteService.Get(credentials);
                if (athlete?.Id == null)
                    return BadRequest(); // TODO: Better handling?

                credentials.StravaId = athlete.Id.Value;
                await _stravaCredentialsService.UpsertTokens(credentials.StravaId, exchangeResult);
                var participant = await _participantService.GetOrDefault(request.LeaderboardId, request.ParticipantId);
                if (participant == null)
                    await _participantService.Create(request.ParticipantId, credentials.StravaId, exchangeResult,
                        request.LeaderboardId);

                return Ok();
            }
            catch (ApiException e)
            {
                _logger.LogError(e, "Failed to authorize with strava");
                return Ok($"Failed to authorize with Strava, error message: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create user with unknown error");
                return Ok(e.Message);
            }
        }
    }
}