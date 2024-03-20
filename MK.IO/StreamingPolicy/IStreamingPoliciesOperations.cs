// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IStreamingPoliciesOperations
    {
        /// <summary>
        /// Returns a list of Streaming Policies for the subscription.
        /// </summary>
        /// <returns></returns>
        List<StreamingPolicySchema> List();

        /// <summary>
        /// Returns a list of Streaming Policies for the subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<StreamingPolicySchema>> ListAsync();

        /// <summary>
        /// Delete a Streaming Policy.
        /// If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <returns></returns>
        void Delete(string streamingPolicyName);

        /// <summary>
        /// Delete a Streaming Policy.
        /// If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <returns></returns>
        Task DeleteAsync(string streamingPolicyName);

        /// <summary>
        /// Get a Streaming Policy by name.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <returns></returns>
        StreamingPolicySchema Get(string streamingPolicyName);

        /// <summary>
        /// Get a Streaming Policy by name.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <returns></returns>
        Task<StreamingPolicySchema> GetAsync(string streamingPolicyName);

        /// <summary>
        /// Create a Streaming Policy.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <param name="properties">Properties for Streaming Policy</param>
        /// <returns></returns>
        StreamingPolicySchema Create(string streamingPolicyName, StreamingPolicyProperties properties);

        /// <summary>
        /// Create a Streaming Policy.
        /// </summary>
        /// <param name="streamingPolicyName">The name of the Streaming Policy.</param>
        /// <param name="properties">Properties for Streaming Policy</param>
        /// <returns></returns>
        Task<StreamingPolicySchema> CreateAsync(string streamingPolicyName, StreamingPolicyProperties properties);
    }
}