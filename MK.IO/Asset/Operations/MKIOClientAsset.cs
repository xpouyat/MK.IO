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

        public List<MKIOAsset> ListAssets()
        {
            Task<List<MKIOAsset>> task = Task.Run<List<MKIOAsset>>(async () => await ListAssetsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<MKIOAsset>> ListAssetsAsync()
        {
            string URL = GenerateApiUrl(assetsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOListAssets.FromJson(responseContent).Value;
        }

        public MKIOAsset GetAsset(string assetName)
        {
            Task<MKIOAsset> task = Task.Run<MKIOAsset>(async () => await GetAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOAsset> GetAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOAsset.FromJson(responseContent);
        }

        public MKIOAsset CreateOrUpdateAsset(string assetName, MKIOAsset content)
        {
            Task<MKIOAsset> task = Task.Run<MKIOAsset>(async () => await CreateOrUpdateAssetAsync(assetName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOAsset> CreateOrUpdateAssetAsync(string assetName, MKIOAsset content)
        {
            string URL = GenerateApiUrl(assetApiUrl, assetName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return MKIOAsset.FromJson(responseContent);
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

        public List<MKIOAssetStreamingLocator> ListStreamingLocatorsForAsset(string assetName)
        {
            Task<List<MKIOAssetStreamingLocator>> task = Task.Run<List<MKIOAssetStreamingLocator>>(async () => await ListStreamingLocatorsForAssetAsync(assetName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<MKIOAssetStreamingLocator>> ListStreamingLocatorsForAssetAsync(string assetName)
        {
            string URL = GenerateApiUrl(assetListStreamingLocatorsApiUrl, assetName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOAssetListStreamingLocators.FromJson(responseContent).StreamingLocators;
        }
    }
}