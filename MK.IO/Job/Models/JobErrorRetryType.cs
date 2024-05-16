// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace MK.IO.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobErrorRetryType
    {
        /// <summary>
        /// Enum JobErrorRetryType for value: DoNotRetry
        /// </summary>
        DoNotRetry,

        /// <summary>
        /// Enum JobErrorRetryType for value: MayRetry
        /// </summary>
        MayRetry
    }
}