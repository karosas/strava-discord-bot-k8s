// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.Workers.Clients.DiscordApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class StartAuthenticationRequest
    {
        /// <summary>
        /// Initializes a new instance of the StartAuthenticationRequest class.
        /// </summary>
        public StartAuthenticationRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the StartAuthenticationRequest class.
        /// </summary>
        public StartAuthenticationRequest(string redirectUrl = default(string))
        {
            RedirectUrl = redirectUrl;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "redirectUrl")]
        public string RedirectUrl { get; set; }

    }
}
