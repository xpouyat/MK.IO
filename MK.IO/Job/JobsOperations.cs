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

        private const string _allJobsApiUrl = MKIOClient._allJobsApiUrl;
        private const string _jobsApiUrl = MKIOClient._transformsApiUrl + "/jobs";
        private const string _jobApiUrl = _jobsApiUrl + "/{2}";

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
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public List<JobSchema> ListAll()
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAllAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<JobSchema>> ListAllAsync()
        {
            var url = Client.GenerateApiUrl(_allJobsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        /// <inheritdoc/>
        public List<JobSchema> List(string transformName)
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<JobSchema>> ListAsync(string transformName)
        {
            var url = Client.GenerateApiUrl(_jobsApiUrl, transformName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        /// <inheritdoc/>
        public JobSchema Get(string transformName, string jobName)
        {
            var task = Task.Run<JobSchema>(async () => await GetAsync(transformName, jobName));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<JobSchema> GetAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(_jobApiUrl, transformName, jobName);
            string responseContent = await Client.GetObjectContentAsync(url);
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public JobSchema Create(string jobName, string transformName, JobProperties properties)
        {
            var task = Task.Run<JobSchema>(async () => await CreateAsync(transformName, jobName, properties));
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<JobSchema> CreateAsync(string jobName, string transformName, JobProperties properties)
        {
            var url = Client.GenerateApiUrl(_jobApiUrl, transformName, jobName);
            // fix to make sure Odattype is set as we use the generated class
            foreach (var o in properties.Outputs)
            {
                o.Odatatype = "#Microsoft.Media.JobOutputAsset";
            }
            var content = new JobSchema { Properties = properties };

            string responseContent = await Client.CreateObjectAsync(url, content.ToJson());
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        /// <inheritdoc/>
        public void Cancel(string transformName, string jobName)
        {
            Task.Run(async () => await CancelAsync(transformName, jobName));
        }

        /// <inheritdoc/>
        public async Task CancelAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(_jobApiUrl + "/cancelJob", transformName, jobName);
            await Client.ObjectContentAsync(url, HttpMethod.Post);
        }

        /// <inheritdoc/>
        public void Delete(string transformName, string jobName)
        {
            Task.Run(async () => await DeleteAsync(transformName, jobName));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string transformName, string jobName)
        {
            var url = Client.GenerateApiUrl(_jobApiUrl, transformName, jobName);
            await Client.ObjectContentAsync(url, HttpMethod.Delete);
        }
    }
}