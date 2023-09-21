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
  public class StreamingLocatorListResponse {
    /// <summary>
    /// List of Streaming Locators
    /// </summary>
    /// <value>List of Streaming Locators</value>
    [DataMember(Name="streamingLocators", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "streamingLocators")]
    public List<StreamingLocatorSchema> StreamingLocators { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class StreamingLocatorListResponse {\n");
      sb.Append("  StreamingLocators: ").Append(StreamingLocators).Append("\n");
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
