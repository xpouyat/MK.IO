// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class ListContentKeyPolicies
    {
        public static ListContentKeyPolicies FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ListContentKeyPolicies>(json, ConverterLE.Settings) ?? throw new Exception("Error with content key policies deserialization");
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<ContentKeyPolicy> Value { get; set; }
    }
}