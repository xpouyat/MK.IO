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
  public class JobInputAsset {
    /// <summary>
    /// The name of the input Asset
    /// </summary>
    /// <value>The name of the input Asset</value>
    [DataMember(Name="assetName", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "assetName")]
    public string AssetName { get; set; }

    /// <summary>
    /// Defines a point on the timeline of the input media at which processing will end. Defaults to the end of the input media.
    /// </summary>
    /// <value>Defines a point on the timeline of the input media at which processing will end. Defaults to the end of the input media.</value>
    [DataMember(Name="end", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "end")]
    public OneOfJobInputAssetEnd End { get; set; }

    /// <summary>
    /// List of files. Required for JobInputAsset.
    /// </summary>
    /// <value>List of files. Required for JobInputAsset.</value>
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
    public OneOfJobInputAssetStart Start { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class JobInputAsset {\n");
      sb.Append("  AssetName: ").Append(AssetName).Append("\n");
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
