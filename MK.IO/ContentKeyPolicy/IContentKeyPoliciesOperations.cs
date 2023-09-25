// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public interface IContentKeyPoliciesOperations
    {
        ContentKeyPolicy Create(string contentKeyPolicyName, ContentKeyPolicy content);
        Task<ContentKeyPolicy> CreateAsync(string contentKeyPolicyName, ContentKeyPolicy content);
        void Delete(string contentKeyPolicyName);
        Task DeleteAsync(string contentKeyPolicyName);
        ContentKeyPolicy Get(string contentKeyPolicyName);
        Task<ContentKeyPolicy> GetAsync(string contentKeyPolicyName);
        List<ContentKeyPolicy> ListContentKeyPolicies();
        Task<List<ContentKeyPolicy>> ListContentKeyPoliciesAsync();
    }
}