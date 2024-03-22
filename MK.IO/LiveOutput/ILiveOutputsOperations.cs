// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface ILiveOutputsOperations
    {
        /// <summary>
        /// Returns a list of Live Outputs for a Live Event.
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <returns></returns>
        List<LiveOutputSchema> List(string liveEventName);

        /// <summary>
        /// Returns a list of Live Outputs for a Live Event.
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <returns></returns>
        Task<List<LiveOutputSchema>> ListAsync(string liveEventName);

        /// <summary>
        /// Deletes a Live Output.
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        void Delete(string liveEventName, string liveOutputName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        /// <returns></returns>
        Task DeleteAsync(string liveEventName, string liveOutputName);

        /// <summary>
        /// Returns a single Live Output.
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        /// <returns></returns>
        LiveOutputSchema Get(string liveEventName, string liveOutputName);

        /// <summary>
        /// Returns a single Live Output.
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        /// <returns></returns>
        Task<LiveOutputSchema> GetAsync(string liveEventName, string liveOutputName);

        /// <summary>
        /// Creates a Live Output
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        /// <param name="properties">The properties of the live output.</param>
        /// <returns></returns>
        LiveOutputSchema Create(string liveEventName, string liveOutputName, LiveOutputProperties properties);

        /// <summary>
        /// Creates a Live Output
        /// </summary>
        /// <param name="liveEventName">The name of the live event.</param>
        /// <param name="liveOutputName">The name of the live output.</param>
        /// <param name="properties">The properties of the live output.</param>
        /// <returns></returns>
        Task<LiveOutputSchema> CreateAsync(string liveEventName, string liveOutputName, LiveOutputProperties properties);
    }
}