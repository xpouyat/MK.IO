// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{

    public class ObjectStorageList
    {
        public static ObjectStorageList FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ObjectStorageList>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("items")]
        public List<BaseObjectStorage> Items { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }
    }
}