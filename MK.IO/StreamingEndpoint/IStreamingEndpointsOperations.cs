// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IStreamingEndpointsOperations
    {
        StreamingEndpointSchema Create(string streamingEndpointName, string location, StreamingEndpointProperties content, bool autoStart = false, Dictionary<string, string> tags = null);
        Task<StreamingEndpointSchema> CreateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string> tags = null);
        void Delete(string streamingEndpointName);
        Task DeleteAsync(string streamingEndpointName);
        StreamingEndpointSchema Get(string streamingEndpointName);
        Task<StreamingEndpointSchema> GetAsync(string streamingEndpointName);
        List<StreamingEndpointSchema> List();
        Task<List<StreamingEndpointSchema>> ListAsync();
        void Start(string streamingEndpointName);
        Task StartAsync(string streamingEndpointName);
        void Stop(string streamingEndpointName);
        Task StopAsync(string streamingEndpointName);
    }
}