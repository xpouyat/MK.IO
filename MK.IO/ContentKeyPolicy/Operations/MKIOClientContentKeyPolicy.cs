// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

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

        public List<ContentKeyPolicy> ListContentKeyPoliciesLocators()
        {
            Task<List<ContentKeyPolicy>> task = Task.Run<List<ContentKeyPolicy>>(async () => await ListContentKeyPoliciesLocatorsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<ContentKeyPolicy>> ListContentKeyPoliciesLocatorsAsync()
        {
            string URL = GenerateApiUrl(contentKeyPoliciesApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return ListContentKeyPolicies.FromJson(responseContent).Value;
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

      
    }
}