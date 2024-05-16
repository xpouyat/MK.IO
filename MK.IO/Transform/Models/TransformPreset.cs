// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(BuiltInStandardEncoderPreset), "#Microsoft.Media.BuiltInStandardEncoderPreset")]
    [JsonSubtypes.KnownSubType(typeof(AudioAnalyzerPreset), "#Microsoft.Media.AudioAnalyzerPreset")]

    //
    // Summary:
    //     Base class for Transform Output preset configuration. A derived class must be used
    //     to create a configuration.
    public class TransformPreset
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}