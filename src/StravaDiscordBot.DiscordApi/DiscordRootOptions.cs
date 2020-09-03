using StravaDiscordBot.Shared;

namespace StravaDiscordBot.DiscordApi
{
    public class DiscordRootOptions : ServiceOptionsBase
    {
        public DiscordOptions Discord { get; set; }
    }

    public class DiscordOptions
    {
        public string Token { get; set; }
    }
}