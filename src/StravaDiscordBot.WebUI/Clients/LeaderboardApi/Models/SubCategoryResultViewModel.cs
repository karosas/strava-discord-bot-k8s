// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.WebUI.Clients.LeaderboardApi.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class SubCategoryResultViewModel
    {
        /// <summary>
        /// Initializes a new instance of the SubCategoryResultViewModel class.
        /// </summary>
        public SubCategoryResultViewModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SubCategoryResultViewModel class.
        /// </summary>
        public SubCategoryResultViewModel(string name = default(string), IList<ParticipantResultViewModel> orderedParticipantResults = default(IList<ParticipantResultViewModel>))
        {
            Name = name;
            OrderedParticipantResults = orderedParticipantResults;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "orderedParticipantResults")]
        public IList<ParticipantResultViewModel> OrderedParticipantResults { get; set; }

    }
}