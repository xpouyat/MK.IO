// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial class MKIOClient
    {
        private readonly string baseUrl = "https://api.io.mediakind.com/";
        private readonly string _MKIOSubscriptionName;
        private readonly string _MKIOtoken;
        private readonly HttpClient _httpClient;
        private readonly Guid _subscription_id;
        private readonly Guid _customer_id;

        public MKIOClient(string MKIOSubscriptionName, string MKIOtoken)
        {
            if (MKIOSubscriptionName == null)
                throw new System.ArgumentNullException(nameof(MKIOSubscriptionName));

            if (MKIOtoken == null)
                throw new System.ArgumentNullException(nameof(MKIOtoken));

            _MKIOSubscriptionName = MKIOSubscriptionName;
            _MKIOtoken = MKIOtoken;

            _httpClient = new HttpClient();

            // Request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _subscription_id = GetStats().Extra.SubscriptionId;
            _customer_id = GetUserInfo().CustomerId;
        }

        private string GenerateApiUrl(string urlPath, string objectName1, string objectName2)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName1, objectName2);
        }
        private string GenerateApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName);
        }
        private string GenerateApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName);
        }

        private string GenerateStorageApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id);
        }

        private string GenerateStorageApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id, objectName);
        }
        private string GenerateStorageApiUrl(string urlPath, string objectName, string objectName2)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id, objectName, objectName2);
        }

        private async Task<string> GetObjectContentAsync(string url)
        {
            return await ObjectContentAsync(url, HttpMethod.Get);
        }

        private async Task<string> ObjectContentAsync(string url, HttpMethod httpMethod)
        {
            using HttpRequestMessage request = new()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);

            using HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            AnalyzeResponseAndThrowIfNeeded(amsRequestResult, responseContent);
            return responseContent;
        }

        private async Task<string> CreateObjectAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Put);
        }

        private async Task<string> CreateObjectPostAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Post);
        }

        internal async Task<string> CreateObjectInternalAsync(string url, string amsJSONObject, HttpMethod httpMethod)
        {
            using HttpRequestMessage request = new()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _MKIOtoken);
            request.Content = new StringContent(amsJSONObject, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            AnalyzeResponseAndThrowIfNeeded(amsRequestResult, responseContent);

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

        public partial class ApiException : Exception
        {
            public int StatusCode { get; private set; }

            public string? Response { get; private set; }

            public ApiException(string message, int statusCode, string? response, Exception? innerException)
                : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
            // : base(message , innerException)
            {
                StatusCode = statusCode;
                Response = response;
            }

            public override string ToString()
            {
                return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
            }
        }


        private static void AnalyzeResponseAndThrowIfNeeded(HttpResponseMessage amsRequestResult, string responseContent)
        {
            var status_ = (int)amsRequestResult.StatusCode;

            var message = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (amsRequestResult.IsSuccessStatusCode)
            {
                if (message == null)
                {
                    throw new ApiException("Response was null which was not expected.", status_, null, null);
                }
            }
            else
            {
                string? errorDetail = null;
                if (message.ContainsKey("error"))
                {
                    try
                    {
                        errorDetail = (string)message.error.detail;
                    }
                    catch
                    {

                    }

                    if (string.IsNullOrEmpty(errorDetail))
                    {

                        errorDetail = (string)message.error;
                    }
                }
                if (errorDetail != null)
                {
                    errorDetail = " : " + errorDetail;
                }

                if (status_ == 400)
                {
                    if (message == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status_, null, null);
                    }
                    throw new ApiException("Bad Request" + errorDetail, status_, responseContent, null);
                }
                else
               if (status_ == 403)
                {
                    if (message == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status_, null, null);
                    }
                    throw new ApiException("Forbidden" + errorDetail, status_, responseContent, null);
                }
                else
                if (status_ == 404)
                {
                    if (message == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status_, null, null);
                    }
                    throw new ApiException("Not Found" + errorDetail, status_, responseContent, null);
                }
                else
                if (status_ == 500)
                {
                    if (message == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status_, null, null);
                    }
                    throw new ApiException("Internal Server Error" + errorDetail, status_, responseContent, null);

                }
                else
                {
                    throw new ApiException("The HTTP status code of the response was not expected(" + status_ + ").", status_, responseContent, null);
                }
            }
        }

        private static string AddParametersToUrl(string url, string name, string? value = null)
        {
            if (value != null)
            {
                UriBuilder baseUri = new UriBuilder(url);
                NameValueCollection queryString = HttpUtility.ParseQueryString(baseUri.Query);

                if (!queryString.HasKeys())
                {
                    url += '?';
                }
                else
                {
                    url += '&';
                }

                url += HttpUtility.UrlPathEncode(name + '=' + value);
            }

            return url;
        }
    }
}