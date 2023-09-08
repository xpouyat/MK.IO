// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{
    public class AssetProperties
    {
        [JsonProperty("assetId")]
        public string AssetId { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("lastModified")]
        public DateTime? LastModified { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("container")]
        public string Container { get; set; }

        [JsonProperty("storageAccountName")]
        public string StorageAccountName { get; set; }

        [JsonProperty("storageEncryptionFormat")]
        public string StorageEncryptionFormat { get; set; }

        [JsonProperty("encryptionScope")]
        public string EncryptionScope { get; set; }
    }
}