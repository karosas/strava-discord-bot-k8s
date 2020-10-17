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
    /// TimeStream
    /// </summary>
    [DataContract]
    public partial class TimeStream :  IEquatable<TimeStream>, IValidatableObject
    {
        /// <summary>
        /// The level of detail (sampling) in which this stream was returned
        /// </summary>
        /// <value>The level of detail (sampling) in which this stream was returned</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ResolutionEnum
        {
            
            /// <summary>
            /// Enum Low for value: low
            /// </summary>
            [EnumMember(Value = "low")]
            Low = 1,
            
            /// <summary>
            /// Enum Medium for value: medium
            /// </summary>
            [EnumMember(Value = "medium")]
            Medium = 2,
            
            /// <summary>
            /// Enum High for value: high
            /// </summary>
            [EnumMember(Value = "high")]
            High = 3
        }

        /// <summary>
        /// The level of detail (sampling) in which this stream was returned
        /// </summary>
        /// <value>The level of detail (sampling) in which this stream was returned</value>
        [DataMember(Name="resolution", EmitDefaultValue=false)]
        public ResolutionEnum? Resolution { get; set; }
        /// <summary>
        /// The base series used in the case the stream was downsampled
        /// </summary>
        /// <value>The base series used in the case the stream was downsampled</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SeriesTypeEnum
        {
            
            /// <summary>
            /// Enum Distance for value: distance
            /// </summary>
            [EnumMember(Value = "distance")]
            Distance = 1,
            
            /// <summary>
            /// Enum Time for value: time
            /// </summary>
            [EnumMember(Value = "time")]
            Time = 2
        }

        /// <summary>
        /// The base series used in the case the stream was downsampled
        /// </summary>
        /// <value>The base series used in the case the stream was downsampled</value>
        [DataMember(Name="series_type", EmitDefaultValue=false)]
        public SeriesTypeEnum? SeriesType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeStream" /> class.
        /// </summary>
        /// <param name="originalSize">The number of data points in this stream.</param>
        /// <param name="resolution">The level of detail (sampling) in which this stream was returned.</param>
        /// <param name="seriesType">The base series used in the case the stream was downsampled.</param>
        /// <param name="data">The sequence of time values for this stream, in seconds.</param>
        public TimeStream(int? originalSize = default(int?), ResolutionEnum? resolution = default(ResolutionEnum?), SeriesTypeEnum? seriesType = default(SeriesTypeEnum?), List<int?> data = default(List<int?>))
        {
            this.OriginalSize = originalSize;
            this.Resolution = resolution;
            this.SeriesType = seriesType;
            this.Data = data;
        }
        
        /// <summary>
        /// The number of data points in this stream
        /// </summary>
        /// <value>The number of data points in this stream</value>
        [DataMember(Name="original_size", EmitDefaultValue=false)]
        public int? OriginalSize { get; set; }



        /// <summary>
        /// The sequence of time values for this stream, in seconds
        /// </summary>
        /// <value>The sequence of time values for this stream, in seconds</value>
        [DataMember(Name="data", EmitDefaultValue=false)]
        public List<int?> Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TimeStream {\n");
            sb.Append("  OriginalSize: ").Append(OriginalSize).Append("\n");
            sb.Append("  Resolution: ").Append(Resolution).Append("\n");
            sb.Append("  SeriesType: ").Append(SeriesType).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
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
            return this.Equals(input as TimeStream);
        }

        /// <summary>
        /// Returns true if TimeStream instances are equal
        /// </summary>
        /// <param name="input">Instance of TimeStream to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TimeStream input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.OriginalSize == input.OriginalSize ||
                    (this.OriginalSize != null &&
                    this.OriginalSize.Equals(input.OriginalSize))
                ) && 
                (
                    this.Resolution == input.Resolution ||
                    (this.Resolution != null &&
                    this.Resolution.Equals(input.Resolution))
                ) && 
                (
                    this.SeriesType == input.SeriesType ||
                    (this.SeriesType != null &&
                    this.SeriesType.Equals(input.SeriesType))
                ) && 
                (
                    this.Data == input.Data ||
                    this.Data != null &&
                    this.Data.SequenceEqual(input.Data)
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
                if (this.OriginalSize != null)
                    hashCode = hashCode * 59 + this.OriginalSize.GetHashCode();
                if (this.Resolution != null)
                    hashCode = hashCode * 59 + this.Resolution.GetHashCode();
                if (this.SeriesType != null)
                    hashCode = hashCode * 59 + this.SeriesType.GetHashCode();
                if (this.Data != null)
                    hashCode = hashCode * 59 + this.Data.GetHashCode();
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
