// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(FromEachInputFile), "FromEachInputFile")]
    [JsonSubtypes.KnownSubType(typeof(FromAllInputFile), "FromAllInputFile")]
    [JsonSubtypes.KnownSubType(typeof(InputFile), "InputFile")]

    /// <summary>
    /// 
    /// </summary>

    public class InputFileDiscriminator
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}
