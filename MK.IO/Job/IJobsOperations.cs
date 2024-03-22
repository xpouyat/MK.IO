// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IJobsOperations
    {
        /// <summary>
        /// Retrieves a list of all Jobs in a subscription.
        /// </summary>
        /// <returns></returns>
        List<JobSchema> ListAll();

        /// <summary>
        /// Retrieves a list of all Jobs in a subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<JobSchema>> ListAllAsync();

        /// <summary>
        /// Retrieves a list of Jobs for a Transform.
        /// </summary>
        /// <param name="transformName"></param>
        /// <returns></returns>
        List<JobSchema> List(string transformName);

        /// <summary>
        /// Retrieves a list of Jobs for a Transform.
        /// </summary>
        /// <param name="transformName"></param>
        /// <returns></returns>
        Task<List<JobSchema>> ListAsync(string transformName);

        /// <summary>
        /// Delete a Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        void Delete(string transformName, string jobName);

        /// <summary>
        /// Delete a Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        Task DeleteAsync(string transformName, string jobName);

        /// <summary>
        /// Get an transform by name.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        JobSchema Get(string transformName, string jobName);

        /// <summary>
        /// Get an transform by name.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        Task<JobSchema> GetAsync(string transformName, string jobName);

        /// <summary>
        /// Create a new Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        JobSchema Create(string transformName, string jobName, JobProperties properties);

        /// <summary>
        /// Create a new Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<JobSchema> CreateAsync(string transformName, string jobName, JobProperties properties);

        /// <summary>
        /// Cancel an already running Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        void Cancel(string transformName, string jobName);

        /// <summary>
        /// Cancel an already running Job.
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        Task CancelAsync(string transformName, string jobName);

        // TODO : implement update the job
    }
}