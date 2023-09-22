using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ContentKeyPolicyOption
    {
        /// <summary>
        /// The key delivery type.
        /// </summary>
        /// <value>The key delivery type.</value>
        [DataMember(Name = "configuration", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "configuration")]
        public Dictionary<string, Object> Configuration { get; set; }

        /// <summary>
        /// The name of the option
        /// </summary>
        /// <value>The name of the option</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The policy option ID
        /// </summary>
        /// <value>The policy option ID</value>
        [DataMember(Name = "policyOptionId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "policyOptionId")]
        public string PolicyOptionId { get; set; }

        /// <summary>
        /// The restriction for the option
        /// </summary>
        /// <value>The restriction for the option</value>
        [DataMember(Name = "restriction", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "restriction")]
        public Dictionary<string, Object> Restriction { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContentKeyPolicyOption {\n");
            sb.Append("  Configuration: ").Append(Configuration).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  PolicyOptionId: ").Append(PolicyOptionId).Append("\n");
            sb.Append("  Restriction: ").Append(Restriction).Append("\n");
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
