using System;

namespace StravaDiscordBot.Shared.Exceptions
{
    public class ConsulException : Exception
    {
        public ConsulException(string message) : base(message)
        {
        }
    }
}