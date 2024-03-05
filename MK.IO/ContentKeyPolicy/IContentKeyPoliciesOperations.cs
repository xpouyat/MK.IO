// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO
{
    public interface IContentKeyPoliciesOperations
    {
        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <returns></returns>
        List<ContentKeyPolicy> List();

        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <returns></returns>
        Task<List<ContentKeyPolicy>> ListAsync();

        /// <summary>
        /// Delete a Content Key Policy. If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        void Delete(string contentKeyPolicyName);

        /// <summary>
        /// Delete a Content Key Policy. If the policy does not exist, this will return a 204.
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        Task DeleteAsync(string contentKeyPolicyName);

        /// <summary>
        /// Get one Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        ContentKeyPolicy Get(string contentKeyPolicyName);

        /// <summary>
        /// Get one Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        Task<ContentKeyPolicy> GetAsync(string contentKeyPolicyName);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        ContentKeyPolicy Create(string contentKeyPolicyName, ContentKeyPolicy content);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<ContentKeyPolicy> CreateAsync(string contentKeyPolicyName, ContentKeyPolicy content);

        /// <summary>
        /// Get the properties of a Content Key Policy including secret values
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        ContentKeyPolicyProperties GetPolicyPropertiesWithSecrets(string contentKeyPolicyName);

        /// <summary>
        /// Get the properties of a Content Key Policy including secret values
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        Task<ContentKeyPolicyProperties> GetPolicyPropertiesWithSecretsAsync(string contentKeyPolicyName);
    }
}