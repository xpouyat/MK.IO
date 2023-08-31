// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class MKIOAsset
    {
        public static MKIOAsset FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOAsset>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public MKIOAsset(string container, string description, string storageAccountName)
        {
            Properties = new MKIOAssetProperties { Description = description, Container = container, StorageAccountName = storageAccountName };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public MKIOAssetProperties Properties { get; set; }
    }
}