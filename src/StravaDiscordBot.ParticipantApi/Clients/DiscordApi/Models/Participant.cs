// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.ParticipantApi.Clients.DiscordApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Participant
    {
        /// <summary>
        /// Initializes a new instance of the Participant class.
        /// </summary>
        public Participant()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Participant class.
        /// </summary>
        public Participant(long? id = default(long?), long? stravaId = default(long?), long? leaderboardId = default(long?))
        {
            Id = id;
            StravaId = stravaId;
            LeaderboardId = leaderboardId;
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
        [JsonProperty(PropertyName = "stravaId")]
        public long? StravaId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "leaderboardId")]
        public long? LeaderboardId { get; set; }

    }
}
