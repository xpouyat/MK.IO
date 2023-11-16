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
        private const string _liveEventsApiUrl = MKIOClient._liveEventsApiUrl;
        private const string _liveEventApiUrl = _liveEventsApiUrl + "/{1}";

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
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<LiveEventSchema> List()
        {
            var task = Task.Run<List<LiveEventSchema>>(async () => await ListAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<LiveEventSchema>> ListAsync()
        {
            var url = Client.GenerateApiUrl(_liveEventsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<LiveEventListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        /// <inheritdoc/>
        public LiveEventSchema Get(string liveEventName)
        {
            var task = Task.Run<LiveEventSchema>(async () => await GetAsync(liveEventName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<LiveEventSchema> GetAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            var url = Client.GenerateApiUrl(_liveEventApiUrl, liveEventName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public LiveEventSchema Update(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null)
        {
            var task = Task.Run<LiveEventSchema>(async () => await UpdateAsync(liveEventName, location, properties, tags));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<LiveEventSchema> UpdateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));
            Argument.AssertNotNullOrEmpty(location, nameof(location));
            Argument.AssertNotNull(properties, nameof(properties));

            return await CreateOrUpdateAsync(liveEventName, location, properties, tags, Client.UpdateObjectAsync);
        }

        /// <inheritdoc/>
        public LiveEventSchema Create(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null)
        {
            var task = Task.Run<LiveEventSchema>(async () => await CreateAsync(liveEventName, location, properties, tags));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<LiveEventSchema> CreateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));
            Argument.AssertNotNullOrEmpty(location, nameof(location));
            Argument.AssertNotNull(properties, nameof(properties));

            return await CreateOrUpdateAsync(liveEventName, location, properties, tags, Client.CreateObjectAsync);
        }

        internal async Task<LiveEventSchema> CreateOrUpdateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags, Func<string, string, Task<string>> func)
        {
            var url = Client.GenerateApiUrl(_liveEventApiUrl, liveEventName);
            tags ??= new Dictionary<string, string>();
            var content = new LiveEventSchema { Location = location, Tags = tags, Properties = properties };
            string responseContent = await func(url, content.ToJson());
            return JsonConvert.DeserializeObject<LiveEventSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Delete(string liveEventName)
        {
            Task.Run(async () => await DeleteAsync(liveEventName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            var url = Client.GenerateApiUrl(_liveEventApiUrl, liveEventName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }

        /// <inheritdoc/>
        public void Start(string liveEventName)
        {
            Task.Run(async () => await StartAsync(liveEventName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task StartAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            await LiveEventOperationAsync(liveEventName, "start", HttpMethod.Post);
        }

        public void Stop(string liveEventName)
        {
            Task.Run(async () => await StopAsync(liveEventName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task StopAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            await LiveEventOperationAsync(liveEventName, "stop", HttpMethod.Post);
        }

        /// <inheritdoc/>
        public void Reset(string liveEventName)
        {
            Task.Run(async () => await ResetAsync(liveEventName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task ResetAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            await LiveEventOperationAsync(liveEventName, "reset", HttpMethod.Post);
        }

        /// <inheritdoc/>
        public void Allocate(string liveEventName)
        {
            Task.Run(async () => await AllocateAsync(liveEventName)).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task AllocateAsync(string liveEventName)
        {
            Argument.AssertNotNullOrEmpty(liveEventName, nameof(liveEventName));

            await LiveEventOperationAsync(liveEventName, "allocate", HttpMethod.Post);
        }

        private async Task LiveEventOperationAsync(string liveEventName, string? operation, HttpMethod httpMethod)
        {
            var url = Client.GenerateApiUrl(_liveEventApiUrl + (operation != null ? "/" + operation : string.Empty), liveEventName);
            await Client.ObjectContentAsync(url, httpMethod);
        }
    }
}