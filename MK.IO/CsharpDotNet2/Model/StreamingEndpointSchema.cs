using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class StreamingEndpointSchema
    {
        /// <summary>
        /// A path to access the resource.
        /// </summary>
        /// <value>A path to access the resource.</value>
        [DataMember(Name = "idx", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "idx")]
        public string Idx { get; set; }

        /// <summary>
        /// The name of the location in which the streaming endpoint is located. This field must match the location in which the user's subscription is provisioned.
        /// </summary>
        /// <value>The name of the location in which the streaming endpoint is located. This field must match the location in which the user's subscription is provisioned.</value>
        [DataMember(Name = "location", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// The name of the streamingEndpoint resource. This is provided in the URL when a resources is created, and is immutable.
        /// </summary>
        /// <value>The name of the streamingEndpoint resource. This is provided in the URL when a resources is created, and is immutable.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Properties
        /// </summary>
        [DataMember(Name = "properties", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "properties")]
        public StreamingEndpointProperties Properties { get; set; }

        /// <summary>
        /// A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.
        /// </summary>
        /// <value>A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.</value>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "tags")]
        public Dictionary<string, string> Tags { get; set; }

        /// <summary>
        /// The resource type. Not used for anything
        /// </summary>
        /// <value>The resource type. Not used for anything</value>
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
            sb.Append("class StreamingEndpointSchema {\n");
            sb.Append("  Idx: ").Append(Idx).Append("\n");
            sb.Append("  Location: ").Append(Location).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
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
