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
  public class LiveOutputHls {
    /// <summary>
    /// The number of fragments per HLS segment.
    /// </summary>
    /// <value>The number of fragments per HLS segment.</value>
    [DataMember(Name="fragmentsPerTsSegment", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "fragmentsPerTsSegment")]
    public int? FragmentsPerTsSegment { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LiveOutputHls {\n");
      sb.Append("  FragmentsPerTsSegment: ").Append(FragmentsPerTsSegment).Append("\n");
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
