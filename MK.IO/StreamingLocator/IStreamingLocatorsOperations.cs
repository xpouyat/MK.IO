// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
using System.Runtime.ConstrainedExecution;

namespace MK.IO
{
    public interface IStreamingLocatorsOperations
    {
        /// <summary>
        /// Returns a list of streaming locators for the subscription.
        /// </summary>
        /// <returns></returns>
        List<StreamingLocatorSchema> List();

        /// <summary>
        /// Returns a list of streaming locators for the subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<StreamingLocatorSchema>> ListAsync();

        /// <summary>
        /// Delete a streaming locator.
        /// If the streaming locator does not exist, a 204 is returned same as if it does.
        /// Once the streaming locator is deleted, all future requests to the path owned by the streaming locator will fail. If content is
        /// cached in a CDN, playback may continue to work for some time until the cached content expires.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// /// <returns></returns>
        void Delete(string streamingLocatorName);

        /// <summary>
        /// Delete a streaming locator.
        /// If the streaming locator does not exist, a 204 is returned same as if it does.
        /// Once the streaming locator is deleted, all future requests to the path owned by the streaming locator will fail. If content is
        /// cached in a CDN, playback may continue to work for some time until the cached content expires.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <returns></returns>
        Task DeleteAsync(string streamingLocatorName);

        /// <summary>
        /// Get a streaming locator by name.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <returns></returns>
        StreamingLocatorSchema Get(string streamingLocatorName);

        /// <summary>
        /// Get a streaming locator by name.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <returns></returns>
        Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName);

        /// <summary>
        /// Create a streaming locator.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties);

        /// <summary>
        /// Create a streaming locator.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties);

        /// <summary>
        /// Return the set of valid streaming paths for a given streaming locator. A distinct set of paths
        /// is returned for each type of DRM configured, per the 'EnabledProtocols' property of the streaming policy.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <returns></returns>
        StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName);

        /// <summary>
        /// Return the set of valid streaming paths for a given streaming locator. A distinct set of paths
        /// is returned for each type of DRM configured, per the 'EnabledProtocols' property of the streaming policy.
        /// </summary>
        /// <param name="streamingLocatorName"></param>
        /// <returns></returns>
        Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName);
    }
}