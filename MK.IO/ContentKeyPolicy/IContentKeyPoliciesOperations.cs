// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IContentKeyPoliciesOperations
    {
        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <returns></returns>
        List<ContentKeyPolicySchema> List();

        /// <summary>
        /// List Content Key Policies
        /// </summary>
        /// <returns></returns>
        Task<List<ContentKeyPolicySchema>> ListAsync();

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
        ContentKeyPolicySchema Get(string contentKeyPolicyName);

        /// <summary>
        /// Get one Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <returns></returns>
        Task<ContentKeyPolicySchema> GetAsync(string contentKeyPolicyName);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        ContentKeyPolicySchema Create(string contentKeyPolicyName, ContentKeyPolicyProperties properties);

        /// <summary>
        /// Create a Content Key Policy
        /// </summary>
        /// <param name="contentKeyPolicyName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<ContentKeyPolicySchema> CreateAsync(string contentKeyPolicyName, ContentKeyPolicyProperties properties);

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