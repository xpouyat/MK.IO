// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MK.IO
{
    /// <summary>
    /// The resource state of the live event.
    /// </summary>
    /// <value>The current resource state of the live event.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LiveEventResourceState
    {
        //
        // Summary:
        //     Stopped live event.
        [EnumMember(Value = "Stopped")]
        Stopped,

        //
        // Summary:
        //     Starting live event.
        [EnumMember(Value = "Starting")]
        Starting,

        //
        // Summary:
        //     Running live event.
        [EnumMember(Value = "Running")]
        Running,

        //
        // Summary:
        //     Stopping live event.
        [EnumMember(Value = "Stopping")]
        Stopping,

        //
        // Summary:
        //     Deleting live event.
        [EnumMember(Value = "Deleting")]
        Deleting,
    }
}