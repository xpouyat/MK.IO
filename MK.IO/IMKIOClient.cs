// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Asset;

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public interface IMKIOClient
    {
        /// <summary>
        /// Gets the ISubscriptionOperations.
        /// </summary>
        ISubscriptionOperations Subscription { get; }

        /// <summary>
        /// Gets the IAssetsOperations.
        /// </summary>
        IAssetsOperations Assets { get; }

        /// <summary>
        /// Gets the ILiveEventsOperations.
        /// </summary>
        ILiveEventsOperations LiveEvents { get; }

        /// <summary>
        /// Gets the IJobsOperations.
        /// </summary>
        IJobsOperations Jobs { get; }

        /// <summary>
        /// Gets the IStreamingEndpointsOperations.
        /// </summary>
        IStreamingEndpointsOperations StreamingEndpoints { get; }

        /// <summary>
        /// Gets the ITransformsOperations.
        /// </summary>
        ITransformsOperations Transforms { get; }

        /// <summary>
        /// Gets the IStreamingLocatorsOperations.
        /// </summary>
        IStreamingLocatorsOperations StreamingLocators { get; }

        /// <summary>
        /// Gets the IContentKeyPoliciesOperations.
        /// </summary>
        IContentKeyPoliciesOperations ContentKeyPolicies { get; }
    }
}