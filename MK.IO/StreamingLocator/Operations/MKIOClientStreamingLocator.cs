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
        // streaming locators
        //
        private const string streamingLocatorsApiUrl = "api/ams/{0}/streamingLocators";
        private const string streamingLocatorApiUrl = streamingLocatorsApiUrl + "/{1}";
        private const string streamingLocatorListPathsApiUrl = streamingLocatorApiUrl + "/listPaths";

        public List<StreamingLocatorSchema> ListStreamingLocators()
        {
            var task = Task.Run<List<StreamingLocatorSchema>>(async () => await ListStreamingLocatorsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<StreamingLocatorSchema>> ListStreamingLocatorsAsync()
        {
            string URL = GenerateApiUrl(streamingLocatorsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingLocatorListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        public StreamingLocatorSchema GetStreamingLocator(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await GetStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorSchema> GetStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
        }

        public StreamingLocatorSchema CreateStreamingLocator(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            var task = Task.Run<StreamingLocatorSchema>(async () => await CreateStreamingLocatorAsync(streamingLocatorName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorSchema> CreateStreamingLocatorAsync(string streamingLocatorName, StreamingLocatorProperties properties)
        {
            string URL = GenerateApiUrl(streamingLocatorApiUrl, streamingLocatorName);
            var content = new StreamingLocatorSchema { Properties = properties };
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<StreamingLocatorSchema>(responseContent, ConverterLE.Settings);
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

        public StreamingLocatorListPathsResponseSchema ListUrlPathsStreamingLocator(string streamingLocatorName)
        {
            var task = Task.Run<StreamingLocatorListPathsResponseSchema>(async () => await ListUrlPathsStreamingLocatorAsync(streamingLocatorName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsStreamingLocatorAsync(string streamingLocatorName)
        {
            string URL = GenerateApiUrl(streamingLocatorListPathsApiUrl, streamingLocatorName);
            string responseContent = await ObjectContentAsync(URL, HttpMethod.Post);
            return JsonConvert.DeserializeObject<StreamingLocatorListPathsResponseSchema>(responseContent, ConverterLE.Settings);
        }
    }
}