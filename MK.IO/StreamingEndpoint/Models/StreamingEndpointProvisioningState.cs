// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MK.IO
{
    /// <summary>
    /// The provisioning state of the streaming endpoint.
    /// </summary>
    /// <value>The provisioning state of the streaming endpoint.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StreamingEndpointProvisioningState
    {
        [EnumMember(Value = "InProgress")]
        InProgress,

        [EnumMember(Value = "Succeeded")]
        Succeeded,

        [EnumMember(Value = "Failed")]
        Failed
    }
}