// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface ILiveEventsOperations
    {
        /// <summary>
        /// Returns a complete list of all live events.
        /// </summary>
        /// <returns></returns>
        List<LiveEventSchema> List();

        /// <summary>
        /// Returns a complete list of all live events.
        /// </summary>
        /// <returns></returns>
        Task<List<LiveEventSchema>> ListAsync();

        /// <summary>
        /// Delete a live event.
        /// </summary>
        /// <param name="liveEventName"></param>
        void Delete(string liveEventName);

        /// <summary>
        /// Delete a live event.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        Task DeleteAsync(string liveEventName);

        /// <summary>
        /// Returns a single live event.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        LiveEventSchema Get(string liveEventName);

        /// <summary>
        /// Returns a single live event.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        Task<LiveEventSchema> GetAsync(string liveEventName);

#if NET7_0_OR_GREATER
        /// <summary>
        /// Update a live event
        /// </summary>
        /// <param name="liveEventName">The name of the live event</param>
        /// <param name="location">The location of the live event. This must match the configured location for your account.</param>
        /// <param name="properties">The properties of the live event.</param>
        /// <param name="tags">A dictionary of tags associated with the live event. Maximum number of tags: 16. Maximum length of a tag: 256 characters.</param>
        /// <returns></returns>
        LiveEventSchema Update(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null);

        /// <summary>
        /// Update a live event
        /// </summary>
        /// <param name="liveEventName">The name of the live event</param>
        /// <param name="location">The location of the live event. This must match the configured location for your account.</param>
        /// <param name="properties">The properties of the live event.</param>
        /// <param name="tags">A dictionary of tags associated with the live event. Maximum number of tags: 16. Maximum length of a tag: 256 characters.</param>
        /// <returns></returns>
        Task<LiveEventSchema> UpdateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags);
#endif

        /// <summary>
        /// Create a single live event
        /// </summary>
        /// <param name="liveEventName">The name of the live event</param>
        /// <param name="location">The location of the live event. This must match the configured location for your account.</param>
        /// <param name="properties">The properties of the live event.</param>
        /// <param name="tags">A dictionary of tags associated with the live event. Maximum number of tags: 16. Maximum length of a tag: 256 characters.</param>
        /// <returns></returns>
        LiveEventSchema Create(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags = null);

        /// <summary>
        /// Create a single live event
        /// </summary>
        /// <param name="liveEventName">The name of the live event</param>
        /// <param name="location">The location of the live event. This must match the configured location for your account.</param>
        /// <param name="properties">The properties of the live event.</param>
        /// <param name="tags">A dictionary of tags associated with the live event. Maximum number of tags: 16. Maximum length of a tag: 256 characters.</param>
        /// <returns></returns>
        Task<LiveEventSchema> CreateAsync(string liveEventName, string location, LiveEventProperties properties, Dictionary<string, string> tags);

        /// <summary>
        /// Allocates resources for a Live Event. A live event is in StandBy state after allocation completes, and is ready to start.
        /// </summary>
        /// <param name="liveEventName"></param>
        void Allocate(string liveEventName);

        /// <summary>
        /// Allocates resources for a Live Event. A live event is in StandBy state after allocation completes, and is ready to start.
        /// </summary>
        /// <param name="liveEventName"></param>
        Task AllocateAsync(string liveEventName);

        /// <summary>
        /// Resets a Live Event. All live outputs for the live event are deleted and the live event is stopped and will be started again.
        /// </summary>
        /// <param name="liveEventName"></param>
        void Reset(string liveEventName);

        /// <summary>
        /// Resets a Live Event. All live outputs for the live event are deleted and the live event is stopped and will be started again.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        Task ResetAsync(string liveEventName);

        /// <summary>
        /// Start Live Events. This operation transitions your Live Event into a running state
        /// </summary>
        /// <param name="liveEventName"></param>
        void Start(string liveEventName);

        /// <summary>
        /// Start Live Events. This operation transitions your Live Event into a running state
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        Task StartAsync(string liveEventName);

        /// <summary>
        /// Stops a Live Event. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="liveEventName"></param>
        void Stop(string liveEventName);

        /// <summary>
        /// Stops a Live Event. Any active playback sessions will be interrupted.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <returns></returns>
        Task StopAsync(string liveEventName);
    }
}