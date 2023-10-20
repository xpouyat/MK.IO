// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public partial class SubscriptionListResponseSchema
    {
        public static SubscriptionListResponseSchema FromJson(string json)
        {
            return JsonConvert.DeserializeObject<SubscriptionListResponseSchema>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }


        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("items")]
        public List<SubscriptionResponseSchema> Items { get; set; }
    }
}