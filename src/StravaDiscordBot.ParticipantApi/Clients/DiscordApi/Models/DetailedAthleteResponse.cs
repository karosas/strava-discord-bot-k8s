// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.ParticipantApi.Clients.DiscordApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class DetailedAthleteResponse
    {
        /// <summary>
        /// Initializes a new instance of the DetailedAthleteResponse class.
        /// </summary>
        public DetailedAthleteResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DetailedAthleteResponse class.
        /// </summary>
        public DetailedAthleteResponse(long? id = default(long?), int? resourceState = default(int?), string firstname = default(string), string lastname = default(string), string profileMedium = default(string), string profile = default(string), string city = default(string), string state = default(string), string country = default(string), bool? premium = default(bool?), bool? summit = default(bool?), System.DateTime? createdAt = default(System.DateTime?), System.DateTime? updatedAt = default(System.DateTime?), int? followerCount = default(int?), int? friendCount = default(int?), int? ftp = default(int?), double? weight = default(double?))
        {
            Id = id;
            ResourceState = resourceState;
            Firstname = firstname;
            Lastname = lastname;
            ProfileMedium = profileMedium;
            Profile = profile;
            City = city;
            State = state;
            Country = country;
            Premium = premium;
            Summit = summit;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            FollowerCount = followerCount;
            FriendCount = friendCount;
            Ftp = ftp;
            Weight = weight;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "resourceState")]
        public int? ResourceState { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "firstname")]
        public string Firstname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "profileMedium")]
        public string ProfileMedium { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "profile")]
        public string Profile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "premium")]
        public bool? Premium { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "summit")]
        public bool? Summit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdAt")]
        public System.DateTime? CreatedAt { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "updatedAt")]
        public System.DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "followerCount")]
        public int? FollowerCount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "friendCount")]
        public int? FriendCount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ftp")]
        public int? Ftp { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "weight")]
        public double? Weight { get; set; }

    }
}