// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class MKIOListStreamingLocators
    {
        public static MKIOListStreamingLocators FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOListStreamingLocators>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<MKIOStreamingLocator> Value { get; set; }
    }
}