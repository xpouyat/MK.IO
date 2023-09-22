// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
using Newtonsoft.Json;

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

        public List<StreamingEndpointSchema> ListStreamingEndpoints()
        {
            var task = Task.Run<List<StreamingEndpointSchema>>(async () => await ListStreamingEndpointsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StreamingEndpointSchema>> ListStreamingEndpointsAsync()
        {
            string URL = GenerateApiUrl(streamingEndpointsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingEndpointListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public StreamingEndpointSchema GetStreamingEndpoint(string streamingEndpointName)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await GetStreamingEndpointAsync(streamingEndpointName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingEndpointSchema> GetStreamingEndpointAsync(string streamingEndpointName)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl, streamingEndpointName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings);
        }

        public StreamingEndpointSchema CreateStreamingEndpoint(string streamingEndpointName, string location, Dictionary<string, string> tags, StreamingEndpointProperties content, bool autoStart = false)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await CreateStreamingEndpointAsync(streamingEndpointName, location, tags, content, autoStart));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingEndpointSchema> CreateStreamingEndpointAsync(string streamingEndpointName, string location, Dictionary<string, string> tags, StreamingEndpointProperties properties, bool autoStart = false)
        {
            string URL = GenerateApiUrl(streamingEndpointApiUrl + "?autoStart=" + autoStart.ToString(), streamingEndpointName);
            var content = new StreamingEndpointSchema { Location = location, Properties = properties, Tags = tags };
            string responseContent = await CreateObjectAsync(URL, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings);
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

        public string GenerateUniqueName(string prefix)
        {
            return prefix + "-" + Guid.NewGuid().ToString()[..13];
        }
    }
}