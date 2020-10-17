namespace StravaDiscordBot.Workers
{
    public class WorkerRootOptions
    {
        public ConsulOptions Consul { get; set; }
    }

    public class ConsulOptions
    {
        public string LeaderboardBaseUrl { get; set; }
        public string ParticipantBaseUrl { get; set; }
        public string DiscordBaseUrl { get; set; }
    }
}