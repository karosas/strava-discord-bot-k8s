using System.Collections.Generic;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
        public interface ICategory
        {
            /// <summary>
            ///     Display name for the category
            /// </summary>
            public string Name { get; }

            /// <summary>
            ///     Filter out unwanted activities (e.g. by activity type)
            /// </summary>
            /// <param name="activities">Unfiltered activities for leaderboard period</param>
            /// <returns>Filtered activity list that are sanitized for this category</returns>
            IList<SummaryActivityResponse> FilterActivities(IList<SummaryActivityResponse> activities);

            /// <summary>
            ///     List of subcategories for this category
            /// </summary>
            IList<ISubCategory> SubCategories { get; }
        }
}