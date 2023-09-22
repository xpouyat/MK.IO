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
        // jobs
        //
        private const string allJobsApiUrl = "api/ams/{0}/jobs";

        private const string jobsApiUrl = transformApiUrl + "/jobs";
        private const string jobApiUrl = jobsApiUrl + "/{2}";


        public List<JobSchema> ListAllJobs()
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAllJobsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<JobSchema>> ListAllJobsAsync()
        {
            string URL = GenerateApiUrl(allJobsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public List<JobSchema> ListJobs(string transformName)
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListJobsAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<JobSchema>> ListJobsAsync(string transformName)
        {
            string URL = GenerateApiUrl(jobsApiUrl, transformName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public JobSchema GetJob(string transformName, string jobName)
        {
            var task = Task.Run<JobSchema>(async () => await GetJobAsync(transformName, jobName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> GetJobAsync(string transformName, string jobName)
        {
            string URL = GenerateApiUrl(jobApiUrl, transformName, jobName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public JobSchema CreateJob(string transformName, string jobName, JobProperties properties)
        {
            var task = Task.Run<JobSchema>(async () => await CreateJobAsync(transformName, jobName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> CreateJobAsync(string transformName, string jobName, JobProperties properties)
        {
            string URL = GenerateApiUrl(jobApiUrl, transformName, jobName);
            // fix to make sure Odattype is set as we use the generated class
            foreach (var o in properties.Outputs)
            {
                o.Odatatype = "#Microsoft.Media.JobOutputAsset";
            }
            var content = new JobSchema { Properties = properties };
            
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public void CancelJob(string transformName, string jobName)
        {
            Task.Run(async () => await CancelJobAsync(transformName, jobName));
        }

        public async Task CancelJobAsync(string transformName, string jobName)
        {
            string URL = GenerateApiUrl(jobApiUrl + "/cancelJob", transformName, jobName);
            await ObjectContentAsync(URL, HttpMethod.Post);
        }

        public void DeleteJob(string transformName, string jobName)
        {
            Task.Run(async () => await DeleteJobAsync(transformName, jobName));
        }

        public async Task DeleteJobAsync(string transformName, string jobName)
        {
            string URL = GenerateApiUrl(jobApiUrl, transformName, jobName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }
    }
}