namespace StravaDiscordBot.ParticipantApi.Models.Requests
{
    public class FinishAuthenticationRequest
    {
        public string Code { get; set; }
        public string ParticipantId { get; set; }
        public string LeaderboardId { get; set; }
    }
}