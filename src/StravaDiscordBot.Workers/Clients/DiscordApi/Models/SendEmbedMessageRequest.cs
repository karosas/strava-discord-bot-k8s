// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.Workers.Clients.DiscordApi.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class SendEmbedMessageRequest
    {
        /// <summary>
        /// Initializes a new instance of the SendEmbedMessageRequest class.
        /// </summary>
        public SendEmbedMessageRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SendEmbedMessageRequest class.
        /// </summary>
        public SendEmbedMessageRequest(IList<FieldViewModel> fields = default(IList<FieldViewModel>), string title = default(string))
        {
            Fields = fields;
            Title = title;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fields")]
        public IList<FieldViewModel> Fields { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

    }
}
