// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Asset;
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
    public partial class MKIOClient : IMKIOClient
    {
        internal readonly string baseUrl = "https://api.io.mediakind.com/";
        internal readonly string _MKIOSubscriptionName;
        private readonly string _MKIOtoken;
        private readonly HttpClient _httpClient;
        private readonly Guid _subscription_id;
        private readonly Guid _customer_id;

        internal const string transformsApiUrl = "api/ams/{0}/transforms";
        internal const string assetsApiUrl = "api/ams/{0}/assets";


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

            Initialize();
        }

        private void Initialize()
        {
            Assets = new AssetsOperations(this);
            LiveEvents = new LiveEventsOperations(this);
            Jobs = new JobsOperations(this);
            StreamingEndpoints = new StreamingEndpointsOperations(this);
            Transforms = new TransformsOperations(this);
            StreamingLocators = new StreamingLocatorsOperations(this);
            ContentKeyPolicies = new ContentKeyPoliciesOperations(this);
        }

        /// <summary>
        /// Gets the IAssetsOperations.
        /// </summary>
        public virtual IAssetsOperations Assets { get; private set; }

        /// <summary>
        /// Gets the ILiveEventsOperations.
        /// </summary>
        public virtual ILiveEventsOperations LiveEvents { get; private set; }

        /// <summary>
        /// Gets the IJobsOperations.
        /// </summary>
        public virtual IJobsOperations Jobs { get; private set; }

        /// <summary>
        /// Gets the IStreamingEndpointsOperations.
        /// </summary>
        public virtual IStreamingEndpointsOperations StreamingEndpoints { get; private set; }

        /// <summary>
        /// Gets the ITransformsOperations.
        /// </summary>
        public virtual ITransformsOperations Transforms { get; private set; }

        /// <summary>
        /// Gets the ITransformsOperations.
        /// </summary>
        public virtual IStreamingLocatorsOperations StreamingLocators { get; private set; }

        /// <summary>
        /// Gets the ITransformsOperations.
        /// </summary>
        public virtual IContentKeyPoliciesOperations ContentKeyPolicies { get; private set; }


        string IMKIOClient.baseUrl => baseUrl;

        string IMKIOClient._MKIOSubscriptionName => _MKIOSubscriptionName;


        public string GenerateApiUrl(string urlPath, string objectName1, string objectName2)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName1, objectName2);
        }
        public string GenerateApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName, objectName);
        }
        public string GenerateApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _MKIOSubscriptionName);
        }

        public string GenerateStorageApiUrl(string urlPath)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id);
        }

        public string GenerateStorageApiUrl(string urlPath, string objectName)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id, objectName);
        }
        public string GenerateStorageApiUrl(string urlPath, string objectName, string objectName2)
        {
            return baseUrl + string.Format(urlPath, _customer_id, _subscription_id, objectName, objectName2);
        }

        internal async Task<string> GetObjectContentAsync(string url)
        {
            return await ObjectContentAsync(url, HttpMethod.Get);
        }

        internal async Task<string> ObjectContentAsync(string url, HttpMethod httpMethod)
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

        internal async Task<string> CreateObjectAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Put);
        }

        internal async Task<string> CreateObjectPostAsync(string url, string amsJSONObject)
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

        internal static string AddParametersToUrl(string url, string name, string? value = null)
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



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}