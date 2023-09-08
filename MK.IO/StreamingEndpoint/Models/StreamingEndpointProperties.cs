// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;

namespace MK.IO
{
    public class StreamingEndpointProperties
    {
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("lastModified")]
        public DateTime? LastModified { get; set; }

        [JsonProperty("provisioningState")]
        public string ProvisioningState { get; set; }

        [JsonProperty("resourceState")]
        public string ResourceState { get; set; }

        [JsonProperty("scaleUnits")]
        public int? ScaleUnits { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("accessControl")]
        public object AccessControl { get; set; }

        [JsonProperty("cdnEnabled")]
        public bool? CdnEnabled { get; set; }

        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("customHostNames")]
        public List<string> CustomHostNames { get; set; }

        [JsonProperty("maxCacheAge")]
        public int? MaxCacheAge { get; set; }

        [JsonProperty("crossSiteAccessPolicies")]
        public object CrossSiteAccessPolicies { get; set; }

        [JsonProperty("sku")]
        public StreamingEndpointSku Sku { get; set; }
    }


    public class StreamingEndpointRel
    {
        [JsonProperty("audits")]
        public List<Audit> Audits { get; set; }
    }

    public class Diff
    {
        [JsonProperty("properties")]
        public StreamingEndpointProperties Properties { get; set; }
    }

    public class Ip
    {
        [JsonProperty("allow")]
        public List<Allow> Allow { get; set; }
    }

    public class Audit
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("streaming_endpoint_id")]
        public Guid StreamingEndpointId { get; set; }

        [JsonProperty("diff")]
        public Diff Diff { get; set; }
    }

    public class AccessControl
    {
        [JsonProperty("ip")]
        public Ip Ip { get; set; }
    }

    public class Allow
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}