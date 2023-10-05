// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;

namespace MK.IO
{
    public interface IStorageAccountsOperations
    {
        /// <summary>
        /// List storage accounts
        /// </summary>
        /// <returns></returns>
        List<StorageResponseSchema> List();

        /// <summary>
        /// List storage accounts
        /// </summary>
        /// <returns></returns>
        Task<List<StorageResponseSchema>> ListAsync();

        /// <summary>
        /// Create a storage account.
        /// The account must be in the same location as your subscription.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        StorageResponseSchema Create(StorageRequestSchema storage);

        /// <summary>
        /// Create a storage account.
        /// The account must be in the same location as your subscription.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        Task<StorageResponseSchema> CreateAsync(StorageRequestSchema storage);

        /// <summary>
        /// Delete our record of your storage account. This operation does not delete the storage account itself.
        /// This operation will not complete successfully if any assets are associated with the storage account.
        /// </summary>
        /// <param name="storageAccountId"></param>
        void Delete(Guid storageAccountId);

        /// <summary>
        /// Delete our record of your storage account. This operation does not delete the storage account itself.
        /// This operation will not complete successfully if any assets are associated with the storage account.
        /// </summary>
        /// <param name="storageAccountId"></param>
        Task DeleteAsync(Guid storageAccountId);

        /// <summary>
        /// Returns details on a single storage account
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        StorageResponseSchema Get(Guid storageAccountId);

        /// <summary>
        /// Returns details on a single storage account
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        Task<StorageResponseSchema> GetAsync(Guid storageAccountId);

        /// <summary>
        /// Returns a list of storage credentials
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        List<CredentialResponseSchema> ListCredentials(Guid storageAccountId);

        /// <summary>
        /// Returns a list of storage credentials
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        Task<List<CredentialResponseSchema>> ListCredentialsAsync(Guid storageAccountId);

        /// <summary>
        /// Returns a single storage credential. Any secret data will be sanitized in the response.
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        CredentialResponseSchema GetCredential(Guid storageAccountId, Guid credentialId);

        /// <summary>
        /// Returns a single storage credential. Any secret data will be sanitized in the response.
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        Task<CredentialResponseSchema> GetCredentialAsync(Guid storageAccountId, Guid credentialId);

        /// <summary>
        /// <para>Creates a storage credential.</para>
        /// <para>You can add as many credentials as you like to a storage account.
        /// We will choose the credential with the longest expiry time when we need to access the storage account.
        /// Credentials are immutable once created - create a new credential record, then delete the old one, to change a credential.</para>
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        CredentialResponseSchema CreateCredential(Guid storageAccountId, CredentialSchema credential);

        /// <summary>
        /// <para>Creates a storage credential.</para>
        /// <para>You can add as many credentials as you like to a storage account.
        /// We will choose the credential with the longest expiry time when we need to access the storage account.
        /// Credentials are immutable once created - create a new credential record, then delete the old one, to change a credential.</para>
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        Task<CredentialResponseSchema> CreateCredentialAsync(Guid storageAccountId, CredentialSchema credential);

        /// <summary>
        /// <para>Removes a storage credential from the storage account.</para>
        /// <para>If the credential is in use and no alternative credentials are available, the storage account will be inaccessible.</para>
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        void DeleteCredential(Guid storageAccountId, Guid credentialId);

        /// <summary>
        /// <para>Removes a storage credential from the storage account.</para>
        /// <para>If the credential is in use and no alternative credentials are available, the storage account will be inaccessible.</para>
        /// </summary>
        /// <param name="storageAccountId"></param>
        /// <returns></returns>
        Task DeleteCredentialAsync(Guid storageAccountId, Guid credentialId);
    }
}