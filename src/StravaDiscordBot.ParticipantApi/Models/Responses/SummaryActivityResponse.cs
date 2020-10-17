using System;
using AutoMapper;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Models.Responses
{
    [AutoMap(typeof(SummaryActivity))]
    public class SummaryActivityResponse
    {
        public ulong Id { get; set; }
        public string ExternalId { get; set; }
        public ulong UploadId { get; set; }
        public string Name { get; set; }
        public float Distance { get; set; }
        public int MovingTime { get; set; }
        public int ElapsedTime { get; set; }
        public float TotalElevationGain { get; set; }
        public float ElevHigh { get; set; }
        public float ElevLow { get; set; }
        public ActivityType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartDateLocal { get; set; }
        public string Timezone { get; set; }
        public int AchievementCount { get; set; }
        public int KudosCount { get; set; }
        public int CommentCount { get; set; }
        public int AthleteCount { get; set; }
        public int PhotoCount { get; set; }
        public int TotalPhotoCount { get; set; }
        public bool Trainer { get; set; }
        public bool Commute { get; set; }
        public bool Manual { get; set; }
        public bool Private { get; set; }
        public bool Flagged { get; set; }
        public int WorkoutType { get; set; }
        public string UploadIdStr { get; set; }
        public float AverageSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public bool HasKudoed { get; set; }
        public string GearId { get; set; }
        public float Kilojoules { get; set; }
        public float AverageWatts { get; set; }
        public bool DeviceWatts { get; set; }
        public int MaxWatts { get; set; }
        public int WeightedAverageWatts { get; set; }
    }
}