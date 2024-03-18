// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IStreamingLocatorsOperations
    {
        /// <summary>
        /// Returns a list of Streaming Locators for the subscription.
        /// </summary>
        /// <returns></returns>
        List<StreamingLocatorSchema> List();

        /// <summary>
        /// Returns a list of Streaming Locators for the subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<StreamingLocatorSchema>> ListAsync();

        /// <summary>
        /// Delete a Streaming Locator.
        /// If the Streaming Locator does not exist, a 204 is returned same as if it does.
        /// Once the Streaming Locator is deleted, all future requests to the path owned by the Streaming Locator will fail. If content is
        /// cached in a CDN, playback may continue to work for some time until the cached content expires.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// /// <returns></returns>
        void Delete(string streamingLocatorName);

        /// <summary>
        /// Delete a Streaming Locator.
        /// If the Streaming Locator does not exist, a 204 is returned same as if it does.
        /// Once the Streaming Locator is deleted, all future requests to the path owned by the Streaming Locator will fail. If content is
        /// cached in a CDN, playback may continue to work for some time until the cached content expires.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <returns></returns>
        Task DeleteAsync(string streamingLocatorName);

        /// <summary>
        /// Get a Streaming Locator by name.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <returns></returns>
        StreamingLocatorSchema Get(string streamingLocatorName);

        /// <summary>
        /// Get a Streaming Locator by name.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <returns></returns>
        Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName);

        /// <summary>
        /// Create a Streaming Locator.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <param name="properties">Properties for Streaming Locator</param>
        /// <returns></returns>
        StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties);

        /// <summary>
        /// Create a Streaming Locator.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <param name="properties">Properties for Streaming Locator</param>
        /// <returns></returns>
        Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties);

        /// <summary>
        /// ListPaths returns the set of valid streaming paths for a given Streaming Locator. A distinct set of paths
        /// is returned for each type of DRM configured, per the 'EnabledProtocols' property of the streaming policy.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <returns></returns>
        StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName);

        /// <summary>
        /// ListPaths returns the set of valid streaming paths for a given Streaming Locator. A distinct set of paths
        /// is returned for each type of DRM configured, per the 'EnabledProtocols' property of the streaming policy.
        /// </summary>
        /// <param name="streamingLocatorName">The name of the Streaming Locator.</param>
        /// <returns></returns>
        Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName);
    }
}