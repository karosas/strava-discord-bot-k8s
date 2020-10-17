namespace StravaDiscordBot.LeaderboardApi
{
    public class LeaderboardApiRootOptions
    {
        public string ConnectionString { get; set; }
        public ConsulOptions Consul { get; set; }
    }
    
    public class ConsulOptions
    {
        public string ParticipantBaseUrl { get; set; }
    }
}