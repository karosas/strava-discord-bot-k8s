using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class DistanceSubCategory : ISubCategory
    {
        public string Name => "Distance";

        public ParticipantResult CalculateParticipantsResults(Participant participant, IList<SummaryActivityResponse> activites)
        {
            var result = activites?.Sum(x => (x.Distance ?? 0d) / 1000) ?? 0;

            return new ParticipantResult
            {
                Participant = participant,
                Value = result,
                DisplayValue = $"{result:0.##} km"
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