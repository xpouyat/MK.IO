// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyTokenRestriction), "#Microsoft.Media.ContentKeyPolicyTokenRestriction")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyOpenRestriction), "#Microsoft.Media.ContentKeyPolicyOpenRestriction")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyUnknownRestriction), "#Microsoft.Media.ContentKeyPolicyUnknownRestriction")]

    //
    // Summary:
    //     Base class for Content Key Policy restrictions. A derived class must be used
    //     to create a restriction.
    public class ContentKeyPolicyRestriction
    {

        [JsonPropertyName("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}
