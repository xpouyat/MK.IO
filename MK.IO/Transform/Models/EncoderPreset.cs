// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using Newtonsoft.Json;
namespace MK.IO
{

    [JsonConverter(typeof(JsonSubtypes), "@odata.type")]
    [JsonSubtypes.KnownSubType(typeof(BuiltInStandardEncoderPreset), "#Microsoft.Media.BuiltInStandardEncoderPreset")]

    //
    // Summary:
    //     Base class for Encoder preset configuration. A derived class must be used
    //     to create a configuration.
    public class EncoderPreset
    {
        [JsonProperty("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}