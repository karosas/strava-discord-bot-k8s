using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class SingleRideDistanceSubCategory : ISubCategory
    {
        public string Name => "Longest Ride";

        public ParticipantResult CalculateParticipantsResults(Participant participant, IList<SummaryActivityResponse> activities)
        {
            var result = 0d;
            if (activities?.Any() ?? false)
                result = activities.Select(x => (x.Distance ?? 0d) / 1000)
                    .DefaultIfEmpty()
                    .Max();

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