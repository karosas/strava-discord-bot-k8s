using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class ElevationSubCategory : ISubCategory
    {
        public string Name => "Elevation";

        public ParticipantResult CalculateParticipantsResults(Participant participant, IList<SummaryActivityResponse> activities)
        {
            var result = activities?.Sum(x => x.TotalElevationGain ?? 0d) ?? 0;
            return new ParticipantResult
            {
                Participant = participant,
                Value = result,
                DisplayValue = $"{result:0.#} m"
            };
        }

        public SubCategoryResult CalculateTotalResult(IList<ParticipantResult> participantResults)
        {
            return new SubCategoryResult
            {
                Name = Name,
                OrderedParticipantResults = participantResults?.OrderByDescending(x => x.Value).ToList()
            };
        }
    }
}