using System.Collections.Generic;

namespace StravaDiscordBot.LeaderboardApi.Models
{
    /// <summary>
    ///     Results of a sub-category with all participant results ordered from best performance to worst
    /// </summary>
    public class SubCategoryResult
    {
        public string Name { get; set; }
        public IList<ParticipantResult> OrderedParticipantResults { get; set; }
    }
}