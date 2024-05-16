// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(AudioTrackDescriptor), "AudioTrackDescriptor")]
    [JsonSubtypes.KnownSubType(typeof(SelectAudioTrackByAttribute), "SelectAudioTrackByAttribute")]
    [JsonSubtypes.KnownSubType(typeof(SelectAudioTrackById), "SelectAudioTrackById")]
    [JsonSubtypes.KnownSubType(typeof(SelectVideoTrackByAttribute), "SelectVideoTrackByAttribute")]
    [JsonSubtypes.KnownSubType(typeof(SelectVideoTrackById), "SelectVideoTrackById")]
    [JsonSubtypes.KnownSubType(typeof(VideoTrackDescriptor), "VideoTrackDescriptor")]

    /// <summary>
    /// 
    /// </summary>

    public class TrackDiscriminator
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }

    }
}
