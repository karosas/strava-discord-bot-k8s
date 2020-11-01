// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.WebUI.Clients.LeaderboardApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class LeaderboardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the LeaderboardViewModel class.
        /// </summary>
        public LeaderboardViewModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the LeaderboardViewModel class.
        /// </summary>
        public LeaderboardViewModel(long? id = default(long?), long? channelId = default(long?))
        {
            Id = id;
            ChannelId = channelId;
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
        [JsonProperty(PropertyName = "channelId")]
        public long? ChannelId { get; set; }

    }
}