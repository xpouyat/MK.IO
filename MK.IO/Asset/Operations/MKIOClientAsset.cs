// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;
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
        // assets
        //
        private const string assetsApiUrl = "api/ams/{0}/assets";
        private const string assetApiUrl = assetsApiUrl + "/{1}";
        private const string assetListStreamingLocatorsApiUrl = assetApiUrl + "/listStreamingLocators";
        private const string assetListTracksAndDirectoryApiUrl = assetApiUrl + "/storage/";

        public List<AssetSchema> ListAssets()
        {
            Task<List<AssetSchema>> task = Task.Run<List<AssetSchema>>(async () => await ListAssetsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<AssetSchema>> ListAssetsAsync()
        {
            string URL = GenerateApiUrl(assetsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<AssetListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public AssetSchema GetAsset(string assetName)
        {
            Task<AssetSchema> task = Task.Run<AssetSchema>(async () => await GetAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<AssetSchema> GetAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings);
        }

        public AssetSchema CreateOrUpdateAsset(string assetName, string containerName, string storageName, string description = null)
        {
            Task<AssetSchema> task = Task.Run<AssetSchema>(async () => await CreateOrUpdateAssetAsync(assetName, containerName, storageName, description));
            return task.GetAwaiter().GetResult();
        }

        public async Task<AssetSchema> CreateOrUpdateAssetAsync(string assetName, string containerName, string storageName, string description = null)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            AssetSchema content = new AssetSchema
            {
                Name = assetName,
                Properties = new AssetProperties
                {
                    Container = containerName,
                    Description = description,
                    StorageAccountName = storageName
                }
            };

            string responseContent = await CreateObjectAsync(URL, JsonConvert.SerializeObject(content, Formatting.Indented));
            return JsonConvert.DeserializeObject<AssetSchema>(responseContent, ConverterLE.Settings);
        }

        public void DeleteAsset(string assetName)
        {
            Task.Run(async () => await DeleteAssetAsync(assetName));
        }

        public async Task DeleteAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public List<AssetStreamingLocator> ListStreamingLocatorsForAsset(string assetName)
        {
            Task<List<AssetStreamingLocator>> task = Task.Run<List<AssetStreamingLocator>>(async () => await ListStreamingLocatorsForAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<AssetStreamingLocator>> ListStreamingLocatorsForAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetListStreamingLocatorsApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return AssetListStreamingLocators.FromJson(responseContent).StreamingLocators;
        }

        // Removed as there is an error (same behavior in the portal too)
        /*
        public Spec ListTracksAndDirListingForAsset(string assetName)
        {
            Task<Spec> task = Task.Run<Spec>(async () => await ListTracksAndDirListingForAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<Spec> ListTracksAndDirListingForAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetListTracksAndDirectoryApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOAssetTracksAndDir.FromJson(responseContent).Spec;
        }
        */
    }
}