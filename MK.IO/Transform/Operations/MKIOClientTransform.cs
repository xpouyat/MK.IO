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
        // transforms
        //
       // private const string transformsApiUrl = "api/ams/{0}/transforms";
        private const string transformApiUrl = transformsApiUrl + "/{1}";

        public List<TransformSchema> ListTransforms()
        {
            var task = Task.Run<List<TransformSchema>>(async () => await ListTransformsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<List<TransformSchema>> ListTransformsAsync()
        {
            string URL = GenerateApiUrl(transformsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<TransformListResponseSchema>(responseContent, ConverterLE.Settings).Value;

        }

        public TransformSchema GetTransform(string transformName)
        {
            var task = Task.Run<TransformSchema>(async () => await GetTransformAsync(transformName));
            return task.GetAwaiter().GetResult();
        }

        public async Task<TransformSchema> GetTransformAsync(string transformName)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            string responseContent = await GetObjectContentAsync(URL);
            return JsonConvert.DeserializeObject<TransformSchema>(responseContent, ConverterLE.Settings);
        }

        public TransformSchema CreateTransform(string transformName, TransformProperties properties)
        {
            var task = Task.Run<TransformSchema>(async () => await CreateTransformAsync(transformName, properties));
            return task.GetAwaiter().GetResult();
        }

        public async Task<TransformSchema> CreateTransformAsync(string transformName, TransformProperties properties)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            var content = new TransformSchema { Properties = properties };
            string responseContent = await CreateObjectAsync(URL, content.ToJson());
            return JsonConvert.DeserializeObject<TransformSchema>(responseContent, ConverterLE.Settings);
        }

        public void DeleteTransform(string transformName)
        {
            Task.Run(async () => await DeleteTransformAsync(transformName));
        }

        public async Task DeleteTransformAsync(string transformName)
        {
            string URL = GenerateApiUrl(transformApiUrl, transformName);
            await ObjectContentAsync(URL, HttpMethod.Delete);
        }
    }
}