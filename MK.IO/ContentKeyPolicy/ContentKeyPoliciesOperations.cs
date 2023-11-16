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
        private const string _contentKeyPoliciesApiUrl = MKIOClient._contentKeyPoliciesApiUrl;
        private const string _contentKeyPolicyApiUrl = _contentKeyPoliciesApiUrl + "/{1}";

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
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<ContentKeyPolicy> List()
        {
            Task<List<ContentKeyPolicy>> task = Task.Run(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<ContentKeyPolicy>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_contentKeyPoliciesApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return Models.ListContentKeyPolicies.FromJson(responseContent).Value;
        }

        /// <inheritdoc/>
        public ContentKeyPolicy Get(string contentKeyPolicyName)
        {
            Task<ContentKeyPolicy> task = Task.Run(async () => await GetAsync(contentKeyPolicyName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<ContentKeyPolicy> GetAsync(string contentKeyPolicyName)
        {
            Argument.AssertNotNullOrEmpty(contentKeyPolicyName, nameof(contentKeyPolicyName));

            var url = Client.GenerateApiUrl(_contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return ContentKeyPolicy.FromJson(responseContent);
        }

        /// <inheritdoc/>
        public void Delete(string contentKeyPolicyName)
        {
            Task.Run(async () => await DeleteAsync(contentKeyPolicyName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string contentKeyPolicyName)
        {
            Argument.AssertNotNullOrEmpty(contentKeyPolicyName, nameof(contentKeyPolicyName));

            var url = Client.GenerateApiUrl(_contentKeyPolicyApiUrl, contentKeyPolicyName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public ContentKeyPolicy Create(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            Task<ContentKeyPolicy> task = Task.Run(async () => await CreateAsync(contentKeyPolicyName, content));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<ContentKeyPolicy> CreateAsync(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            Argument.AssertNotNullOrEmpty(contentKeyPolicyName, nameof(contentKeyPolicyName));
            Argument.AssertNotNull(content, nameof(content));

            var url = Client.GenerateApiUrl(_contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return ContentKeyPolicy.FromJson(responseContent);
        }

        /// <inheritdoc/>
        public ContentKeyPolicyProperties GetPolicyPropertiesWithSecrets(string contentKeyPolicyName)
        {
            Task<ContentKeyPolicyProperties> task = Task.Run(async () => await GetPolicyPropertiesWithSecretsAsync(contentKeyPolicyName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<ContentKeyPolicyProperties> GetPolicyPropertiesWithSecretsAsync(string contentKeyPolicyName)
        {
            Argument.AssertNotNullOrEmpty(contentKeyPolicyName, nameof(contentKeyPolicyName));

            var url = Client.GenerateApiUrl(_contentKeyPolicyApiUrl + "/getPolicyPropertiesWithSecrets", contentKeyPolicyName);
            string responseContent = await Client.GetObjectPostContentAsync(url);
            return ContentKeyPolicy.FromJson(responseContent).Properties;
        }
    }
}