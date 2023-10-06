// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MK.IO
{

    public class SubscriptionResponseSchema
    {
        public static SubscriptionResponseSchema FromJson(string json)
        {
            return JsonConvert.DeserializeObject<SubscriptionResponseSchema>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("metadata")]
        public MetadataSubscription Metadata { get; set; }

        [JsonProperty("spec")]
        public SubscriptionSchema Spec { get; set; }
    }

    public class MetadataSubscription
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}