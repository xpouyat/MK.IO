// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface ILiveOutputsOperations
    {
        /// <summary>
        /// Returns a list of Live Outputs for a Live Event.
        /// </summary>
        /// <returns></returns>
        List<LiveOutputSchema> List(string liveEventName);

        /// <summary>
        /// Returns a list of Live Outputs for a Live Event.
        /// </summary>
        /// <returns></returns>
        Task<List<LiveOutputSchema>> ListAsync(string liveEventName);

        /// <summary>
        /// Deletes a Live Output.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        void Delete(string liveEventName, string liveOutputName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        /// <returns></returns>
        Task DeleteAsync(string liveEventName, string liveOutputName);

        /// <summary>
        /// Returns a single Live Output.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        /// <returns></returns>
        LiveOutputSchema Get(string liveEventName, string liveOutputName);

        /// <summary>
        /// Returns a single Live Output.
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        /// <returns></returns>
        Task<LiveOutputSchema> GetAsync(string liveEventName, string liveOutputName);

        /// <summary>
        /// Creates a Live Output
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        LiveOutputSchema Create(string liveEventName, string liveOutputName, LiveOutputProperties properties);

        /// <summary>
        /// Creates a Live Output
        /// </summary>
        /// <param name="liveEventName"></param>
        /// <param name="liveOutputName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<LiveOutputSchema> CreateAsync(string liveEventName, string liveOutputName, LiveOutputProperties properties);
    }
}