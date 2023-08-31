// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using Newtonsoft.Json;
using System.Collections.Generic;

namespace MK.IO.Models
{

    public class MKIOAssetListStreamingLocators
    {
        public static MKIOAssetListStreamingLocators FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOAssetListStreamingLocators>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("streamingLocators")]
        public List<MKIOAssetStreamingLocator> StreamingLocators { get; set; }
    }
}