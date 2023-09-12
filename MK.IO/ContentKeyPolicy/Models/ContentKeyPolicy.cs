// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;

namespace MK.IO
{

    public partial class ContentKeyPolicy
    {
        public static ContentKeyPolicy FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ContentKeyPolicy>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("properties")]
        public ContentKeyPolicyProperties Properties { get; set; }

    }

    public class ContentKeyPolicyProperties
    {
        [JsonProperty("options")]
        public List<ContentKeyPolicyOption> Options { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}