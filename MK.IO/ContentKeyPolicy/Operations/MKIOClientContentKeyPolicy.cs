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