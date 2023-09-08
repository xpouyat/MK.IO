// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClient
    {
        //
        // streaming endpoints
        //
        private const string streamingEndpointsApiUrl = "api/ams/{0}/streamingEndpoints";
        private const string streamingEndpointApiUrl = streamingEndpointsApiUrl + "/{1}";

        public List<StreamingEndpoint> ListStreamingEndpoints()
        {
            Task<List<StreamingEndpoint>> task = Task.Run<List<StreamingEndpoint>>(async () => await ListStreamingEndpointsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StreamingEndpoint>> ListStreamingEndpointsAsync()
        {
            string URL = GenerateApiUrl(streamingEndpointsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return ListStreamingEndpoint.FromJson(responseContent).Value;
        }

        public StreamingEndpoint GetStreamingEndpoint(string streamingEndpointName)
        {
            Task<StreamingEndpoint> task = Task.Run<StreamingEndpoint>(async () => await GetStreamingEndpointAsync(streamingEndpointName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingEndpoint> GetStreamingEndpointAsync(string streamingEndpointName)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl, streamingEndpointName);
            string responseContent = await GetObjectContentAsync(URL);
            return StreamingEndpoint.FromJson(responseContent);
        }

        public StreamingEndpoint CreateStreamingEndpoint(string streamingEndpointName, StreamingEndpoint content, bool autoStart = true)
        {
            Task<StreamingEndpoint> task = Task.Run<StreamingEndpoint>(async () => await CreateStreamingEndpointAsync(streamingEndpointName, content, autoStart));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingEndpoint> CreateStreamingEndpointAsync(string streamingEndpointName, StreamingEndpoint content, bool autoStart = true)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl + "?autoStart=" + autoStart.ToString(), streamingEndpointName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return StreamingEndpoint.FromJson(responseContent);
        }

        public void StopStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await StopStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task StopStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "stop", HttpMethod.Post);
        }

        public void StartStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await StartStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task StartStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "start", HttpMethod.Post);
        }

        public void DeleteStreamingEndpoint(string streamingEndpointName)
        {
            Task.Run(async () => await DeleteStreamingEndpointAsync(streamingEndpointName));
        }

        public async Task DeleteStreamingEndpointAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, null, HttpMethod.Delete);
        }

        private async Task StreamingEndpointOperationAsync(string streamingEndpointName, string? operation, HttpMethod httpMethod)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl + (operation != null ? "/" + operation : string.Empty), streamingEndpointName);
            await ObjectContentAsync(URL, httpMethod);
        }
    }
}