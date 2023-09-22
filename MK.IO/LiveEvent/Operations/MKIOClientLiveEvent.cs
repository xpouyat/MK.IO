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
        // live events
        //
        private const string LiveEventsApiUrl = "api/ams/{0}/liveEvents";
        private const string LiveEventApiUrl = LiveEventsApiUrl + "/{1}";

        public List<LiveEventSchema> ListLiveEvents()
        {
            var task = Task.Run<List<LiveEventSchema>>(async () => await ListLiveEventsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<LiveEventSchema>> ListLiveEventsAsync()
        {
            string URL = GenerateApiUrl(LiveEventsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<LiveEventListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        public LiveEventSchema GetLiveEvent(string liveEventName)
        {
            var task = Task.Run<LiveEventSchema>(async () => await GetLiveEventAsync(liveEventName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<LiveEventSchema> GetLiveEventAsync(string liveEventName)
        {
            string URL = GenerateApiUrl(LiveEventApiUrl, liveEventName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        public LiveEventSchema CreateLiveEvent(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null)
        {
            var task = Task.Run<LiveEventSchema>(async () => await CreateLiveEventAsync(liveEventName, location, properties, tags));
            return task.GetAwaiter().GetResult();
        }

        public async Task<LiveEventSchema> CreateLiveEventAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags)
        {
            string URL = GenerateApiUrl(LiveEventApiUrl, liveEventName);
            if (tags == null)
            {
                tags = new Dictionary<string, string>();
            }
            var content = new LiveEventSchema { Location = location, Tags = tags, Properties = properties };
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        public void DeleteLiveEvent(string liveEventName)
        {
            Task.Run(async () => await DeleteLiveEventAsync(liveEventName));
        }

        public async Task DeleteLiveEventAsync(string liveEventName)
        {
            string URL = GenerateApiUrl(LiveEventApiUrl, liveEventName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }

        public void StartLiveEvent(string liveEventName)
        {
            Task.Run(async () => await StartLiveEventAsync(liveEventName));
        }

        public async Task StartLiveEventAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "start", HttpMethod.Post);
        }

        public void StopLiveEvent(string liveEventName)
        {
            Task.Run(async () => await StopLiveEventAsync(liveEventName));
        }

        public async Task StopLiveEventAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "stop", HttpMethod.Post);
        }

        public void ResetLiveEvent(string liveEventName)
        {
            Task.Run(async () => await ResetLiveEventAsync(liveEventName));
        }

        public async Task ResetLiveEventAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "reset", HttpMethod.Post);
        }

        public void AllocateLiveEvent(string liveEventName)
        {
            Task.Run(async () => await AllocateLiveEventAsync(liveEventName));
        }

        public async Task AllocateLiveEventAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "allocate", HttpMethod.Post);
        }

        private async Task LiveEventOperationAsync(string liveEventName, string? operation, HttpMethod httpMethod)
        {
            string URL = GenerateApiUrl(LiveEventApiUrl + (operation != null ? "/" + operation : string.Empty), liveEventName);
            await ObjectContentAsync(URL, httpMethod);
        }
    }
}