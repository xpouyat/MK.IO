// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;

namespace MK.IO
{

    public partial class StreamingEndpoint
    {
        public static StreamingEndpoint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<StreamingEndpoint>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        public StreamingEndpoint(string location, string description, StreamingEndpointSku sku, int scaleUnits, bool cdnEnabled)
        {
            Location = location;
            Properties = new StreamingEndpointProperties { Description = description, Sku = sku, ScaleUnits = scaleUnits, CdnEnabled = cdnEnabled };
            Tags = new Dictionary<string, string>();
        }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty("properties")]
        public StreamingEndpointProperties Properties { get; set; }

        [JsonProperty("rel")]
        public StreamingEndpointRel Rel { get; set; }
    }
}

