// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MK.IO
{
    /// <summary>
    /// The current state of the job.
    /// </summary>
    /// <value>The current state of the job.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobState
    {
        //
        // Summary:
        //     The job was canceled. This is a final state for the job.
        [EnumMember(Value = "Canceled")]
        Canceled = 1,

        //
        // Summary:
        //     The job is in the process of being canceled. This is a transient state for the
        //     job.
        [EnumMember(Value = "Canceling")]
        Canceling = 2,

        //
        // Summary:
        //     The job has encountered an error. This is a final state for the job.
        [EnumMember(Value = "Error")]
        Error = 3,

        //
        // Summary:
        //     The job is finished. This is a final state for the job.
        [EnumMember(Value = "Finished")]
        Finished = 4,

        //
        // Summary:
        //     The job is processing. This is a transient state for the job.
        [EnumMember(Value = "Processing")]
        Processing = 5,

        //
        // Summary:
        //     The job is in a queued state, waiting for resources to become available. This
        //     is a transient state.
        [EnumMember(Value = "Queued")]
        Queued = 6,

        //
        // Summary:
        //     The job is being scheduled to run on an available resource. This is a transient
        //     state, between queued and processing states.
        [EnumMember(Value = "Scheduled")]
        Scheduled = 7
    }
}