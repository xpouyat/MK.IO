// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary> The encoding types for live event. </summary>
    public readonly partial struct LiveEventEncodingType
    {

        /// <summary>
        /// The ingested stream passes through the live event from the contribution encoder.
        /// </summary>
        public static readonly string PassthroughBasic = "PassthroughBasic";

        /// <summary>
        /// The ingested stream passes through the live event from the contribution encoder.
        /// </summary>
        public static readonly string PassthroughStandard = "PassthroughStandard";

        /// <summary>
        /// A live encoder transcodes the incoming stream into multiple bitrates or layers
        /// </summary>
        public static readonly string Standard = "Standard ";

        /// <summary>
        /// A live encoder transcodes the incoming stream into multiple bitrates or layers
        /// </summary>
        public static readonly string Premium1080p = "Premium1080p ";
    }
}