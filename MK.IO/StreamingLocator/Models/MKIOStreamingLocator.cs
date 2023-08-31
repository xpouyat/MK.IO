// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class MKIOStreamingLocator
    {
        public static MKIOStreamingLocator FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOStreamingLocator>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public MKIOStreamingLocator(string assetName, string streamingPolicyName, List<object>? contentKeys = null, DateTime? endTime = null, Guid? streamingLocatorId = null)
        {
            Properties = new MKIOStreamingLocatorProperties { AssetName = assetName, StreamingPolicyName = streamingPolicyName, ContentKeys = contentKeys, EndTime = endTime, StreamingLocatorId = streamingLocatorId };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public MKIOStreamingLocatorProperties Properties { get; set; }
    }
}