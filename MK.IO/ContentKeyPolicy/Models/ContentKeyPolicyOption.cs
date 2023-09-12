// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("restriction")]
        public ContentKeyPolicyRestriction Restriction { get; set; }

        [JsonProperty("configuration")]
        public ContentKeyPolicyConfigurationBase Configuration { get; set; }
    }
}