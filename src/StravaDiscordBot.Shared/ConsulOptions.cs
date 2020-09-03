using System;

namespace StravaDiscordBot.Shared
{
    public class ConsulOptions
    {
        public Uri ConsulAddress { get; set; }
        public string Service { get; set; }
        public Uri ServiceAddress { get; set; }
        public int Port { get; set; }
    }
}