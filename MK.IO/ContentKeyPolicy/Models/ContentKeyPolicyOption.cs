// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyOption
    {

        public ContentKeyPolicyOption(string name,  ContentKeyPolicyConfiguration configuration, ContentKeyPolicyRestriction restriction)
        {
            Name = name;
            Restriction = restriction;
            Configuration = configuration;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("restriction")]
        public ContentKeyPolicyRestriction Restriction { get; set; }

        [JsonProperty("configuration")]
        public ContentKeyPolicyConfiguration Configuration { get; set; }
    }
}