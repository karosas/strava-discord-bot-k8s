using System.Collections.Generic;

namespace StravaDiscordBot.LeaderboardApi.Models
{
    public class LeaderboardResult
    {
        public IList<CategoryResult> CategoryResults { get; set; }
    }
}