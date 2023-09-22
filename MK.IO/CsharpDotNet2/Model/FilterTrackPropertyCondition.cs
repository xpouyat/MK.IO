using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class FilterTrackPropertyCondition
    {
        /// <summary>
        /// Set to either 'Equal' or 'NotEqual'. Tracks matching this predicate will be included.                  Examples:         To include all audio tracks, set operation to 'Equal', property to 'Type', and value to 'Audio'.         
        /// </summary>
        /// <value>Set to either 'Equal' or 'NotEqual'. Tracks matching this predicate will be included.                  Examples:         To include all audio tracks, set operation to 'Equal', property to 'Type', and value to 'Audio'.         </value>
        [DataMember(Name = "operation", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }

        /// <summary>
        /// The property to match against. Supported properties include:         Bitrate, FourCC, Language, Name, Type, and Unknown.           Bitrate may be provided as a single numeric value '100000' or as a range '500000-1000000'.         FourCC is the codec, and may be one of 'avc1', 'hev1', and 'hvc1' for video, or one of 'mp4a' and 'ec-3' for audio.         Language is the value of a language tag to include, as specified in RFC 5646.         Name is the name of the track as specified in the manifest.         Type is one of either 'video', 'audio', or 'text'.                  
        /// </summary>
        /// <value>The property to match against. Supported properties include:         Bitrate, FourCC, Language, Name, Type, and Unknown.           Bitrate may be provided as a single numeric value '100000' or as a range '500000-1000000'.         FourCC is the codec, and may be one of 'avc1', 'hev1', and 'hvc1' for video, or one of 'mp4a' and 'ec-3' for audio.         Language is the value of a language tag to include, as specified in RFC 5646.         Name is the name of the track as specified in the manifest.         Type is one of either 'video', 'audio', or 'text'.                  </value>
        [DataMember(Name = "property", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "property")]
        public string Property { get; set; }

        /// <summary>
        /// The value to match against. Values are not case-sensitive.
        /// </summary>
        /// <value>The value to match against. Values are not case-sensitive.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FilterTrackPropertyCondition {\n");
            sb.Append("  Operation: ").Append(Operation).Append("\n");
            sb.Append("  Property: ").Append(Property).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
