using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class StreamingEndpointAccessControl {
    /// <summary>
    /// Gets or Sets Akamai
    /// </summary>
    [DataMember(Name="akamai", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "akamai")]
    public StreamingEndpointAkamiACL Akamai { get; set; }

    /// <summary>
    /// Gets or Sets Ip
    /// </summary>
    [DataMember(Name="ip", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ip")]
    public StreamingEndpointIPACL Ip { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class StreamingEndpointAccessControl {\n");
      sb.Append("  Akamai: ").Append(Akamai).Append("\n");
      sb.Append("  Ip: ").Append(Ip).Append("\n");
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
