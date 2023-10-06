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
    internal class StreamingEndpointsOperations : IStreamingEndpointsOperations
    {
        //
        // streaming endpoints
        //
        private const string _streamingEndpointsApiUrl = MKIOClient._streamingEndpointsApiUrl;
        private const string _streamingEndpointApiUrl = _streamingEndpointsApiUrl + "/{1}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the StreamingEndpointsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal StreamingEndpointsOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<StreamingEndpointSchema> List()
        {
            var task = Task.Run<List<StreamingEndpointSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<StreamingEndpointSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_streamingEndpointsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingEndpointListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        /// <inheritdoc/>
        public StreamingEndpointSchema Get(string streamingEndpointName)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await GetAsync(streamingEndpointName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingEndpointSchema> GetAsync(string streamingEndpointName)
        {
            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl, streamingEndpointName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public StreamingEndpointSchema Create(string streamingEndpointName, string location, StreamingEndpointProperties content, bool autoStart = false, Dictionary<string, string> tags = null)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await CreateAsync(streamingEndpointName, location, content, autoStart, tags));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingEndpointSchema> CreateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string> tags = null)
        {
            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl + "?autoStart=" + autoStart.ToString(), streamingEndpointName);
            tags ??= new Dictionary<string, string>();
            var content = new StreamingEndpointSchema { Location = location, Properties = properties, Tags = tags };
            string responseContent = await Client.CreateObjectAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Stop(string streamingEndpointName)
        {
            Task.Run(async () => await StopAsync(streamingEndpointName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task StopAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "stop", HttpMethod.Post);
        }

        /// <inheritdoc/>
        public void Start(string streamingEndpointName)
        {
            Task.Run(async () => await StartAsync(streamingEndpointName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task StartAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, "start", HttpMethod.Post);
        }

        /// <inheritdoc/>
        public void Delete(string streamingEndpointName)
        {
            Task.Run(async () => await DeleteAsync(streamingEndpointName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string streamingEndpointName)
        {
            await StreamingEndpointOperationAsync(streamingEndpointName, null, HttpMethod.Delete);
        }

        private async Task StreamingEndpointOperationAsync(string streamingEndpointName, string? operation, HttpMethod httpMethod)
        {
            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl + (operation != null ? "/" + operation : string.Empty), streamingEndpointName);
            await Client.ObjectContentAsync(url, httpMethod);
        }
    }
}