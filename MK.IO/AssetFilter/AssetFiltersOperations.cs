// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;
using Newtonsoft.Json;
using System.Net;
#if NET462
using System.Net.Http;
#endif

namespace MK.IO.Operations
{
    internal class AssetFiltersOperations : IAssetFiltersOperations
    {
        private const string _assetsApiUrl = MKIOClient._assetsApiUrl;
        private const string _assetApiUrl = _assetsApiUrl + "/{1}";
        private const string _assetFiltersApiUrl = _assetApiUrl + "/assetFilters";
        private const string _assetFilterApiUrl = _assetApiUrl + "/filters/{2}";


        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the AssetFiltersOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal AssetFiltersOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<AssetFilterSchema> List(string assetName, string? orderBy = null, string? filter = null, int? top = null)
        {
            Task<List<AssetFilterSchema>> task = Task.Run(async () => await ListAsync(assetName, orderBy, filter, top));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<AssetFilterSchema>> ListAsync(string assetName, string? orderBy = null, string? filter = null, int? top = null)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));

            var url = Client.GenerateApiUrl(_assetFiltersApiUrl, assetName);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$filter", filter);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);

            string responseContent = await Client.GetObjectContentAsync(url);
            var objectToReturn = JsonConvert.DeserializeObject<AssetFilterListResponseSchema>(responseContent, ConverterLE.Settings);
            return objectToReturn != null ? objectToReturn.Value : throw new Exception($"Error with asset filter list deserialization");
        }

        /// <inheritdoc/>
        public PagedResult<AssetFilterSchema> ListAsPage(string assetName, string? orderBy = null, string? filter = null, int? top = null)
        {
            Task<PagedResult<AssetFilterSchema>> task = Task.Run(async () => await ListAsPageAsync(assetName, orderBy, filter, top));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<AssetFilterSchema>> ListAsPageAsync(string assetName, string? orderBy = null, string? filter = null, int? top = null)
        {
            var url = Client.GenerateApiUrl(_assetFiltersApiUrl, assetName);
            url = MKIOClient.AddParametersToUrl(url, "$orderby", orderBy);
            url = MKIOClient.AddParametersToUrl(url, "$filter", filter);
            url = MKIOClient.AddParametersToUrl(url, "$top", top != null ? ((int)top).ToString() : null);

            string responseContent = await Client.GetObjectContentAsync(url);

            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            string? nextPageLink = responseObject["@odata.nextLink"];

            var objectToReturn = JsonConvert.DeserializeObject<AssetFilterListResponseSchema>(responseContent, ConverterLE.Settings);
            if (objectToReturn == null)
            {
                throw new Exception($"Error with asset filter deserialization");
            }
            else
            {
                return new PagedResult<AssetFilterSchema>
                {
                    NextPageLink = WebUtility.UrlDecode(nextPageLink),
                    Results = objectToReturn.Value
                };
            }
        }

        /// <inheritdoc/>
        public PagedResult<AssetFilterSchema> ListAsPageNext(string? nextPageLink)
        {
            Task<PagedResult<AssetFilterSchema>> task = Task.Run(async () => await ListAsPageNextAsync(nextPageLink));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<AssetFilterSchema>> ListAsPageNextAsync(string? nextPageLink)
        {
            return await Client.ListAsPageNextGenericAsync<AssetFilterSchema>(nextPageLink, typeof(AssetFilterListResponseSchema), "asset filter");
        }

        /// <inheritdoc/>
        public AssetFilterSchema Get(string assetName, string filterName)
        {
            return GetAsync(assetName, filterName).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetFilterSchema> GetAsync(string assetName, string filterName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));
            Argument.AssertNotNullOrEmpty(filterName, nameof(filterName));

            var url = Client.GenerateApiUrl(_assetFilterApiUrl, assetName, filterName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<AssetFilterSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with asset filter deserialization");
        }

        /// <inheritdoc/>
        public AssetFilterSchema CreateOrUpdate(string assetName, string filterName, MediaFilterProperties properties)
        {
            Task<AssetFilterSchema> task = Task.Run(async () => await CreateOrUpdateAsync(assetName, filterName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AssetFilterSchema> CreateOrUpdateAsync(string assetName, string filterName, MediaFilterProperties properties)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));
            Argument.AssertNotNullOrEmpty(filterName, nameof(filterName));
            Argument.AssertNotContainsSpace(filterName, nameof(filterName));
            Argument.AssertNotMoreThanLength(filterName, nameof(filterName), 260);
            Argument.AssertRespectRegex(filterName, nameof(filterName), @"^[a-zA-Z0-9\-_.~]+$");
            Argument.AssertNotNull(properties, nameof(properties));

            var url = Client.GenerateApiUrl(_assetFilterApiUrl, assetName, filterName);
            AssetFilterSchema content = new()
            {
                //Name = assetFilterName,
                Properties = properties
            };

            string responseContent = await Client.CreateObjectPutAsync(url, JsonConvert.SerializeObject(content, ConverterLE.Settings));
            return JsonConvert.DeserializeObject<AssetFilterSchema>(responseContent, ConverterLE.Settings) ?? throw new Exception("Error with asset filter deserialization");
        }

        /// <inheritdoc/>
        public void Delete(string assetName, string filterName)
        {
            Task.Run(async () => await DeleteAsync(assetName, filterName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string assetName, string filterName)
        {
            Argument.AssertNotNullOrEmpty(assetName, nameof(assetName));
            Argument.AssertNotNullOrEmpty(filterName, nameof(filterName));

            var url = Client.GenerateApiUrl(_assetFilterApiUrl, assetName, filterName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}
