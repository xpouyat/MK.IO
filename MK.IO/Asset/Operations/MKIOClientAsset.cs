// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

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

        public List<Asset> ListAssets()
        {
            Task<List<Asset>> task = Task.Run<List<Asset>>(async () => await ListAssetsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<Asset>> ListAssetsAsync()
        {
            string URL = GenerateApiUrl(assetsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return Models.ListAssets.FromJson(responseContent).Value;
        }

        public Asset GetAsset(string assetName)
        {
            Task<Asset> task = Task.Run<Asset>(async () => await GetAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<Asset> GetAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return Asset.FromJson(responseContent);
        }

        public Asset CreateOrUpdateAsset(string assetName, Asset content)
        {
            Task<Asset> task = Task.Run<Asset>(async () => await CreateOrUpdateAssetAsync(assetName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<Asset> CreateOrUpdateAssetAsync(string assetName, Asset content)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return Asset.FromJson(responseContent);
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