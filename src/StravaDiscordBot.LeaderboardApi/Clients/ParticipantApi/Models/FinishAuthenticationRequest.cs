// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class FinishAuthenticationRequest
    {
        /// <summary>
        /// Initializes a new instance of the FinishAuthenticationRequest
        /// class.
        /// </summary>
        public FinishAuthenticationRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FinishAuthenticationRequest
        /// class.
        /// </summary>
        public FinishAuthenticationRequest(string code = default(string), string participantId = default(string), string leaderboardId = default(string))
        {
            Code = code;
            ParticipantId = participantId;
            LeaderboardId = leaderboardId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "participantId")]
        public string ParticipantId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "leaderboardId")]
        public string LeaderboardId { get; set; }

    }
}
