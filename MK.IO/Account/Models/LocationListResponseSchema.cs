// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public partial class LocationListResponseSchema
    {
        public static LocationListResponseSchema FromJson(string json)
        {
            return JsonConvert.DeserializeObject<LocationListResponseSchema>(json, ConverterLE.Settings) ?? throw new Exception("Error with location deserialization");
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }


        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("items")]
        public List<LocationResponseSchema> Items { get; set; }
    }
}