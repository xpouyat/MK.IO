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
    internal class JobsOperations : IJobsOperations
    {
        //
        // jobs
        //

        private const string AllJobsApiUrl = MKIOClient.AllJobsApiUrl;
        private const string JobsApiUrl = MKIOClient.TransformsApiUrl + "/jobs";
        private const string JobApiUrl = JobsApiUrl + "/{2}";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the JobsOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal JobsOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }

        public List<JobSchema> ListAll()
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAllAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<JobSchema>> ListAllAsync()
        {
            var url = Client.GenerateApiUrl(AllJobsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public List<JobSchema> List(string transformName)
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<JobSchema>> ListAsync(string transformName)
        {
            var url = Client.GenerateApiUrl(JobsApiUrl, transformName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public JobSchema Get(string transformName, string jobName)
        {
            var task = Task.Run<JobSchema>(async () => await GetAsync(transformName, jobName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> GetAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(JobApiUrl, transformName, jobName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public JobSchema Create(string transformName, string jobName, JobProperties properties)
        {
            var task = Task.Run<JobSchema>(async () => await CreateAsync(transformName, jobName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> CreateAsync(string transformName, string jobName, JobProperties properties)
        {
            var url = Client.GenerateApiUrl(JobApiUrl, transformName, jobName);
            // fix to make sure Odattype is set as we use the generated class
            foreach (var o in properties.Outputs)
            {
                o.Odatatype = "#Microsoft.Media.JobOutputAsset";
            }
            var content = new JobSchema { Properties = properties };

            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public void Cancel(string transformName, string jobName)
        {
            Task.Run(async () => await CancelAsync(transformName, jobName));
        }

        public async Task CancelAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(JobApiUrl + "/cancelJob", transformName, jobName);
            await Client.ObjectContentAsync(url, HttpMethod.Post);
        }

        public void Delete(string transformName, string jobName)
        {
            Task.Run(async () => await DeleteAsync(transformName, jobName));
        }

        public async Task DeleteAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(JobApiUrl, transformName, jobName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}