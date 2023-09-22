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
  public class AudioTrackDescriptor {
    /// <summary>
    /// Optional designation for single channel audio tracks.
    /// </summary>
    /// <value>Optional designation for single channel audio tracks.</value>
    [DataMember(Name="channelMapping", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "channelMapping")]
    public string ChannelMapping { get; set; }

    /// <summary>
    /// The discriminator for derived types.
    /// </summary>
    /// <value>The discriminator for derived types.</value>
    [DataMember(Name="odatatype", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "odatatype")]
    public string Odatatype { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class AudioTrackDescriptor {\n");
      sb.Append("  ChannelMapping: ").Append(ChannelMapping).Append("\n");
      sb.Append("  Odatatype: ").Append(Odatatype).Append("\n");
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
