// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{

    public class ListStreamingEndpoint
    {
        public static ListStreamingEndpoint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ListStreamingEndpoint>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<StreamingEndpoint> Value { get; set; }
    }
}