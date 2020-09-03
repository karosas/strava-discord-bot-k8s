using StravaDiscordBot.ParticipantApi.StravaClient;

namespace StravaDiscordBot.ParticipantApi.Storage.Entities
{
    public class Participant
    {
        public Participant()
        {
        }

        public long Id { get; set; }
        public long StravaId { get; set; }
        public long LeaderboardId { get; set; }

    }
}