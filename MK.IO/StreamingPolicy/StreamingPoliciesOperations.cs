// Copyright (c) Microsoft Corporation. All rights reserved.
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
    internal class StreamingPoliciesOperations : IStreamingPoliciesOperations
    {
        //
        // streaming policies
        //
        private const string _streamingPoliciesApiUrl = MKIOClient._streamingPoliciesApiUrl;
        private const string _streamingPolicyApiUrl = _streamingPoliciesApiUrl + "/{1}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the StreamingPoliciesOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal StreamingPoliciesOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<StreamingPolicySchema> List()
        {
            var task = Task.Run(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<StreamingPolicySchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_streamingPoliciesApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            var objectToReturn = JsonConvert.DeserializeObject<StreamingPolicyListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with streaming policy list deserialization");
        }

        /// <inheritdoc/>
        public StreamingPolicySchema Get(string streamingPolicyName)
        {
            var task = Task.Run(async () => await GetAsync(streamingPolicyName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingPolicySchema> GetAsync(string streamingPolicyName)
        {
            Argument.AssertNotNullOrEmpty(streamingPolicyName, nameof(streamingPolicyName));

            var url = Client.GenerateApiUrl(_streamingPolicyApiUrl, streamingPolicyName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StreamingPolicySchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming policy deserialization");
        }

        /// <inheritdoc/>
        public StreamingPolicySchema Create(string streamingPolicyName, StreamingPolicyProperties properties)
        {
            var task = Task.Run(async () => await CreateAsync(streamingPolicyName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StreamingPolicySchema> CreateAsync(string streamingPolicyName, StreamingPolicyProperties properties)
        {
            Argument.AssertNotNullOrEmpty(streamingPolicyName, nameof(streamingPolicyName));
            Argument.AssertNotMoreThanLength(streamingPolicyName, nameof(streamingPolicyName), 260);
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_streamingPolicyApiUrl, streamingPolicyName);
            var content = new StreamingPolicySchema { Properties = properties };
            string responseContent = await Client.CreateObjectPutAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<StreamingPolicySchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with streaming policy deserialization");
        }

        /// <inheritdoc/>
        public void Delete(string streamingPolicyName)
        {
            Task.Run(async () => await DeleteAsync(streamingPolicyName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string streamingPolicyName)
        {
            Argument.AssertNotNullOrEmpty(streamingPolicyName, nameof(streamingPolicyName));

            var url = Client.GenerateApiUrl(_streamingPolicyApiUrl, streamingPolicyName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}