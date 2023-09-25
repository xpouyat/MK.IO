// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IStorageAccountsOperations
    {
        StorageResponseSchema Create(StorageRequestSchema storage);
        Task<StorageResponseSchema> CreateAsync(StorageRequestSchema storage);
        void Delete(Guid storageAccountId);
        Task DeleteAsync(Guid storageAccountId);
        StorageResponseSchema Get(Guid storageAccountId);
        Task<StorageResponseSchema> GetAsync(Guid storageAccountId);
        CredentialResponseSchema GetCredential(Guid storageAccountId, Guid credentialId);
        Task<CredentialResponseSchema> GetCredentialAsync(Guid storageAccountId, Guid credentialId);
        List<StorageResponseSchema> List();
        Task<List<StorageResponseSchema>> ListAsync();
        List<CredentialResponseSchema> ListCredentials(Guid storageAccountId);
        Task<List<CredentialResponseSchema>> ListCredentialsAsync(Guid storageAccountId);
    }
}