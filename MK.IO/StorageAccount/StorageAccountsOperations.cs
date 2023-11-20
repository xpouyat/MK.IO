// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
using Newtonsoft.Json;
#if NET48
using System.Net.Http;
#endif

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    internal class StorageAccountsOperations : IStorageAccountsOperations
    {
        //
        // storage
        //
        private const string _storageApiUrl = "api/accounts/{0}/subscriptions/{1}/storage/";
        private const string _storageSelectionApiUrl = _storageApiUrl + "{2}/";
        private const string _storageListCredentialsApiUrl = _storageSelectionApiUrl + "credentials/";
        private const string _storageCredentialApiUrl = _storageListCredentialsApiUrl + "{3}/";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the StorageAccountsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal StorageAccountsOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public StorageResponseSchema Create(StorageRequestSchema storage)
        {
            var task = Task.Run<StorageResponseSchema>(async () => await CreateAsync(storage));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StorageResponseSchema> CreateAsync(StorageRequestSchema storage)
        {
            Argument.AssertNotNull(storage, nameof(storage));

            storage.Spec.Type = "Microsoft.Storage"; // needed
            var url = GenerateStorageApiUrl(_storageApiUrl);
            string responseContent = await Client.CreateObjectPostAsync(url, storage.ToJson());
            return JsonConvert.DeserializeObject<StorageResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with storage deserialization");
        }

        /// <inheritdoc/>
        public List<StorageResponseSchema> List()
        {
            var task = Task.Run<List<StorageResponseSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<StorageResponseSchema>> ListAsync()
        {
            var url = GenerateStorageApiUrl(_storageApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            var objectToReturn = JsonConvert.DeserializeObject<StorageListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Items : throw new Exception($"Error with storage list deserialization");
        }

        /// <inheritdoc/>
        public StorageResponseSchema Get(Guid storageAccountId)
        {
            var task = Task.Run<StorageResponseSchema>(async () => await GetAsync(storageAccountId));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<StorageResponseSchema> GetAsync(Guid storageAccountId)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));

            var url = GenerateStorageApiUrl(_storageSelectionApiUrl, storageAccountId.ToString());
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<StorageResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with storage deserialization");
        }

        /// <inheritdoc/>
        public void Delete(Guid storageAccountId)
        {
            Task.Run(async () => await DeleteAsync(storageAccountId)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid storageAccountId)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));

            await StorageAccountOperationAsync(storageAccountId, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public List<CredentialResponseSchema> ListCredentials(Guid storageAccountId)
        {
            var task = Task.Run<List<CredentialResponseSchema>>(async () => await ListCredentialsAsync(storageAccountId));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<CredentialResponseSchema>> ListCredentialsAsync(Guid storageAccountId)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));

            var url = GenerateStorageApiUrl(_storageListCredentialsApiUrl, storageAccountId.ToString());
            string responseContent = await Client.GetObjectContentAsync(url);
            var objectToReturn = JsonConvert.DeserializeObject<CredentialListReponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Items : throw new Exception($"Error with credential list deserialization");
        }

        /// <inheritdoc/>
        public CredentialResponseSchema GetCredential(Guid storageAccountId, Guid credentialId)
        {
            var task = Task.Run<CredentialResponseSchema>(async () => await GetCredentialAsync(storageAccountId, credentialId));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<CredentialResponseSchema> GetCredentialAsync(Guid storageAccountId, Guid credentialId)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));
            Argument.AssertNotNull(credentialId, nameof(credentialId));

            var url = GenerateStorageApiUrl(_storageCredentialApiUrl, storageAccountId.ToString(), credentialId.ToString());
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<CredentialResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with credential deserialization");
        }

        /// <inheritdoc/>
        public CredentialResponseSchema CreateCredential(Guid storageAccountId, CredentialSchema credential)
        {
            var task = Task.Run(async () => await CreateCredentialAsync(storageAccountId, credential));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<CredentialResponseSchema> CreateCredentialAsync(Guid storageAccountId, CredentialSchema credential)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));
            Argument.AssertNotNull(credential, nameof(credential));

            var url = GenerateStorageApiUrl(_storageListCredentialsApiUrl, storageAccountId.ToString());
            CredentialRequestSchema content = new()
            {
                Spec = credential
            };
            string responseContent = await Client.CreateObjectPostAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<CredentialResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with credential deserialization");
        }

        /// <inheritdoc/>
        public void DeleteCredential(Guid storageAccountId, Guid credentialId)
        {
            Task.Run(async () => await DeleteCredentialAsync(storageAccountId, credentialId)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteCredentialAsync(Guid storageAccountId, Guid credentialId)
        {
            Argument.AssertNotNull(storageAccountId, nameof(storageAccountId));
            Argument.AssertNotNull(credentialId, nameof(credentialId));

            await CredentialOperationAsync(storageAccountId, credentialId, HttpMethod.Delete);
        }

        private async Task StorageAccountOperationAsync(Guid storageAccountId, HttpMethod httpMethod)
        {
            var url = GenerateStorageApiUrl(_storageSelectionApiUrl, storageAccountId.ToString());
            await Client.ObjectContentAsync(url, httpMethod);
        }

        private async Task CredentialOperationAsync(Guid storageAccountId, Guid credentialId, HttpMethod httpMethod)
        {
            var url = GenerateStorageApiUrl(_storageCredentialApiUrl, storageAccountId.ToString(), credentialId.ToString());
            await Client.ObjectContentAsync(url, httpMethod);
        }

        internal string GenerateStorageApiUrl(string urlPath)
        {
            return Client._baseUrl + string.Format(urlPath, Client.GetCustomerId(), Client.GetSubscriptionId());
        }

        internal string GenerateStorageApiUrl(string urlPath, string objectName)
        {
            return Client._baseUrl + string.Format(urlPath, Client.GetCustomerId(), Client.GetSubscriptionId(), objectName);
        }
        internal string GenerateStorageApiUrl(string urlPath, string objectName, string objectName2)
        {
            return Client._baseUrl + string.Format(urlPath, Client.GetCustomerId(), Client.GetSubscriptionId(), objectName, objectName2);
        }
    }
}