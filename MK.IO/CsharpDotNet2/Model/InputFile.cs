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
  public class InputFile {
    /// <summary>
    /// Name of the file that this input definition applies to.
    /// </summary>
    /// <value>Name of the file that this input definition applies to.</value>
    [DataMember(Name="filename", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "filename")]
    public string Filename { get; set; }

    /// <summary>
    /// The list of TrackDescriptors which define the metadata and selection of tracks in the input.
    /// </summary>
    /// <value>The list of TrackDescriptors which define the metadata and selection of tracks in the input.</value>
    [DataMember(Name="includedTracks", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "includedTracks")]
    public List<TrackDiscriminator> IncludedTracks { get; set; }

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
      sb.Append("class InputFile {\n");
      sb.Append("  Filename: ").Append(Filename).Append("\n");
      sb.Append("  IncludedTracks: ").Append(IncludedTracks).Append("\n");
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
