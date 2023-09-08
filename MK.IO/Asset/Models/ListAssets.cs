// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using Newtonsoft.Json;
using System.Collections.Generic;

namespace MK.IO.Models
{

    public class ListAssets
    {
        public static ListAssets FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ListAssets>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<Asset> Value { get; set; }
    }
}