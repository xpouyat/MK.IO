// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace MK.IO.Models
{

    public class MKIOStreamingEndpoint
    {
        public static MKIOStreamingEndpoint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOStreamingEndpoint>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public MKIOStreamingEndpoint(string location, string description, MKIOStreamingEndpointSku sku, int scaleUnits, bool cdnEnabled)
        {
            Location = location;
            Properties = new MKIOStreamingEndpointProperties { Description = description, Sku = sku, ScaleUnits = scaleUnits, CdnEnabled = cdnEnabled };
            Tags = new Dictionary<string, string>();
        }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty("properties")]
        public MKIOStreamingEndpointProperties Properties { get; set; }
    }
}