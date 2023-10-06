// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary> Defines values for JobState. </summary>
    public readonly partial struct JobState
    {
        //
        // Summary:
        //     The job was canceled. This is a final state for the job.
        public static readonly string Canceled = "Canceled";

        //
        // Summary:
        //     The job is in the process of being canceled. This is a transient state for the
        //     job.
        public static readonly string Canceling = "Canceling";

        //
        // Summary:
        //     The job has encountered an error. This is a final state for the job.
        public static readonly string Error = "Error";

        //
        // Summary:
        //     The job is finished. This is a final state for the job.
        public static readonly string Finished = "Finished";

        //
        // Summary:
        //     The job is processing. This is a transient state for the job.
        public static readonly string Processing = "Processing";

        //
        // Summary:
        //     The job is in a queued state, waiting for resources to become available. This
        //     is a transient state.
        public static readonly string Queued = "Queued";

        //
        // Summary:
        //     The job is being scheduled to run on an available resource. This is a transient
        //     state, between queued and processing states.
        public static readonly string Scheduled = "Scheduled";
    }
}