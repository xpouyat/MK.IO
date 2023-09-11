// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;

namespace MK.IO
{

    public partial class ObjectStorage : BaseObjectStorage
    {
        public ObjectStorage(StorageSpec storage) : base(storage)
        {
        }

        public static ObjectStorage FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ObjectStorage>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }
              
        [JsonProperty("rel")]
        public StorageRel Rel { get; set; }
    }


    public class StorageRel
    {
        [JsonProperty("location")]
        public StorageLocation Location { get; set; }

        [JsonProperty("sasTokenCount")]
        public int SasTokenCount { get; set; }

        [JsonProperty("activeCredential")]
        public StorageCredential ActiveCredential { get; set; }
    }

    public class StorageLocation
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class StorageCredential
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("spec")]
        public Spec Spec { get; set; }

        [JsonProperty("supplemental")]
        public Supplemental Supplemental { get; set; }
    }

    public class Supplemental
    {
        [JsonProperty("signed_start")]
        public DateTime SignedStart { get; set; }

        [JsonProperty("signed_expiry")]
        public DateTime SignedExpiry { get; set; }

        [JsonProperty("test_result")]
        public TestResult TestResult { get; set; }
    }

    public class TestResult
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}