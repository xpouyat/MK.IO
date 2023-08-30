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
    public partial class MKIOClientRest
    {
        //
        // streaming locators
        //
        private const string streamingLocatorsApiUrl = "api/ams/{0}/streamingLocators";
        private const string streamingLocatorApiUrl = streamingLocatorsApiUrl + "/{1}";
        private const string streamingLocatorListPathsApiUrl = streamingLocatorApiUrl + "/listPaths";

        public List<MKIOStreamingLocator> ListStreamingLocators()
        {
            Task<List<MKIOStreamingLocator>> task = Task.Run<List<MKIOStreamingLocator>>(async () => await ListStreamingLocatorsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<MKIOStreamingLocator>> ListStreamingLocatorsAsync()
        {
            string URL = GenerateApiUrl(streamingLocatorsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOListStreamingLocators.FromJson(responseContent).Value;
        }

        public MKIOStreamingLocator GetStreamingLocator(string streamingLocatorName)
        {
            Task<MKIOStreamingLocator> task = Task.Run<MKIOStreamingLocator>(async () => await GetStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOStreamingLocator> GetStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await GetObjectContentAsync(URL);
            return MKIOStreamingLocator.FromJson(responseContent);
        }

        public MKIOStreamingLocator CreateStreamingLocator(string streamingLocatorName, MKIOStreamingLocator content)
        {
            Task<MKIOStreamingLocator> task = Task.Run<MKIOStreamingLocator>(async () => await CreateStreamingLocatorAsync(streamingLocatorName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOStreamingLocator> CreateStreamingLocatorAsync(string streamingLocatorName, MKIOStreamingLocator content)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return MKIOStreamingLocator.FromJson(responseContent);
        }

        public void DeleteStreamingLocator(string streamingLocatorName)
        {
            Task.Run(async () => await DeleteStreamingLocatorAsync(streamingLocatorName));
        }

        public async Task DeleteStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public MKIOStreamingLocatorListPaths ListUrlPathsStreamingLocator(string streamingLocatorName)
        {
            Task<MKIOStreamingLocatorListPaths> task = Task.Run<MKIOStreamingLocatorListPaths>(async () => await ListUrlPathsStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<MKIOStreamingLocatorListPaths> ListUrlPathsStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await ObjectContentAsync(URL, HttpMethod.Post);
            return MKIOStreamingLocatorListPaths.FromJson(responseContent);
        }
    }
}