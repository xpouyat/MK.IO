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
        // streaming locators
        //
        private const string streamingLocatorsApiUrl = "api/ams/{0}/streamingLocators";
        private const string streamingLocatorApiUrl = streamingLocatorsApiUrl + "/{1}";
        private const string streamingLocatorListPathsApiUrl = streamingLocatorApiUrl + "/listPaths";

        public List<StreamingLocator> ListStreamingLocators()
        {
            Task<List<StreamingLocator>> task = Task.Run<List<StreamingLocator>>(async () => await ListStreamingLocatorsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StreamingLocator>> ListStreamingLocatorsAsync()
        {
            string URL = GenerateApiUrl(streamingLocatorsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return Models.ListStreamingLocators.FromJson(responseContent).Value;
        }

        public StreamingLocator GetStreamingLocator(string streamingLocatorName)
        {
            Task<StreamingLocator> task = Task.Run<StreamingLocator>(async () => await GetStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocator> GetStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await GetObjectContentAsync(URL);
            return StreamingLocator.FromJson(responseContent);
        }

        public StreamingLocator CreateStreamingLocator(string streamingLocatorName, StreamingLocator content)
        {
            Task<StreamingLocator> task = Task.Run<StreamingLocator>(async () => await CreateStreamingLocatorAsync(streamingLocatorName, content));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocator> CreateStreamingLocatorAsync(string streamingLocatorName, StreamingLocator content)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return StreamingLocator.FromJson(responseContent);
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

        public StreamingLocatorListPaths ListUrlPathsStreamingLocator(string streamingLocatorName)
        {
            Task<StreamingLocatorListPaths> task = Task.Run<StreamingLocatorListPaths>(async () => await ListUrlPathsStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorListPaths> ListUrlPathsStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await ObjectContentAsync(URL, HttpMethod.Post);
            return StreamingLocatorListPaths.FromJson(responseContent);
        }
    }
}