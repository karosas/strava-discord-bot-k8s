using System.Collections.Generic;

namespace StravaDiscordBot.LeaderboardApi.Models
{
    public class CategoryResult
    {
        public string Name { get; set; }
        public IList<SubCategoryResult> SubCategoryResults { get; set; }
    }
}