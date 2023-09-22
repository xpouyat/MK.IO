using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MK.IO.Models {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UtcClipTime {
    /// <summary>
    /// The discriminator for derived types.
    /// </summary>
    /// <value>The discriminator for derived types.</value>
    [DataMember(Name="odatatype", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "odatatype")]
    public string Odatatype { get; set; }

    /// <summary>
    /// The time position on the timeline of the input media based on Utc time.
    /// </summary>
    /// <value>The time position on the timeline of the input media based on Utc time.</value>
    [DataMember(Name="time", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "time")]
    public string Time { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UtcClipTime {\n");
      sb.Append("  Odatatype: ").Append(Odatatype).Append("\n");
      sb.Append("  Time: ").Append(Time).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
