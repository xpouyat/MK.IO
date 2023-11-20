using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SelectVideoTrackById
    {
        /// <summary>
        /// The discriminator for derived types.
        /// </summary>
        /// <value>The discriminator for derived types.</value>
        [DataMember(Name = "@odata.type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "@odata.type")]
        public string OdataType { get; set; }

        /// <summary>
        /// Track indentifer to select
        /// </summary>
        /// <value>Track indentifer to select</value>
        [DataMember(Name = "trackId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "trackId")]
        public int? TrackId { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SelectVideoTrackById {\n");
            sb.Append("  OdataType: ").Append(OdataType).Append("\n");
            sb.Append("  TrackId: ").Append(TrackId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

    }
}
