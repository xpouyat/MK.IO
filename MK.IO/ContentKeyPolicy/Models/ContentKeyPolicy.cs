// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MK.IO
{

    public partial class ContentKeyPolicy
    {
        public ContentKeyPolicy() { }


        public ContentKeyPolicy(ContentKeyPolicyProperties properties)
        {
            Properties = properties;
        }

        public ContentKeyPolicy(string description, List<ContentKeyPolicyOption> contentKeyPolicyOptions)
        {
            Properties = new ContentKeyPolicyProperties { Description = description, Options = contentKeyPolicyOptions };
        }

        public static ContentKeyPolicy FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ContentKeyPolicy>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty("properties")]
        public ContentKeyPolicyProperties Properties { get; set; }

    }

    public class ContentKeyPolicyProperties
    {
        [JsonProperty("options")]
        public List<ContentKeyPolicyOption> Options { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("policyId")]
        public Guid? PolicyId { get; set; }
    }
}