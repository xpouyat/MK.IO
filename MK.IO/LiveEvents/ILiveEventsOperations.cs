// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface ILiveEventsOperations
    {
        void Allocate(string liveEventName);
        Task AllocateAsync(string liveEventName);
        LiveEventSchema Create(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null);
        Task<LiveEventSchema> CreateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags);
        void Delete(string liveEventName);
        Task DeleteAsync(string liveEventName);
        LiveEventSchema Get(string liveEventName);
        Task<LiveEventSchema> GetAsync(string liveEventName);
        List<LiveEventSchema> List();
        Task<List<LiveEventSchema>> ListAsync();
        void Reset(string liveEventName);
        Task ResetAsync(string liveEventName);
        void Start(string liveEventName);
        Task StartAsync(string liveEventName);
        void Stop(string liveEventName);
        Task StopAsync(string liveEventName);
    }
}