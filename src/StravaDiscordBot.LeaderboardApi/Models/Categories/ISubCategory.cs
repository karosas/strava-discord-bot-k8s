using System.Collections.Generic;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public interface ISubCategory
    {
        /// <summary>
        ///     Display name of subcategory
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Calculates single participant's result for this specific leaderboard's subcategory
        /// </summary>
        /// <param name="participant">Participant of the leaderboard</param>
        /// <param name="activities">Participant's activities that are already pre-filtered for parent category and timespan of the leaderboard</param>
        /// <returns>ParticipantResult with participant and string result</returns>
        public ParticipantResult CalculateParticipantsResults(Participant participant, IList<SummaryActivityResponse> activities);

        /// <summary>
        ///     Calculates final subcategory result
        /// </summary>
        /// <param name="participantResults">Results of all participants</param>
        /// <returns>SubcategoryResult with correctly ordered results</returns>
        public SubCategoryResult CalculateTotalResult(IList<ParticipantResult> participantResults);
    }
}