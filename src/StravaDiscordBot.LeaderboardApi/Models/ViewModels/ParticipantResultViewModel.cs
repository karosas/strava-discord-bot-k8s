using AutoMapper;

namespace StravaDiscordBot.LeaderboardApi.Models.ViewModels
{
    [AutoMap(typeof(ParticipantResult))]
    public class ParticipantResultViewModel
    {
        public ParticipantViewModel Participant { get; set; }
        public double Value { get; set; }
        public string DisplayValue { get; set; }
    }
}