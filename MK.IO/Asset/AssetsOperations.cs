using Microsoft.VisualBasic;
using MK.IO.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Principal;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MK.IO.Asset
{
    internal class AssetsOperations : IAssetsOperations
    {
        private const string AssetsApiUrl = MKIOClient.AssetsApiUrl;
        private const string AssetApiUrl = AssetsApiUrl + "/{1}";
        private const string AssetListStreamingLocatorsApiUrl = AssetApiUrl + "/listStreamingLocators";
        private const string AssetListTracksAndDirectoryApiUrl = AssetApiUrl + "/storage/";

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
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }

        /// <inheritdoc/>
        public List<AssetSchema> List(string? orderBy = null, int? top = null)
        {
            Task<List<AssetSchema>> task = Task.Run(async () => await ListAsync(orderBy, top));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<AssetSchema>> ListAsync(string? orderBy = null, int? top = null)
        {
            var url = Client.GenerateApiUrl(AssetsApiUrl);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        /// <inheritdoc/>
        public PagedResult<AssetSchema> ListAsPage(string? orderBy = null, int? top = null)
        {
            Task<PagedResult<AssetSchema>> task = Task.Run(async () => await ListAsPageAsync(orderBy, top));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<AssetSchema>> ListAsPageAsync(string? orderBy = null, int? top = null)
        {
            var url = Client.GenerateApiUrl(AssetsApiUrl);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);
            string responseContent = await Client.GetObjectContentAsync(url);

            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            string? nextPageLink = null;

            nextPageLink = responseObject["@odata.nextLink"];

            return new PagedResult<AssetSchema>
            {
                NextPageLink = HttpUtility.UrlDecode(nextPageLink),
                Results = JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings).Value
            };
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
            var url = Client.BaseUrl.Substring(0, Client.BaseUrl.Length - 1) + nextPageLink;
            string responseContent = await Client.GetObjectContentAsync(url);

            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            nextPageLink = responseObject["@odata.nextLink"];

            return new PagedResult<AssetSchema>
            {
                NextPageLink = HttpUtility.UrlDecode(nextPageLink),
                Results = JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings).Value
            };
        }

        /// <inheritdoc/>
        public AssetSchema Get(string assetName)
        {
            return GetAsync(assetName).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetSchema> GetAsync(string assetName)
        {
            var url = Client.GenerateApiUrl(AssetApiUrl, assetName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public AssetSchema CreateOrUpdate(string assetName, string containerName, string storageName, string description = null)
        {
            Task<AssetSchema> task = Task.Run(async () => await CreateOrUpdateAsync(assetName, containerName, storageName, description));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetSchema> CreateOrUpdateAsync(string assetName, string containerName, string storageName, string description = null)
        {
            var url = Client.GenerateApiUrl(AssetApiUrl, assetName);
            AssetSchema content = new()
            {
                Name = assetName,
                Properties = new AssetProperties
                {
                    Container = containerName,
                    Description = description,
                    StorageAccountName = storageName
                }
            };

            string responseContent = await Client.CreateObjectAsync(url, JsonConvert.SerializeObject(content, Formatting.Indented));
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Delete(string assetName)
        {
            Task.Run(async () => await DeleteAsync(assetName));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string assetName)
        {
            var url = Client.GenerateApiUrl(AssetApiUrl, assetName);
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
            var url = Client.GenerateApiUrl(AssetListStreamingLocatorsApiUrl, assetName);
            string responseContent = await Client.GetObjectContentAsync(url);
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
            var url = Client.GenerateApiUrl(AssetListTracksAndDirectoryApiUrl, assetName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetStorageResponseSchema>(responseContent, ConverterLE.Settings);
        }
    }
}
