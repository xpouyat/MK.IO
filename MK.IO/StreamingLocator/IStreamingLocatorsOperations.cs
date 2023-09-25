// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IStreamingLocatorsOperations
    {
        StreamingLocatorSchema Create(string streamingLocatorName, StreamingLocatorProperties properties);
        Task<StreamingLocatorSchema> CreateAsync(string streamingLocatorName, StreamingLocatorProperties properties);
        void Delete(string streamingLocatorName);
        Task DeleteAsync(string streamingLocatorName);
        StreamingLocatorSchema Get(string streamingLocatorName);
        Task<StreamingLocatorSchema> GetAsync(string streamingLocatorName);
        List<StreamingLocatorSchema> List();
        Task<List<StreamingLocatorSchema>> ListAsync();
        StreamingLocatorListPathsResponseSchema ListUrlPaths(string streamingLocatorName);
        Task<StreamingLocatorListPathsResponseSchema> ListUrlPathsAsync(string streamingLocatorName);
    }
}