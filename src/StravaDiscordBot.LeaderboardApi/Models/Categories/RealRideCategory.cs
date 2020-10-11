using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class RealRideCategory : ICategory
    {
        public string Name => "Ride";

        public IList<ISubCategory> SubCategories => new List<ISubCategory>
        {
            new DistanceSubCategory(),
            new ElevationSubCategory(),
            new PowerSubCategory(),
            new SingleRideDistanceSubCategory()
        };

        public IList<SummaryActivityResponse> FilterActivities(IList<SummaryActivityResponse> activities)
        {
            if(activities?.Any() ?? false)
                return activities.Where(x => x.Type == ActivityType.Ride).ToList();

            return new List<SummaryActivityResponse>();
        }
    }
}