// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.Workers.Clients.DiscordApi.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class StravaOauthResponse
    {
        /// <summary>
        /// Initializes a new instance of the StravaOauthResponse class.
        /// </summary>
        public StravaOauthResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the StravaOauthResponse class.
        /// </summary>
        public StravaOauthResponse(string accessToken = default(string), string refreshToken = default(string))
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "refreshToken")]
        public string RefreshToken { get; set; }

    }
}
