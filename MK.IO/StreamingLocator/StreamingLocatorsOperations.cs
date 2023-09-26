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
    internal class StreamingLocatorsOperations : IStreamingLocatorsOperations
    {
        //
        // streaming locators
        //
        private const string StreamingLocatorsApiUrl = MKIOClient.StreamingLocatorsApiUrl;
        private const string StreamingLocatorApiUrl = StreamingLocatorsApiUrl + "/{1}";
        private const string StreamingLocatorListPathsApiUrl = StreamingLocatorApiUrl + "/listPaths";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the StreamingLocatorsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal StreamingLocatorsOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }


        /// <inheritdoc/>
        public List<StreamingLocatorSchema> List()
        {
            var task = Task.Run<List<StreamingLocatorSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<StreamingLocatorSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(StreamingLocatorsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingLocatorListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        /// <inheritdoc/>
        public StreamingLocatorSchema Get(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await GetAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName)
        {
            var url = Client.GenerateApiUrl(StreamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await CreateAsync(streamingLocatorName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            var url = Client.GenerateApiUrl(StreamingLocatorApiUrl, streamingLocatorName);
            var content = new StreamingLocatorSchema { Properties = properties };
            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Delete(string streamingLocatorName)
        {
            Task.Run(async () => await DeleteAsync(streamingLocatorName));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string streamingLocatorName)
        {
            var url = Client.GenerateApiUrl(StreamingLocatorApiUrl, streamingLocatorName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorListPathsResponseSchema>(async () => await ListUrlPathsAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName)
        {
            var url = Client.GenerateApiUrl(StreamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await Client.ObjectContentAsync(url, HttpMethod.Post);
            return JsonConvert.DeserializeObject<StreamingLocatorListPathsResponseSchema>(responseContent, ConverterLE.Settings);
        }
    }
}