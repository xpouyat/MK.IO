// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IStreamingEndpointsOperations
    {
        /// <summary>
        /// Retrieves a list of Streaming Endpoints for the specified subscription.
        /// </summary>
        /// <returns></returns>
        List<StreamingEndpointSchema> List();

        /// <summary>
        /// Retrieves a list of Streaming Endpoints for the specified subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<StreamingEndpointSchema>> ListAsync();

        /// <summary>
        /// Delete a Streaming Endpoint. If the Streaming Endpoint does not exist, this API will return a 204.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        void Delete(string streamingEndpointName);

        /// <summary>
        /// Delete a Streaming Endpoint. If the Streaming Endpoint does not exist, this API will return a 204.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <returns></returns>
        Task DeleteAsync(string streamingEndpointName);

        /// <summary>
        /// Retrieves a single Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <returns></returns>
        StreamingEndpointSchema Get(string streamingEndpointName);

        /// <summary>
        /// Retrieves a single Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <returns></returns>
        Task<StreamingEndpointSchema> GetAsync(string streamingEndpointName);

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        /// <summary>
        /// Update a Streaming Endpoint.
        /// Only the Name and cdnProvider fields are immutable.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="location">The name of the location in which the Streaming Endpoint is located. This field must match the location in which the user's subscription is provisioned.</param>
        /// <param name="properties">The properties of the Streaming Endpoint.</param>
        /// <param name="tags">A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.</param>
        /// <returns></returns>
        StreamingEndpointSchema Update(string streamingEndpointName, string location, StreamingEndpointProperties properties, Dictionary<string, string>? tags = null);

        /// <summary>
        /// Update a Streaming Endpoint.
        /// Only the Name and cdnProvider fields are immutable.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="location">The name of the location in which the Streaming Endpoint is located. This field must match the location in which the user's subscription is provisioned.</param>
        /// <param name="properties">The properties of the Streaming Endpoint.</param>
        /// <param name="tags">A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.</param>
        /// <returns></returns>
        Task<StreamingEndpointSchema> UpdateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, Dictionary<string, string>? tags = null);
#endif

        /// <summary>
        /// Create a Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="location">The name of the location in which the Streaming Endpoint is located. This field must match the location in which the user's subscription is provisioned.</param>
        /// <param name="properties">The properties of the Streaming Endpoint.</param>
        /// <param name="autoStart"></param>
        /// <param name="tags">A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.</param>
        /// <returns></returns>
        StreamingEndpointSchema Create(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string>? tags = null);

        /// <summary>
        /// Create a Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="location">The name of the location in which the Streaming Endpoint is located. This field must match the location in which the user's subscription is provisioned.</param>
        /// <param name="properties">The properties of the Streaming Endpoint.</param>
        /// <param name="autoStart"></param>
        /// <param name="tags">A dictionary of key:value pairs describing the resource. Search may be implemented against tags in the future.</param>
        /// <returns></returns>
        Task<StreamingEndpointSchema> CreateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string>? tags = null);

        /// <summary>
        /// Changes the scale of the Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="scaleUnit">The scale unit count for this Streaming Endpoint.</param>
        void Scale(string streamingEndpointName, int scaleUnit);

        /// <summary>
        /// Changes the scale of the Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <param name="scaleUnit">The scale unit count for this Streaming Endpoint.</param>
        Task ScaleAsync(string streamingEndpointName, int scaleUnit);

        /// <summary>
        /// Start a Streaming Endpoint.
        /// This operation transitions your Streaming Endpoint into a running state.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        void Start(string streamingEndpointName);

        /// <summary>
        /// Start a Streaming Endpoint.
        /// This operation transitions your Streaming Endpoint into a running state.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        Task StartAsync(string streamingEndpointName);

        /// <summary>
        /// Stop a Streaming Endpoint. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        void Stop(string streamingEndpointName);

        /// <summary>
        /// Stop a Streaming Endpoint. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="streamingEndpointName">The name of the Streaming Endpoint.</param>
        /// <returns></returns>
        Task StopAsync(string streamingEndpointName);
    }
}