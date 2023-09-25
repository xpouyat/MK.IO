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
    internal class LiveEventsOperations : ILiveEventsOperations
    {
        //
        // live events
        //
        private const string LiveEventsApiUrl = MKIOClient.LiveEventsApiUrl;
        private const string LiveEventApiUrl = LiveEventsApiUrl + "/{1}";


        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the LiveEventsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal LiveEventsOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }

        public List<LiveEventSchema> List()
        {
            var task = Task.Run<List<LiveEventSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<LiveEventSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(LiveEventsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<LiveEventListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        public LiveEventSchema Get(string liveEventName)
        {
            var task = Task.Run<LiveEventSchema>(async () => await GetAsync(liveEventName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<LiveEventSchema> GetAsync(string liveEventName)
        {
            var url = Client.GenerateApiUrl(LiveEventApiUrl, liveEventName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        public LiveEventSchema Create(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null)
        {
            var task = Task.Run<LiveEventSchema>(async () => await CreateAsync(liveEventName, location, properties, tags));
            return task.GetAwaiter().GetResult();
        }

        public async Task<LiveEventSchema> CreateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags)
        {
            var url = Client.GenerateApiUrl(LiveEventApiUrl, liveEventName);
            if (tags == null)
            {
                tags = new Dictionary<string, string>();
            }
            var content = new LiveEventSchema { Location = location, Tags = tags, Properties = properties };
            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        public void Delete(string liveEventName)
        {
            Task.Run(async () => await DeleteAsync(liveEventName));
        }

        public async Task DeleteAsync(string liveEventName)
        {
            var url = Client.GenerateApiUrl(LiveEventApiUrl, liveEventName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        public void Start(string liveEventName)
        {
            Task.Run(async () => await StartAsync(liveEventName));
        }

        public async Task StartAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "start", HttpMethod.Post);
        }

        public void Stop(string liveEventName)
        {
            Task.Run(async () => await StopAsync(liveEventName));
        }

        public async Task StopAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "stop", HttpMethod.Post);
        }

        public void Reset(string liveEventName)
        {
            Task.Run(async () => await ResetAsync(liveEventName));
        }

        public async Task ResetAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "reset", HttpMethod.Post);
        }

        public void Allocate(string liveEventName)
        {
            Task.Run(async () => await AllocateAsync(liveEventName));
        }

        public async Task AllocateAsync(string liveEventName)
        {
            await LiveEventOperationAsync(liveEventName, "allocate", HttpMethod.Post);
        }

        private async Task LiveEventOperationAsync(string liveEventName, string? operation, HttpMethod httpMethod)
        {
            var url = Client.GenerateApiUrl(LiveEventApiUrl + (operation != null ? "/" + operation : string.Empty), liveEventName);
            await Client.ObjectContentAsync(url, httpMethod);
        }
    }
}