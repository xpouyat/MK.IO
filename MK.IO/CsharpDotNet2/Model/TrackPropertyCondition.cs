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
  public class TrackPropertyCondition {
    /// <summary>
    /// Track property operation. Only equality is supported.
    /// </summary>
    /// <value>Track property operation. Only equality is supported.</value>
    [DataMember(Name="operation", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "operation")]
    public string Operation { get; set; }

    /// <summary>
    /// Track property type
    /// </summary>
    /// <value>Track property type</value>
    [DataMember(Name="property", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "property")]
    public string Property { get; set; }

    /// <summary>
    /// Track property value
    /// </summary>
    /// <value>Track property value</value>
    [DataMember(Name="value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class TrackPropertyCondition {\n");
      sb.Append("  Operation: ").Append(Operation).Append("\n");
      sb.Append("  Property: ").Append(Property).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
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
