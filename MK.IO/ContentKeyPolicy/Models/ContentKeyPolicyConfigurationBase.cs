// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace MK.IO
{

    [JsonConverter(typeof(JsonSubtypes), "@odata.type")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationPlayReady), "#Microsoft.Media.ContentKeyPolicyPlayReadyConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationWidevine), "#Microsoft.Media.ContentKeyPolicyWidevineConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationFairPlay), "#Microsoft.Media.ContentKeyPolicyFairPlayConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyClearKeyConfiguration), "#Microsoft.Media.ContentKeyPolicyClearKeyConfiguration")]

    public class ContentKeyPolicyConfigurationBase
    {
        [JsonProperty("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}