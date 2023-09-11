// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{

    public class StorageCredentialList
    {
        public static StorageCredentialList FromJson(string json)
        {
            return JsonConvert.DeserializeObject<StorageCredentialList>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("items")]
        public List<StorageCredential> Items { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }
    }
}