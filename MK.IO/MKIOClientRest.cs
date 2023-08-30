﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClientRest
    {
        private string baseUrl = "https://api.io.mediakind.com/";
        private string _MKIOSubscriptionName;
        private string _MKIOtoken;
        private HttpClient _httpClient;

        public MKIOClientRest(string MKIOSubscriptionName, string MKIOtoken)
        {
            _MKIOSubscriptionName = MKIOSubscriptionName;
            _MKIOtoken = MKIOtoken;

            _httpClient = new HttpClient();
            // Request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string GenerateApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName);
        }
        private string GenerateApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName);
        }


        private async Task<string> GetObjectContentAsync(string url)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);

            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
            }
            return responseContent;
        }

        private async Task<string> ObjectContentAsync(string url, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);

            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
            }

            return responseContent;
        }


        private async Task<string> CreateObjectAsync(string url, string amsJSONObject)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Put,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);
            request.Content = new StringContent(amsJSONObject, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!amsRequestResult.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<dynamic>(responseContent);
                throw new Exception((string)error.error.detail);
            }

            if (amsRequestResult.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                // let's wait for the operation to complete
                var monitorUrl = amsRequestResult.Headers.Where(h => h.Key == "Azure-AsyncOperation").FirstOrDefault().Value.FirstOrDefault();
                int monitorDelay = 1000 * int.Parse(amsRequestResult.Headers.Where(h => h.Key == "Retry-After").FirstOrDefault().Value.FirstOrDefault());
                bool notComplete = true;
                do
                {
                    await Task.Delay(monitorDelay);
                    HttpResponseMessage amsRequestResultWait = await _httpClient.GetAsync(monitorUrl).ConfigureAwait(false);
                    string responseContentWait = await amsRequestResultWait.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic data = JsonConvert.DeserializeObject(responseContentWait);
                    notComplete = data.status == "InProgress";
                }
                while (notComplete);
            }
            return responseContent;
        }
    }
}