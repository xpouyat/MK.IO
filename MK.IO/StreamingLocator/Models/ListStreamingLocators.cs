// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class ListStreamingLocators
    {
        public static ListStreamingLocators FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ListStreamingLocators>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<StreamingLocator> Value { get; set; }
    }
}