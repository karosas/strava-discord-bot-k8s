using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Models;
using StravaDiscordBot.LeaderboardApi.Models.Categories;

namespace StravaDiscordBot.LeaderboardApi.Services
{
    public interface ICategoryService
    {
        CategoryResult GetTopResults(ICategory category, IList<ParticipantWithActivities> participantWithActivities);
    }

    public class CategoryService : ICategoryService
    {
        public CategoryResult GetTopResults(ICategory category,
            IList<ParticipantWithActivities> participantsWithActivities)
        {
            var participantResultsForSubCategory = new Dictionary<ISubCategory, List<ParticipantResult>>();

            foreach (var participantWithActivities in participantsWithActivities)
            {
                var participant = participantWithActivities.Participant;
                var matchingActivitiesForCategory = category.FilterActivities(participantWithActivities.Activities);

                foreach (var subCategory in category.SubCategories)
                {
                    if (participantResultsForSubCategory.All(x => x.Key.Name != subCategory.Name))
                        participantResultsForSubCategory.Add(subCategory, new List<ParticipantResult>());

                    participantResultsForSubCategory[subCategory]
                        .Add(subCategory.CalculateParticipantsResults(participant, matchingActivitiesForCategory));
                }
            }

            return new CategoryResult
            {
                Name = category.Name,
                SubCategoryResults = participantResultsForSubCategory
                    .Select(x => x.Key.CalculateTotalResult(x.Value))
                    .ToList()
            };
        }
    }
}