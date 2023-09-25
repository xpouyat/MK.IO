// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public class ContentKeyPoliciesOperations : IContentKeyPoliciesOperations
    {
        //
        // content key policy
        //
        private const string contentKeyPoliciesApiUrl = "api/ams/{0}/contentKeyPolicies";
        private const string contentKeyPolicyApiUrl = contentKeyPoliciesApiUrl + "/{1}";

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

        public List<ContentKeyPolicy> ListContentKeyPolicies()
        {
            Task<List<ContentKeyPolicy>> task = Task.Run<List<ContentKeyPolicy>>(async () => await ListContentKeyPoliciesAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<ContentKeyPolicy>> ListContentKeyPoliciesAsync()
        {
            string URL = Client.GenerateApiUrl(contentKeyPoliciesApiUrl);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return Models.ListContentKeyPolicies.FromJson(responseContent).Value;
        }

        public ContentKeyPolicy Get(string contentKeyPolicyName)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await GetAsync(contentKeyPolicyName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ContentKeyPolicy> GetAsync(string contentKeyPolicyName)
        {
            string URL = Client.GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return ContentKeyPolicy.FromJson(responseContent);
        }

        public void Delete(string contentKeyPolicyName)
        {
            Task.Run(async () => await DeleteAsync(contentKeyPolicyName));
        }

        public async Task DeleteAsync(string contentKeyPolicyName)
        {
            string URL = Client.GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            await Client.ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public ContentKeyPolicy Create(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await CreateAsync(contentKeyPolicyName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ContentKeyPolicy> CreateAsync(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            string URL = Client.GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await Client.CreateObjectAsync(URL, content.ToJson());
            var t = content.ToJson();
            return ContentKeyPolicy.FromJson(responseContent);
        }
    }
}