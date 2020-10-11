namespace StravaDiscordBot.ParticipantApi.Models.Requests
{
    public class FinishAuthenticationRequest
    {
        public string Code { get; set; }
        public ulong ParticipantId { get; set; }
        public ulong LeaderboardId { get; set; }
    }
}