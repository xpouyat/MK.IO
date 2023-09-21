// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using IO.Swagger.Model;
using Newtonsoft.Json;

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
        // storage
        //
        private const string storageApiUrl = "api/accounts/{0}/subscriptions/{1}/storage/";
        private const string storageSelectionApiUrl = storageApiUrl + "{2}/";
        private const string storageListCredentialsApiUrl = storageSelectionApiUrl + "credentials/";
        private const string storageCredentialApiUrl = storageListCredentialsApiUrl + "{3}/";


        public StorageResponseSchema CreateStorageAccount(StorageRequestSchema storage)
        {
           var task = Task.Run<StorageResponseSchema>(async () => await CreateStorageAccountAsync(storage));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StorageResponseSchema> CreateStorageAccountAsync(StorageRequestSchema storage)
        {
            storage.Spec.Type = "Microsoft.Storage"; // needed
            string URL = GenerateStorageApiUrl(storageApiUrl);
            string responseContent = await CreateObjectPostAsync(URL, storage.ToJson());
            return JsonConvert.DeserializeObject<StorageResponseSchema>(responseContent, ConverterLE.Settings);
        }

        public List<StorageResponseSchema> ListStorageAccounts()
        {
            var task = Task.Run<List<StorageResponseSchema>>(async () => await ListStorageAccountAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StorageResponseSchema>> ListStorageAccountAsync()
        {
            string URL = GenerateStorageApiUrl(storageApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StorageListResponseSchema>(responseContent, ConverterLE.Settings).Items;
        }

        public StorageResponseSchema GetStorageAccount(Guid storageAccountId)
        {
           var task = Task.Run<StorageResponseSchema>(async () => await GetStorageAccountAsync(storageAccountId));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StorageResponseSchema> GetStorageAccountAsync(Guid storageAccountId)
        {
            string URL = GenerateStorageApiUrl(storageSelectionApiUrl, storageAccountId.ToString());
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StorageResponseSchema>(responseContent, ConverterLE.Settings);
        }

        public void DeleteStorageAccount(Guid storageAccountId)
        {
            Task.Run(async () => await DeleteStorageAccountAsync(storageAccountId));
        }

        public async Task DeleteStorageAccountAsync(Guid storageAccountId)
        {
            await StorageAccountOperationAsync(storageAccountId, HttpMethod.Delete);
        }

        private async Task StorageAccountOperationAsync(Guid storageAccountId,  HttpMethod httpMethod)
        {
            string URL = GenerateStorageApiUrl(storageSelectionApiUrl, storageAccountId.ToString());
            await ObjectContentAsync(URL, httpMethod);
        }

        public List<CredentialResponseSchema> ListStorageAccountCredentials(Guid storageAccountId)
        {
            var task = Task.Run<List<CredentialResponseSchema>>(async () => await ListStorageAccountCredentialsAsync(storageAccountId));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<CredentialResponseSchema>> ListStorageAccountCredentialsAsync(Guid storageAccountId)
        {
            string URL = GenerateStorageApiUrl(storageListCredentialsApiUrl, storageAccountId.ToString());
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<CredentialListReponseSchema>(responseContent, ConverterLE.Settings).Items;
        }

        public CredentialResponseSchema GetStorageAccountCredential(Guid storageAccountId, Guid credentialId)
        {
           var task = Task.Run<CredentialResponseSchema>(async () => await GetStorageAccountCredentialAsync(storageAccountId, credentialId));
            return task.GetAwaiter().GetResult();
        }

        public async Task<CredentialResponseSchema> GetStorageAccountCredentialAsync(Guid storageAccountId, Guid credentialId)
        {
            string URL = GenerateStorageApiUrl(storageCredentialApiUrl, storageAccountId.ToString(), credentialId.ToString());
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<CredentialResponseSchema>(responseContent, ConverterLE.Settings);
        }

    }
}