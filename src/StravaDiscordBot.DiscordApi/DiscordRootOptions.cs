namespace StravaDiscordBot.DiscordApi
{
    public class DiscordRootOptions
    {
        public DiscordOptions Discord { get; set; }
        public ConsulOptions Consul { get; set; }
    }

    public class DiscordOptions
    {
        public string Token { get; set; }
    }

    public class ConsulOptions
    {
        public string LeaderboardBaseUrl { get; set; }
        public string ParticipantBaseUrl { get; set; }
    }
}