namespace StravaDiscordBot.ParticipantApi.Models.Requests
{
    public class FinishAuthenticationRequest
    {
        public string Code { get; set; }
        public long ParticipantId { get; set; }
        public long LeaderboardId { get; set; }
    }
}