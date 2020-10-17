using System.ComponentModel.DataAnnotations;
using StravaDiscordBot.ParticipantApi.StravaClient;

namespace StravaDiscordBot.ParticipantApi.Storage.Entities
{
    public class StravaCredentials
    {
        public StravaCredentials()
        {
        }

        public StravaCredentials(long stravaId, string accessToken, string refreshToken)
        {
            StravaId = stravaId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        [Key] public long StravaId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }


        public void UpdateWithNewTokens(StravaOauthResponse response)
        {
            AccessToken = response.AccessToken;
            RefreshToken = response.RefreshToken;
        }
    }
}