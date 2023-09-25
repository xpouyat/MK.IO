// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Asset;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public interface IMKIOClient
    {
        ISubscriptionOperations Subscription { get; }
        IAssetsOperations Assets { get; }
        ILiveEventsOperations LiveEvents { get; }
        IJobsOperations Jobs { get; }
        IStreamingEndpointsOperations StreamingEndpoints { get; }
        ITransformsOperations Transforms { get; }
        IStreamingLocatorsOperations StreamingLocators { get; }
        IContentKeyPoliciesOperations ContentKeyPolicies { get; }
    }
}