// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader), "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyPlayReadyContentEncryptionKeyFromKeyIdentifier), "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromKeyIdentifier")]

    //
    // Summary:
    //     Base class for PlayReady content key location. A derived class must be used
    //     to specify a location.
    public class PlayReadyContentKeyLocation
    {
        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}
