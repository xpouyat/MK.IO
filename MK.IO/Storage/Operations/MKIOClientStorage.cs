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
        // storage
        //
        private const string storageApiUrl = "api/accounts/{0}/subscriptions/{1}/storage/";
        private const string storageSelectionApiUrl = storageApiUrl + "{2}/";


        public ObjectStorage CreateStorageAccount(StorageSpec storage)
        {
            Task<ObjectStorage> task = Task.Run<ObjectStorage>(async () => await CreateStorageAccountAsync(storage));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ObjectStorage> CreateStorageAccountAsync(StorageSpec storage)
        {
            var content = new BaseObjectStorage(storage);
            string URL = GenerateStorageApiUrl(storageApiUrl);
            string responseContent = await CreateObjectPostAsync(URL, content.ToJson());
            return ObjectStorage.FromJson(responseContent);
        }

        public List<BaseObjectStorage> ListStorageAccounts()
        {
            Task<List<BaseObjectStorage>> task = Task.Run<List<BaseObjectStorage>>(async () => await ListStorageAccountAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<BaseObjectStorage>> ListStorageAccountAsync()
        {
            string URL = GenerateStorageApiUrl(storageApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return ObjectStorageList.FromJson(responseContent).Items;
        }

        public ObjectStorage GetStorageAccount(Guid storageAccountId)
        {
            Task<ObjectStorage> task = Task.Run<ObjectStorage>(async () => await GetStorageAccountAsync(storageAccountId));
            return task.GetAwaiter().GetResult();
        }

        public async Task<ObjectStorage> GetStorageAccountAsync(Guid storageAccountId)
        {
            string URL = GenerateStorageApiUrl(storageSelectionApiUrl, storageAccountId.ToString());
            string responseContent = await GetObjectContentAsync(URL);
            return ObjectStorage.FromJson(responseContent);
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
    }
}