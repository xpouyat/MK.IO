using MK.IO.Models;
using Newtonsoft.Json;
using System.Net;
#if NET48
using System.Net.Http;
#endif

namespace MK.IO.Asset
{
    internal class AssetsOperations : IAssetsOperations
    {
        private const string _assetsApiUrl = MKIOClient._assetsApiUrl;
        private const string _assetApiUrl = _assetsApiUrl + "/{1}";
        private const string _assetListStreamingLocatorsApiUrl = _assetApiUrl + "/listStreamingLocators";
        private const string _assetListTracksAndDirectoryApiUrl = _assetApiUrl + "/storage/";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the AssetsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal AssetsOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<AssetSchema> List(string? orderBy = null, int? top = null, string? filter = null)
        {
            Task<List<AssetSchema>> task = Task.Run(async () => await ListAsync(orderBy, top, filter));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<AssetSchema>> ListAsync(string? orderBy = null, int? top = null, string? filter = null)
        {
            var url = Client.GenerateApiUrl(_assetsApiUrl);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);
            url = MKIOClient.AddParametersToUrl(url, "$filter", filter);
            string responseContent = await Client.GetObjectContentAsync(url);

            var objectToReturn = JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with asset list deserialization");
        }

        /// <inheritdoc/>
        public PagedResult<AssetSchema> ListAsPage(string? orderBy = null, int? top = null, string? filter = null)
        {
            Task<PagedResult<AssetSchema>> task = Task.Run(async () => await ListAsPageAsync(orderBy, top, filter));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<AssetSchema>> ListAsPageAsync(string? orderBy = null, int? top = null, string? filter = null)
        {
            var url = Client.GenerateApiUrl(_assetsApiUrl);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);
            url = MKIOClient.AddParametersToUrl(url, "$filter", filter);
            string responseContent = await Client.GetObjectContentAsync(url);

            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            string? nextPageLink = responseObject["@odata.nextLink"];

            var objectToReturn = JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings);
            if (objectToReturn == null)
            {
                throw new Exception($"Error with asset list deserialization");
            }
            else
            {
                return new PagedResult<AssetSchema>
                {
                    NextPageLink = WebUtility.UrlDecode(nextPageLink),
                    Results = objectToReturn.Value
                };
            }
        }

        /// <inheritdoc/>
        public PagedResult<AssetSchema> ListAsPageNext(string? nextPageLink)
        {
            Task<PagedResult<AssetSchema>> task = Task.Run(async () => await ListAsPageNextAsync(nextPageLink));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<AssetSchema>> ListAsPageNextAsync(string? nextPageLink)
        {
            var url = Client._baseUrl.Substring(0, Client._baseUrl.Length - 1) + nextPageLink;
            string responseContent = await Client.GetObjectContentAsync(url);

            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            nextPageLink = responseObject["@odata.nextLink"];

            var objectToReturn = JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings);
            if (objectToReturn == null)
            {
                throw new Exception($"Error with asset list deserialization");
            }
            else
            {
                return new PagedResult<AssetSchema>
                {
                    NextPageLink = WebUtility.UrlDecode(nextPageLink),
                    Results = objectToReturn.Value
                };
            }
        }

        /// <inheritdoc/>
        public AssetSchema Get(string assetName)
        {
            return GetAsync(assetName).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetSchema> GetAsync(string assetName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));

            var url = Client.GenerateApiUrl(_assetApiUrl, assetName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with asset deserialization");
        }

        /// <inheritdoc/>
        public AssetSchema CreateOrUpdate(string assetName, string containerName, string storageName, string? description = null)
        {
            Task<AssetSchema> task = Task.Run(async () => await CreateOrUpdateAsync(assetName, containerName, storageName, description));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetSchema> CreateOrUpdateAsync(string assetName, string containerName, string storageName, string? description = null)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));
            Argument.AssertNotNullOrEmpty(containerName, nameof(containerName));
            Argument.AssertNotNullOrEmpty(storageName, nameof(storageName));

            var url = Client.GenerateApiUrl(_assetApiUrl, assetName);
            AssetSchema content = new()
            {
                Properties = new AssetProperties
                {
                    Container = containerName,
                    Description = description!,
                    StorageAccountName = storageName
                }
            };

            string responseContent = await Client.CreateObjectAsync(url, JsonConvert.SerializeObject(content, Formatting.Indented));
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with asset deserialization");
        }

        /// <inheritdoc/>
        public void Delete(string assetName)
        {
            Task.Run(async () => await DeleteAsync(assetName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string assetName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));

            var url = Client.GenerateApiUrl(_assetApiUrl, assetName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public List<AssetStreamingLocator> ListStreamingLocators(string assetName)
        {
            Task<List<AssetStreamingLocator>> task = Task.Run(async () => await ListStreamingLocatorsAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<AssetStreamingLocator>> ListStreamingLocatorsAsync(string assetName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));

            var url = Client.GenerateApiUrl(_assetListStreamingLocatorsApiUrl, assetName);
            string responseContent = await Client.GetObjectPostContentAsync(url);
            return AssetListStreamingLocators.FromJson(responseContent).StreamingLocators;
        }

        /// <inheritdoc/>
        public AssetStorageResponseSchema ListTracksAndDirListing(string assetName)
        {
            var task = Task.Run(async () => await ListTracksAndDirListingAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetStorageResponseSchema> ListTracksAndDirListingAsync(string assetName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));

            var url = Client.GenerateApiUrl(_assetListTracksAndDirectoryApiUrl, assetName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetStorageResponseSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with asset storage deserialization");
        }
    }
}
