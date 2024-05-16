// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(UtcClipTime), "#Microsoft.Media.UtcClipTime")]
    [JsonSubtypes.KnownSubType(typeof(AbsoluteClipTime), "#Microsoft.Media.AbsoluteClipTime")]

    //
    // Summary:
    //     Base class for Job Start. A derived class must be used
    //     to create a configuration.
    public class JobInputTime
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}