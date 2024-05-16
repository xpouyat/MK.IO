// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(JobInputAsset), "#Microsoft.Media.JobInputAsset")]
    [JsonSubtypes.KnownSubType(typeof(JobInputHttp), "#Microsoft.Media.JobInputHttp")]

    //
    // Summary:
    //     Base class for Job Input. A derived class must be used
    //     to create a configuration.
    public class JobInput
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}