using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models
{
    /// <summary>
    ///     Result of for a participant in a sub-category.
    ///     `Value` - raw value of achieved result (e.g. 123.4)
    ///     `DisplayValue` - formatted value for display in leaderboard (e.g. 123 km)
    /// </summary>
    public class ParticipantResult
    {
        public Participant Participant { get; set; }
        public double Value { get; set; }
        public string DisplayValue { get; set; }
    }
}