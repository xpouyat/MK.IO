// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class Asset
    {
        public static Asset FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Asset>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public Asset(string container, string description, string storageAccountName)
        {
            Properties = new AssetProperties { Description = description, Container = container, StorageAccountName = storageAccountName };
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public AssetProperties Properties { get; set; }
    }
}