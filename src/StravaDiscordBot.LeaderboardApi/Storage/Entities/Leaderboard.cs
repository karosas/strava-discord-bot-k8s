using System.ComponentModel.DataAnnotations;
using AutoMapper;
using StravaDiscordBot.LeaderboardApi.Models.ViewModels;

namespace StravaDiscordBot.LeaderboardApi.Storage.Entities
{
    [AutoMap(typeof(LeaderboardViewModel))]
    public class Leaderboard
    {
        [Key] public ulong Id { get; set; }
        public ulong ChannelId { get; set; }
    }
}