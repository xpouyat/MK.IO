﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MK.IO
{
    /// <summary>
    /// The resource state of the live output.
    /// </summary>
    /// <value>The current resource state of the live output.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LiveOutputResourceState
    {
        //
        // Summary:
        //     Creating live output.
        [EnumMember(Value = "Creating")]
        Creating,

        //
        // Summary:
        //     Deleting live output.
        [EnumMember(Value = "Deleting")]
        Deleting,

        //
        // Summary:
        //     Running live output.
        [EnumMember(Value = "Running")]
        Running,
    }
}