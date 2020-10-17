using System.Collections.Generic;
using AutoMapper;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(LeaderboardResult))]
    public class LeaderboardResultViewModel
    {
        public IList<SubCategoryResultViewModel> CategoryResults { get; set; }
    }
}