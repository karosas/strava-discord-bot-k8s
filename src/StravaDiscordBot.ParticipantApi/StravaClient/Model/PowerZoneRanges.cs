/* 
 * Strava API v3
 *
 * The [Swagger Playground](https://developers.strava.com/playground) is the easiest way to familiarize yourself with the Strava API by submitting HTTP requests and observing the responses before you write any client code. It will show what a response will look like with different endpoints depending on the authorization scope you receive from your athletes. To use the Playground, go to https://www.strava.com/settings/api and change your “Authorization Callback Domain” to developers.strava.com. Please note, we only support Swagger 2.0. There is a known issue where you can only select one scope at a time. For more information, please check the section “client code” at https://developers.strava.com/docs.
 *
 * OpenAPI spec version: 3.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = StravaDiscordBot.ParticipantApi.StravaClient.Client.SwaggerDateConverter;

namespace StravaDiscordBot.ParticipantApi.StravaClient.Model
{
    /// <summary>
    /// PowerZoneRanges
    /// </summary>
    [DataContract]
    public partial class PowerZoneRanges :  IEquatable<PowerZoneRanges>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerZoneRanges" /> class.
        /// </summary>
        /// <param name="zones">zones.</param>
        public PowerZoneRanges(ZoneRanges zones = default(ZoneRanges))
        {
            this.Zones = zones;
        }
        
        /// <summary>
        /// Gets or Sets Zones
        /// </summary>
        [DataMember(Name="zones", EmitDefaultValue=false)]
        public ZoneRanges Zones { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PowerZoneRanges {\n");
            sb.Append("  Zones: ").Append(Zones).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as PowerZoneRanges);
        }

        /// <summary>
        /// Returns true if PowerZoneRanges instances are equal
        /// </summary>
        /// <param name="input">Instance of PowerZoneRanges to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PowerZoneRanges input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Zones == input.Zones ||
                    (this.Zones != null &&
                    this.Zones.Equals(input.Zones))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Zones != null)
                    hashCode = hashCode * 59 + this.Zones.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
