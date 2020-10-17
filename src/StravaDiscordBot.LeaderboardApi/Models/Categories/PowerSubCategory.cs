using System.Collections.Generic;
using System.Linq;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models;

namespace StravaDiscordBot.LeaderboardApi.Models.Categories
{
    public class PowerSubCategory : ISubCategory
    {
        public string Name => "Weighted Power Ride";

        public ParticipantResult CalculateParticipantsResults(Participant participant, IList<SummaryActivityResponse> activities)
        {
            var result = 0d;
            if (activities?.Any() ?? false)
                result = activities?.Where(x => (x.ElapsedTime ?? 0d) > 20 * 60)
                    .Select(x => x.WeightedAverageWatts ?? 0)
                    .DefaultIfEmpty()
                    .Max() ?? 0;

            return new ParticipantResult
            {
                Participant = participant,
                Value = result,
                DisplayValue = $"{result:0} W"
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