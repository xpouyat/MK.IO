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
  public class JobInputHttp {
    /// <summary>
    /// Base URI for HTTPS job input. It will be concatenated with provided file names. If no base uri is given, then the provided file list is assumed to be fully qualified uris.
    /// </summary>
    /// <value>Base URI for HTTPS job input. It will be concatenated with provided file names. If no base uri is given, then the provided file list is assumed to be fully qualified uris.</value>
    [DataMember(Name="baseUri", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "baseUri")]
    public string BaseUri { get; set; }

    /// <summary>
    /// Defines a point on the timeline of the input media at which processing will end. Defaults to the end of the input media.
    /// </summary>
    /// <value>Defines a point on the timeline of the input media at which processing will end. Defaults to the end of the input media.</value>
    [DataMember(Name="end", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "end")]
    public OneOfJobInputHttpEnd End { get; set; }

    /// <summary>
    /// List of files. Required for JobInputHttp.
    /// </summary>
    /// <value>List of files. Required for JobInputHttp.</value>
    [DataMember(Name="files", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "files")]
    public List<string> Files { get; set; }

    /// <summary>
    /// Defines a list of InputDefinitions
    /// </summary>
    /// <value>Defines a list of InputDefinitions</value>
    [DataMember(Name="inputDefinitions", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "inputDefinitions")]
    public List<InputFileDiscriminator> InputDefinitions { get; set; }

    /// <summary>
    /// A label that is assigned to a JobInputClip
    /// </summary>
    /// <value>A label that is assigned to a JobInputClip</value>
    [DataMember(Name="label", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "label")]
    public string Label { get; set; }

    /// <summary>
    /// The discriminator for derived types.
    /// </summary>
    /// <value>The discriminator for derived types.</value>
    [DataMember(Name="odatatype", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "odatatype")]
    public string Odatatype { get; set; }

    /// <summary>
    /// Defines a point on the timeline of the input media at which processing will start. Defaults to the beginning of the input media.
    /// </summary>
    /// <value>Defines a point on the timeline of the input media at which processing will start. Defaults to the beginning of the input media.</value>
    [DataMember(Name="start", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "start")]
    public OneOfJobInputHttpStart Start { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class JobInputHttp {\n");
      sb.Append("  BaseUri: ").Append(BaseUri).Append("\n");
      sb.Append("  End: ").Append(End).Append("\n");
      sb.Append("  Files: ").Append(Files).Append("\n");
      sb.Append("  InputDefinitions: ").Append(InputDefinitions).Append("\n");
      sb.Append("  Label: ").Append(Label).Append("\n");
      sb.Append("  Odatatype: ").Append(Odatatype).Append("\n");
      sb.Append("  Start: ").Append(Start).Append("\n");
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
