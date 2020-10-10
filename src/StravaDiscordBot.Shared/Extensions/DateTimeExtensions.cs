﻿using System;

namespace StravaDiscordBot.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static long GetEpochTimestamp(this DateTime datetime)
        {
            return (long) (datetime - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}