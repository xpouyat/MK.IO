using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MK.IO.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SelectAudioTrackByAttribute : TrackDiscriminator
    {
        public SelectAudioTrackByAttribute(string attribute, string filter, string filterValue)
        {
            Attribute = attribute;
            Filter = filter;
            FilterValue = filterValue;
        }

        /// <summary>
        /// The discriminator for derived types.
        /// </summary>
        /// <value>The discriminator for derived types.</value>
        [JsonProperty(PropertyName = "@odata.type")]
        internal override string OdataType => "SelectAudioTrackByAttribute";

        /// <summary>
        /// The TrackAttribute to filter the tracks by.
        /// </summary>
        /// <value>The TrackAttribute to filter the tracks by.</value>
        [DataMember(Name = "attribute", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "attribute")]
        public string Attribute { get; set; }

        /// <summary>
        /// Optional designation for single channel audio tracks.
        /// </summary>
        /// <value>Optional designation for single channel audio tracks.</value>
        [DataMember(Name = "channelMapping", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "channelMapping")]
        public string ChannelMapping { get; set; }

        /// <summary>
        /// The type of AttributeFilter to apply to the TrackAttribute in order to select the tracks.
        /// </summary>
        /// <value>The type of AttributeFilter to apply to the TrackAttribute in order to select the tracks.</value>
        [DataMember(Name = "filter", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "filter")]
        public string Filter { get; set; }

        /// <summary>
        /// The value to filter the tracks by.
        /// </summary>
        /// <value>The value to filter the tracks by.</value>
        [DataMember(Name = "filterValue", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "filterValue")]
        public string FilterValue { get; set; }
    }
}
