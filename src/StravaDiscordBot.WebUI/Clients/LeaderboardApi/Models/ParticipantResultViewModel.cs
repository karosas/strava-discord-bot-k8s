// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.WebUI.Clients.LeaderboardApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ParticipantResultViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ParticipantResultViewModel class.
        /// </summary>
        public ParticipantResultViewModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ParticipantResultViewModel class.
        /// </summary>
        public ParticipantResultViewModel(ParticipantViewModel participant = default(ParticipantViewModel), double? value = default(double?), string displayValue = default(string))
        {
            Participant = participant;
            Value = value;
            DisplayValue = displayValue;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "participant")]
        public ParticipantViewModel Participant { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public double? Value { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "displayValue")]
        public string DisplayValue { get; set; }

    }
}
