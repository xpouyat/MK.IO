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
    internal class TransformsOperations : ITransformsOperations
    {
        //
        // transforms
        //

        private const string TransformsApiUrl = MKIOClient.TransformsApiUrl;
        private const string TransformApiUrl = TransformsApiUrl + "/{1}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the TransformsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal TransformsOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }


        /// <inheritdoc/>
        public List<TransformSchema> List()
        {
            var task = Task.Run<List<TransformSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<TransformSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(TransformsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<TransformListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        /// <inheritdoc/>
        public TransformSchema Get(string transformName)
        {
            var task = Task.Run<TransformSchema>(async () => await GetAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<TransformSchema> GetAsync(string transformName)
        {
            var url = Client.GenerateApiUrl(TransformApiUrl, transformName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<TransformSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public TransformSchema CreateOrUpdate(string transformName, TransformProperties properties)
        {
            var task = Task.Run<TransformSchema>(async () => await CreateOrUpdateAsync(transformName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<TransformSchema> CreateOrUpdateAsync(string transformName, TransformProperties properties)
        {
            var url = Client.GenerateApiUrl(TransformApiUrl, transformName);
            var content = new TransformSchema { Properties = properties };
            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<TransformSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Delete(string transformName)
        {
            Task.Run(async () => await DeleteAsync(transformName));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string transformName)
        {
            var url = Client.GenerateApiUrl(TransformApiUrl, transformName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}