// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationWidevine : ContentKeyPolicyConfigurationBase
    {
        [JsonProperty("widevineTemplate")]
        public string WidevineTemplate { get; set; }
    }
}