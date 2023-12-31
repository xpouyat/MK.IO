﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using JsonSubTypes;
using Newtonsoft.Json;
namespace MK.IO
{

    [JsonConverter(typeof(JsonSubtypes), "@odata.type")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationPlayReady), "#Microsoft.Media.ContentKeyPolicyPlayReadyConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationWidevine), "#Microsoft.Media.ContentKeyPolicyWidevineConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyConfigurationFairPlay), "#Microsoft.Media.ContentKeyPolicyFairPlayConfiguration")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyClearKeyConfiguration), "#Microsoft.Media.ContentKeyPolicyClearKeyConfiguration")]

    //
    // Summary:
    //     Base class for Content Key Policy configuration. A derived class must be used
    //     to create a configuration.
    public class ContentKeyPolicyConfiguration
    {
        [JsonProperty("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}