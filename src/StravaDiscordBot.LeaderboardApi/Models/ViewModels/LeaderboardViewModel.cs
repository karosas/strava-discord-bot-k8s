using AutoMapper;
using StravaDiscordBot.LeaderboardApi.Storage.Entities;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(Leaderboard))]
    public class LeaderboardViewModel
    {
        public ulong Id { get; set; }
        public ulong ChannelId { get; set; }
    }
}