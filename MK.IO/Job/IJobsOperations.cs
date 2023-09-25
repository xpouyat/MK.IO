// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface IJobsOperations
    {
        void Cancel(string transformName, string jobName);
        Task CancelAsync(string transformName, string jobName);
        JobSchema Create(string transformName, string jobName, JobProperties properties);
        Task<JobSchema> CreateAsync(string transformName, string jobName, JobProperties properties);
        void Delete(string transformName, string jobName);
        Task DeleteAsync(string transformName, string jobName);
        JobSchema Get(string transformName, string jobName);
        Task<JobSchema> GetAsync(string transformName, string jobName);
        List<JobSchema> List(string transformName);
        List<JobSchema> ListAll();
        Task<List<JobSchema>> ListAllAsync();
        Task<List<JobSchema>> ListAsync(string transformName);
    }
}