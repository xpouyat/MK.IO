// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
using Newtonsoft.Json;
#if NET462
using System.Net.Http;
#endif

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
        private const string _streamingLocatorsApiUrl = MKIOClient._streamingLocatorsApiUrl;
        private const string _streamingLocatorApiUrl = _streamingLocatorsApiUrl + "/{1}";
        private const string _streamingLocatorListPathsApiUrl = _streamingLocatorApiUrl + "/listPaths";

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
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<StreamingLocatorSchema> List()
        {
            var task = Task.Run(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<StreamingLocatorSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_streamingLocatorsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            var objectToReturn = JsonConvert.DeserializeObject<StreamingLocatorListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with streaming locator list deserialization");
        }

        /// <inheritdoc/>
        public StreamingLocatorSchema Get(string streamingLocatorName)
        {
            var task = Task.Run(async () => await GetAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName)
        {
            Argument.AssertNotNullOrEmpty(streamingLocatorName, nameof(streamingLocatorName));

            var url = Client.GenerateApiUrl(_streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming locator deserialization");
        }

        /// <inheritdoc/>
        public StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            var task = Task.Run(async () => await CreateAsync(streamingLocatorName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            Argument.AssertNotNullOrEmpty(streamingLocatorName, nameof(streamingLocatorName));
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_streamingLocatorApiUrl, streamingLocatorName);
            var content = new StreamingLocatorSchema { Properties = properties };
            string responseContent = await Client.CreateObjectPutAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming locator deserialization");
        }

        /// <inheritdoc/>
        public void Delete(string streamingLocatorName)
        {
            Task.Run(async () => await DeleteAsync(streamingLocatorName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string streamingLocatorName)
        {
            Argument.AssertNotNullOrEmpty(streamingLocatorName, nameof(streamingLocatorName));

            var url = Client.GenerateApiUrl(_streamingLocatorApiUrl, streamingLocatorName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName)
        {
            var task = Task.Run(async () => await ListUrlPathsAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName)
        {
            Argument.AssertNotNullOrEmpty(streamingLocatorName, nameof(streamingLocatorName));

            var url = Client.GenerateApiUrl(_streamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await Client.ObjectContentAsync(url, HttpMethod.Post);
            return JsonConvert.DeserializeObject<StreamingLocatorListPathsResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming locator list paths deserialization");
        }
    }
}