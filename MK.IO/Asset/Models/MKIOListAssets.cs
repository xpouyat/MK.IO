// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using Newtonsoft.Json;
using System.Collections.Generic;

namespace MK.IO.Models
{

    public class MKIOListAssets
    {
        public static MKIOListAssets FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOListAssets>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("value")]
        public List<MKIOAsset> Value { get; set; }
    }
}