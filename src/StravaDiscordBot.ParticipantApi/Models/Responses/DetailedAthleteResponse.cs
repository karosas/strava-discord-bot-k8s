using System;
using AutoMapper;
using StravaDiscordBot.ParticipantApi.StravaClient.Model;

namespace StravaDiscordBot.ParticipantApi.Models.Responses
{
    [AutoMap(typeof(DetailedAthlete))]
    public class DetailedAthleteResponse
    {
        public ulong Id { get; set; }
        public int ResourceState { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfileMedium { get; set; }
        public string Profile { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool Premium { get; set; }
        public bool Summit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int FollowerCount { get; set; }
        public int FriendCount { get; set; }
        public int Ftp { get; set; }
        public float Weight { get; set; }
    }
}