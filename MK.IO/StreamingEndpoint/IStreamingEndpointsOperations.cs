// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IStreamingEndpointsOperations
    {
        /// <summary>
        /// Retrieves a list of streaming endpoints for the specified subscription.
        /// </summary>
        /// <returns></returns>
        List<StreamingEndpointSchema> List();

        /// <summary>
        /// Retrieves a list of streaming endpoints for the specified subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<StreamingEndpointSchema>> ListAsync();

        /// <summary>
        /// Delete a Streaming Endpoint. If the Streaming Endpoint does not exist, this API will return a 204.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        void Delete(string streamingEndpointName);

        /// <summary>
        /// Delete a Streaming Endpoint. If the Streaming Endpoint does not exist, this API will return a 204.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <returns></returns>
        Task DeleteAsync(string streamingEndpointName);

        /// <summary>
        /// Retrieves a single Streaming Endpoint record.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <returns></returns>
        StreamingEndpointSchema Get(string streamingEndpointName);

        /// <summary>
        /// Retrieves a single Streaming Endpoint record.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <returns></returns>
        Task<StreamingEndpointSchema> GetAsync(string streamingEndpointName);

        /// <summary>
        /// Create a Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <param name="location"></param>
        /// <param name="content"></param>
        /// <param name="autoStart"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        StreamingEndpointSchema Create(string streamingEndpointName, string location, StreamingEndpointProperties content, bool autoStart = false, Dictionary<string, string>? tags = null);

        /// <summary>
        /// Create a Streaming Endpoint.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <param name="location"></param>
        /// <param name="properties"></param>
        /// <param name="autoStart"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<StreamingEndpointSchema> CreateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string>? tags = null);

        /// <summary>
        /// Start Streaming Endpoints.
        /// This operation transitions your streaming endpoints into a running state.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        void Start(string streamingEndpointName);

        /// <summary>
        /// Start Streaming Endpoints.
        /// This operation transitions your streaming endpoints into a running state.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        Task StartAsync(string streamingEndpointName);

        /// <summary>
        /// Stops a streaming endpoint. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        void Stop(string streamingEndpointName);

        /// <summary>
        /// Stops a streaming endpoint. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="streamingEndpointName"></param>
        /// <returns></returns>
        Task StopAsync(string streamingEndpointName);
    }
}