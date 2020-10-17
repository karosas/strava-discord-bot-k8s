using StravaDiscordBot.ParticipantApi.StravaClient;

namespace StravaDiscordBot.ParticipantApi.Storage.Entities
{
    public class Participant
    {
        public Participant()
        {
        }

        public ulong Id { get; set; }
        public long StravaId { get; set; }
        public ulong LeaderboardId { get; set; }

    }
}