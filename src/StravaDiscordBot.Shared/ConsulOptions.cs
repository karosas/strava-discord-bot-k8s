using System;

namespace StravaDiscordBot.Shared
{
    public class ConsulOptions
    {
        public bool Enabled { get; set; }
        public string Service { get; set; }
        public Uri Address { get; set; }
        public int Port { get; set; }
    }
}