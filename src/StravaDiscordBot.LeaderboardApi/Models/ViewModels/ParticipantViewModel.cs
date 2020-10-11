using AutoMapper;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(Participant))]
    public class ParticipantViewModel
    {
        public long? Id { get; set; }
        public long? StravaId { get; set; }
        public long? LeaderboardId { get; set; }
    }
}