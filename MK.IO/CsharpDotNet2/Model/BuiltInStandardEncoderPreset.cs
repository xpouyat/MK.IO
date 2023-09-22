using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BuiltInStandardEncoderPreset
    {
        /// <summary>
        /// The discriminator for derived types. Must be set to #Microsoft.Media.BuiltInStandardEncoderPreset
        /// </summary>
        /// <value>The discriminator for derived types. Must be set to #Microsoft.Media.BuiltInStandardEncoderPreset</value>
        [DataMember(Name = "odatatype", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "odatatype")]
        public string Odatatype { get; set; }

        /// <summary>
        /// The built-in preset to be used for encoding videos.
        /// </summary>
        /// <value>The built-in preset to be used for encoding videos.</value>
        [DataMember(Name = "presetName", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "presetName")]
        public string PresetName { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BuiltInStandardEncoderPreset {\n");
            sb.Append("  Odatatype: ").Append(Odatatype).Append("\n");
            sb.Append("  PresetName: ").Append(PresetName).Append("\n");
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
