// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClient
    {
        //
        // content key policy
        //
        private const string contentKeyPoliciesApiUrl = "api/ams/{0}/contentKeyPolicies";
        private const string contentKeyPolicyApiUrl = contentKeyPoliciesApiUrl + "/{1}";

        public List<ContentKeyPolicy> ListContentKeyPolicies()
        {
            Task<List<ContentKeyPolicy>> task = Task.Run<List<ContentKeyPolicy>>(async () => await ListContentKeyPoliciesAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<ContentKeyPolicy>> ListContentKeyPoliciesAsync()
        {
            string URL = GenerateApiUrl(contentKeyPoliciesApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return Models.ListContentKeyPolicies.FromJson(responseContent).Value;
        }

        public ContentKeyPolicy GetContentKeyPolicy(string contentKeyPolicyName)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await GetContentKeyPolicyAsync(contentKeyPolicyName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ContentKeyPolicy> GetContentKeyPolicyAsync(string contentKeyPolicyName)
        {
            string URL = GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await GetObjectContentAsync(URL);
            return ContentKeyPolicy.FromJson(responseContent);
        }

        public void DeleteContentKeyPolicy(string contentKeyPolicyName)
        {
            Task.Run(async () => await DeleteContentKeyPolicyAsync(contentKeyPolicyName));
        }

        public async Task DeleteContentKeyPolicyAsync(string contentKeyPolicyName)
        {
            string URL = GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public ContentKeyPolicy CreateContentKeyPolicy(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            Task<ContentKeyPolicy> task = Task.Run<ContentKeyPolicy>(async () => await CreateContentKeyPolicyAsync(contentKeyPolicyName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ContentKeyPolicy> CreateContentKeyPolicyAsync(string contentKeyPolicyName, ContentKeyPolicy content)
        {
            string URL = GenerateApiUrl(contentKeyPolicyApiUrl, contentKeyPolicyName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            var t = content.ToJson();
            return ContentKeyPolicy.FromJson(responseContent);
        }
    }
}