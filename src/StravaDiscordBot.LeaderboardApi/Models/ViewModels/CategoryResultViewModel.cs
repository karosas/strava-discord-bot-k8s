using System.Collections.Generic;
using AutoMapper;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(CategoryResult))]
    public class CategoryResultViewModel
    {
        public string Name { get; set; }
        public IList<SubCategoryResultViewModel> SubCategoryResults { get; set; }
    }
}