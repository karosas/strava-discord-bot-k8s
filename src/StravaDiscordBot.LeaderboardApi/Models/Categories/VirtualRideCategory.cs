using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class VirtualRideCategory : ICategory
    {
        public string Name => "VirtualRide";

        public IList<ISubCategory> SubCategories => new List<ISubCategory>
        {
            new DistanceSubCategory(),
            new ElevationSubCategory(),
            new PowerSubCategory(),
            new SingleRideDistanceSubCategory()
        };

        public IList<SummaryActivityResponse> FilterActivities(IList<SummaryActivityResponse> activities)
        {
            if (activities?.Any() ?? false)
                return activities.Where(x => x.Type == ActivityType.VirtualRide).ToList();

            return new List<SummaryActivityResponse>();
        }
    }
}