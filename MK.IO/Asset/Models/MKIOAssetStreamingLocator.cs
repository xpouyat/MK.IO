// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{
    public class MKIOAssetStreamingLocator
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("streamingLocatorId")]
        public Guid? StreamingLocatorId { get; set; }

        [JsonProperty("streamingPolicyName")]
        public string? StreamingPolicyName { get; set; }

        [JsonProperty("defaultContentKeyPolicyName")]
        public string? DefaultContentKeyPolicyName { get; set; }

        [JsonProperty("contentKeys")]
        public List<object>? ContentKeys { get; set; }

        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }
    }
}