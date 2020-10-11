using System.Collections.Generic;
using AutoMapper;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(SubCategoryResult))]
    public class SubCategoryResultViewModel
    {
        public string Name { get; set; }
        public IList<ParticipantResultViewModel> OrderedParticipantResults { get; set; }
    }
}