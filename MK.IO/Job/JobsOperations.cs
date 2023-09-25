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
    public class JobsOperations : IJobsOperations
    {
        //
        // jobs
        //

        private const string allJobsApiUrl = MKIOClient.allJobsApiUrl;
        private const string jobsApiUrl = MKIOClient.transformsApiUrl + "/jobs";
        private const string jobApiUrl = jobsApiUrl + "/{2}";

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
            string URL = Client.GenerateApiUrl(allJobsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public List<JobSchema> List(string transformName)
        {
            var task = Task.Run<List<JobSchema>>(async () => await ListAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<JobSchema>> ListAsync(string transformName)
        {
            string URL = Client.GenerateApiUrl(jobsApiUrl, transformName);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobListResponseSchema>(responseContent, ConverterLE.Settings).Value;
        }

        public JobSchema Get(string transformName, string jobName)
        {
            var task = Task.Run<JobSchema>(async () => await GetAsync(transformName, jobName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> GetAsync(string transformName, string jobName)
        {
            string URL = Client.GenerateApiUrl(jobApiUrl, transformName, jobName);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public JobSchema Create(string transformName, string jobName, JobProperties properties)
        {
            var task = Task.Run<JobSchema>(async () => await CreateAsync(transformName, jobName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<JobSchema> CreateAsync(string transformName, string jobName, JobProperties properties)
        {
            string URL = Client.GenerateApiUrl(jobApiUrl, transformName, jobName);
            // fix to make sure Odattype is set as we use the generated class
            foreach (var o in properties.Outputs)
            {
                o.Odatatype = "#Microsoft.Media.JobOutputAsset";
            }
            var content = new JobSchema { Properties = properties };

            string responseContent = await Client.CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<JobSchema>(responseContent, ConverterLE.Settings);
        }

        public void Cancel(string transformName, string jobName)
        {
            Task.Run(async () => await CancelAsync(transformName, jobName));
        }

        public async Task CancelAsync(string transformName, string jobName)
        {
            string URL = Client.GenerateApiUrl(jobApiUrl + "/cancelJob", transformName, jobName);
            await Client.ObjectContentAsync(URL, HttpMethod.Post);
        }

        public void Delete(string transformName, string jobName)
        {
            Task.Run(async () => await DeleteAsync(transformName, jobName));
        }

        public async Task DeleteAsync(string transformName, string jobName)
        {
            string URL = Client.GenerateApiUrl(jobApiUrl, transformName, jobName);
            await Client.ObjectContentAsync(URL, HttpMethod.Delete);
        }
    }
}