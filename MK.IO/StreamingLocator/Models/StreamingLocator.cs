// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class StreamingLocator
    {
        public static StreamingLocator FromJson(string json)
        {
            return JsonConvert.DeserializeObject<StreamingLocator>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public StreamingLocator(string assetName, string streamingPolicyName, List<object>? contentKeys = null, DateTime? endTime = null, Guid? streamingLocatorId = null)
        {
            Properties = new StreamingLocatorProperties { AssetName = assetName, StreamingPolicyName = streamingPolicyName, ContentKeys = contentKeys, EndTime = endTime, StreamingLocatorId = streamingLocatorId };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public StreamingLocatorProperties Properties { get; set; }
    }
}