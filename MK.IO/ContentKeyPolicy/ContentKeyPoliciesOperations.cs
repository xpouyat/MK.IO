// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    internal class ContentKeyPoliciesOperations : IContentKeyPoliciesOperations
    {
        //
        // content key policy
        //
        private const string ContentKeyPoliciesApiUrl = MKIOClient.ContentKeyPoliciesApiUrl;
        private const string ContentKeyPolicyApiUrl = ContentKeyPoliciesApiUrl + "/{1}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the ContentKeyPoliciesOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal ContentKeyPoliciesOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }

        /// <inheritdoc/>
        public List<ContentKeyPolicy> List()
        {
            Task<List<ContentKeyPolicy>> task = Task.Run<List<ContentKeyPolicy>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<ContentKeyPolicy>> ListAsync()
        {
            var url = Client.GenerateApiUrl(ContentKeyPoliciesApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return Models.ListContentKeyPolicies.FromJson(responseContent).Value;
        }

        /// <inheritdoc/>
        public ContentKeyPolicy Get(string contentKeyPolicyName)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await GetAsync(contentKeyPolicyName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<ContentKeyPolicy> GetAsync(string contentKeyPolicyName)
        {
            var url = Client.GenerateApiUrl(ContentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return ContentKeyPolicy.FromJson(responseContent);
        }

        /// <inheritdoc/>
        public void Delete(string contentKeyPolicyName)
        {
            Task.Run(async () => await DeleteAsync(contentKeyPolicyName));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string contentKeyPolicyName)
        {
            var url = Client.GenerateApiUrl(ContentKeyPolicyApiUrl, contentKeyPolicyName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public ContentKeyPolicy Create(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await CreateAsync(contentKeyPolicyName, content));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<ContentKeyPolicy> CreateAsync(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            var url = Client.GenerateApiUrl(ContentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            var t = content.ToJson();
            return ContentKeyPolicy.FromJson(responseContent);
        }
    }
}