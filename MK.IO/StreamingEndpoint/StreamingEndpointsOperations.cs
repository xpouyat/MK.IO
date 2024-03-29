﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;
#if NET462
using System.Net.Http;
#endif

namespace MK.IO.Operations
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
            var objectToReturn = JsonConvert.DeserializeObject<StreamingEndpointListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with streaming endpoint list deserialization");
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
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));

            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl, streamingEndpointName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming endpoint deserialization");
        }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        /// <inheritdoc/>
        public StreamingEndpointSchema Update(string streamingEndpointName, string location, StreamingEndpointProperties properties, Dictionary<string, string>? tags = null)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await UpdateAsync(streamingEndpointName, location, properties, tags));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingEndpointSchema> UpdateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, Dictionary<string, string>? tags = null)
        {
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));
            Argument.AssertNotContainsSpace(streamingEndpointName, nameof(streamingEndpointName));
            Argument.AssertNotNullOrEmpty(location, nameof(location));
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl, streamingEndpointName);
            tags ??= new Dictionary<string, string>();
            var content = new StreamingEndpointSchema { Location = location, Properties = properties, Tags = tags };
            string responseContent = await Client.UpdateObjectPatchAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming endpoint deserialization");
        }
#endif

        /// <inheritdoc/>
        public StreamingEndpointSchema Create(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string>? tags = null)
        {
            var task = Task.Run<StreamingEndpointSchema>(async () => await CreateAsync(streamingEndpointName, location, properties, autoStart, tags));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingEndpointSchema> CreateAsync(string streamingEndpointName, string location, StreamingEndpointProperties properties, bool autoStart = false, Dictionary<string, string>? tags = null)
        {
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));
            Argument.AssertNotContainsSpace(streamingEndpointName, nameof(streamingEndpointName));
            Argument.AssertNotMoreThanLength(streamingEndpointName, nameof(streamingEndpointName), 24);
            Argument.AssertRespectRegex(streamingEndpointName, nameof(streamingEndpointName), @"^[a-zA-Z0-9]+$");
            Argument.AssertNotNullOrEmpty(location, nameof(location));
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl + "?autoStart=" + autoStart.ToString(), streamingEndpointName);
            tags ??= new Dictionary<string, string>();
            var content = new StreamingEndpointSchema { Location = location, Properties = properties, Tags = tags };
            string responseContent = await Client.CreateObjectPutAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<StreamingEndpointSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming endpoint deserialization");
        }

        /// <inheritdoc/>
        public void Scale(string streamingEndpointName, int scaleUnit)
        {
            Task.Run(async () => await ScaleAsync(streamingEndpointName, scaleUnit)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task ScaleAsync(string streamingEndpointName, int scaleUnit)
        {
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));
            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl + "/scale", streamingEndpointName);
            var content = new StreamingEndpointScaleSchema { ScaleUnit = scaleUnit };
            await Client.CreateObjectPostAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
        }

        /// <inheritdoc/>
        public void Stop(string streamingEndpointName)
        {
            Task.Run(async () => await StopAsync(streamingEndpointName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task StopAsync(string streamingEndpointName)
        {
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));

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
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));

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
            Argument.AssertNotNullOrEmpty(streamingEndpointName, nameof(streamingEndpointName));

            await StreamingEndpointOperationAsync(streamingEndpointName, null, HttpMethod.Delete);
        }

        private async Task StreamingEndpointOperationAsync(string streamingEndpointName, string? operation, HttpMethod httpMethod)
        {
            var url = Client.GenerateApiUrl(_streamingEndpointApiUrl + (operation != null ? "/" + operation : string.Empty), streamingEndpointName);
            await Client.ObjectContentAsync(url, httpMethod);
        }
    }
}