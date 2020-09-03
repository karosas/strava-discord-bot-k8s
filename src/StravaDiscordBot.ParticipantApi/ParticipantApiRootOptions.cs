using StravaDiscordBot.Shared;

namespace StravaDiscordBot.ParticipantApi
{
    public class ParticipantApiRootOptions : ServiceOptionsBase
    {
        public StravaOptions Strava { get; set; }
    }

    public class StravaOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}