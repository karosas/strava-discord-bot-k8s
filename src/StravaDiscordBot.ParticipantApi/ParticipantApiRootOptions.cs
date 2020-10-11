using StravaDiscordBot.Shared;

namespace StravaDiscordBot.ParticipantApi
{
    public class ParticipantApiRootOptions
    {
        public string ConnectionString { get; set; }
        public StravaOptions Strava { get; set; }
    }

    public class StravaOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}