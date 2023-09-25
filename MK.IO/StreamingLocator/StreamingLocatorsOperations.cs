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
    public class StreamingLocatorsOperations : IStreamingLocatorsOperations
    {
        //
        // streaming locators
        //
        private const string streamingLocatorsApiUrl = MKIOClient.streamingLocatorsApiUrl;
        private const string streamingLocatorApiUrl = streamingLocatorsApiUrl + "/{1}";
        private const string streamingLocatorListPathsApiUrl = streamingLocatorApiUrl + "/listPaths";

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


        public List<StreamingLocatorSchema> List()
        {
            var task = Task.Run<List<StreamingLocatorSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StreamingLocatorSchema>> ListAsync()
        {
            string URL = Client.GenerateApiUrl(streamingLocatorsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingLocatorListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        public StreamingLocatorSchema Get(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await GetAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName)
        {
            string URL = Client.GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
        }

        public StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await CreateAsync(streamingLocatorName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            string URL = Client.GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            var content = new StreamingLocatorSchema { Properties = properties };
            string responseContent = await Client.CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
        }

        public void Delete(string streamingLocatorName)
        {
            Task.Run(async () => await DeleteAsync(streamingLocatorName));
        }

        public async Task DeleteAsync(string streamingLocatorName)
        {
            string URL = Client.GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            await Client.ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorListPathsResponseSchema>(async () => await ListUrlPathsAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName)
        {
            string URL = Client.GenerateApiUrl(streamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await Client.ObjectContentAsync(URL, HttpMethod.Post);
            return JsonConvert.DeserializeObject<StreamingLocatorListPathsResponseSchema>(responseContent, ConverterLE.Settings);
        }
    }
}