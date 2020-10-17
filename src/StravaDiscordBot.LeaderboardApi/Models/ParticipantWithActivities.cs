using System.Collections.Generic;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models
{
    public class ParticipantWithActivities
    {
        public Participant Participant { get; set; }
        public IList<SummaryActivityResponse> Activities { get; set; }
    }
}