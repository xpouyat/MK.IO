using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class LiveOutputSchema
    {
        /// <summary>
        /// The fully qualified resource ID for the live output. This is a GUID auto-generated at creation time.
        /// </summary>
        /// <value>The fully qualified resource ID for the live output. This is a GUID auto-generated at creation time.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the live output. This is a user-defined value that must be unique within the live event. If supplied in the 
        /// </summary>
        /// <value>The name of the live output. This is a user-defined value that must be unique within the live event. If supplied in the </value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Properties
        /// </summary>
        [DataMember(Name = "properties", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "properties")]
        public LiveOutputProperties Properties { get; set; }

        /// <summary>
        /// The type of the live output. This is always Microsoft.Media/mediaservices/liveOutputs.
        /// </summary>
        /// <value>The type of the live output. This is always Microsoft.Media/mediaservices/liveOutputs.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LiveOutputSchema {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
