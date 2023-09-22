using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class TransformProperties
    {
        /// <summary>
        /// The creation date and time of the Transform. Set by the system.
        /// </summary>
        /// <value>The creation date and time of the Transform. Set by the system.</value>
        [DataMember(Name = "created", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// The description of the Transform.
        /// </summary>
        /// <value>The description of the Transform.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The last modified date and time of the Transform. Set by the system.
        /// </summary>
        /// <value>The last modified date and time of the Transform. Set by the system.</value>
        [DataMember(Name = "lastModified", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lastModified")]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// An array of one or more TransformOutputs that the Transform should generate.
        /// </summary>
        /// <value>An array of one or more TransformOutputs that the Transform should generate.</value>
        [DataMember(Name = "outputs", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "outputs")]
        public List<TransformOutput> Outputs { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransformProperties {\n");
            sb.Append("  Created: ").Append(Created).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  LastModified: ").Append(LastModified).Append("\n");
            sb.Append("  Outputs: ").Append(Outputs).Append("\n");
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
