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
using SwaggerDateConverter = StravaDiscordBot.StravaAPI.StravaClient.Client.SwaggerDateConverter;

namespace StravaDiscordBot.StravaAPI.StravaClient.Model
{
    /// <summary>
    /// ZoneRange
    /// </summary>
    [DataContract]
    public partial class ZoneRange :  IEquatable<ZoneRange>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneRange" /> class.
        /// </summary>
        /// <param name="min">The minimum value in the range..</param>
        /// <param name="max">The maximum value in the range..</param>
        public ZoneRange(int? min = default(int?), int? max = default(int?))
        {
            this.Min = min;
            this.Max = max;
        }
        
        /// <summary>
        /// The minimum value in the range.
        /// </summary>
        /// <value>The minimum value in the range.</value>
        [DataMember(Name="min", EmitDefaultValue=false)]
        public int? Min { get; set; }

        /// <summary>
        /// The maximum value in the range.
        /// </summary>
        /// <value>The maximum value in the range.</value>
        [DataMember(Name="max", EmitDefaultValue=false)]
        public int? Max { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ZoneRange {\n");
            sb.Append("  Min: ").Append(Min).Append("\n");
            sb.Append("  Max: ").Append(Max).Append("\n");
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
            return this.Equals(input as ZoneRange);
        }

        /// <summary>
        /// Returns true if ZoneRange instances are equal
        /// </summary>
        /// <param name="input">Instance of ZoneRange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ZoneRange input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Min == input.Min ||
                    (this.Min != null &&
                    this.Min.Equals(input.Min))
                ) && 
                (
                    this.Max == input.Max ||
                    (this.Max != null &&
                    this.Max.Equals(input.Max))
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
                if (this.Min != null)
                    hashCode = hashCode * 59 + this.Min.GetHashCode();
                if (this.Max != null)
                    hashCode = hashCode * 59 + this.Max.GetHashCode();
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