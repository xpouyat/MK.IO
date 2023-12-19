using MK.IO.Models;
using Newtonsoft.Json;
#if NET48
using System.Net.Http;
#endif

namespace MK.IO.Asset
{
    internal class AccountFiltersOperations : IAccountFiltersOperations
    {
        private const string _accountFiltersApiUrl = MKIOClient._accountFiltersApiUrl;
        private const string _accountFilterApiUrl = _accountFiltersApiUrl + "/{1}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the AccountFiltersOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal AccountFiltersOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<AccountFilterSchema> List()
        {
            Task<List<AccountFilterSchema>> task = Task.Run(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<AccountFilterSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_accountFiltersApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);

            var objectToReturn = JsonConvert.DeserializeObject<AccountFilterListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with account filter deserialization");
        }

        /// <inheritdoc/>
        public AccountFilterSchema Get(string accountFilterName)
        {
            return GetAsync(accountFilterName).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AccountFilterSchema> GetAsync(string accountFilterName)
        {
            Argument.AssertNotNullOrEmpty(accountFilterName, nameof(accountFilterName));

            var url = Client.GenerateApiUrl(_accountFilterApiUrl, accountFilterName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AccountFilterSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with account filter deserialization");
        }

        /// <inheritdoc/>
        public AccountFilterSchema CreateOrUpdate(string accountFilterName, MediaFilterProperties properties)
        {
            Task<AccountFilterSchema> task = Task.Run(async () => await CreateOrUpdateAsync(accountFilterName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AccountFilterSchema> CreateOrUpdateAsync(string accountFilterName, MediaFilterProperties properties)
        {
            Argument.AssertNotNullOrEmpty(accountFilterName, nameof(accountFilterName));
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_accountFilterApiUrl, accountFilterName);
            AccountFilterSchema content = new()
            {
                //Name = accountFilterName,
                Properties = properties
            };

            string responseContent = await Client.CreateObjectPutAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<AccountFilterSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with account filter deserialization");
        }

        /// <inheritdoc/>
        public void Delete(string accountFilterName)
        {
            Task.Run(async () => await DeleteAsync(accountFilterName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string accountFilterName)
        {
            Argument.AssertNotNullOrEmpty(accountFilterName, nameof(accountFilterName));

            var url = Client.GenerateApiUrl(_accountFilterApiUrl, accountFilterName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}
