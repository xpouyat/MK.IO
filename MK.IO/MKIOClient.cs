// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Asset;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
#if NET462
using System.Net.Http;
#endif

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public class MKIOClient : IMKIOClient
    {
        internal readonly string _baseUrl = "https://api.io.mediakind.com/";
        internal const string _allJobsApiUrl = "api/ams/{0}/jobs";
        internal const string _transformsApiUrl = "api/ams/{0}/transforms";
        internal const string _assetsApiUrl = "api/ams/{0}/assets";
        internal const string _streamingLocatorsApiUrl = "api/ams/{0}/streamingLocators";
        internal const string _liveEventsApiUrl = "api/ams/{0}/liveEvents";
        internal const string _contentKeyPoliciesApiUrl = "api/ams/{0}/contentKeyPolicies";
        internal const string _streamingEndpointsApiUrl = "api/ams/{0}/streamingEndpoints";
        internal const string _accountFiltersApiUrl = "api/ams/{0}/accountFilters";

        private readonly string _subscriptionName;
        private readonly string _apiToken;
        private readonly HttpClient _httpClient;

        private Guid _subscriptionId;
        internal Guid GetSubscriptionId()
        {
            if (default == _subscriptionId)
            {
                _subscriptionId = Account.GetSubscriptionStats().Extra.SubscriptionId;
            }
            return _subscriptionId;
        }

        private Guid _customerId;
        internal Guid GetCustomerId()
        {
            if (default == _customerId)
            {
                _customerId = Account.GetUserProfile().CustomerId;
            }
            return _customerId;
        }

        public MKIOClient(string MKIOSubscriptionName, string MKIOtoken)
        {
            Argument.AssertNotNullOrEmpty(MKIOSubscriptionName, nameof(MKIOSubscriptionName));
            Argument.AssertNotNullOrEmpty(MKIOtoken, nameof(MKIOtoken));

            _subscriptionName = MKIOSubscriptionName;
            _apiToken = MKIOtoken;
            _httpClient = new HttpClient();

            // Request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Initialize();
        }

        private void Initialize()
        {
            Account = new AccountOperations(this);
            StorageAccounts = new StorageAccountsOperations(this);
            Assets = new AssetsOperations(this);
            LiveEvents = new LiveEventsOperations(this);
            LiveOutputs = new LiveOutputsOperations(this);
            Jobs = new JobsOperations(this);
            StreamingEndpoints = new StreamingEndpointsOperations(this);
            Transforms = new TransformsOperations(this);
            StreamingLocators = new StreamingLocatorsOperations(this);
            ContentKeyPolicies = new ContentKeyPoliciesOperations(this);
            AccountFilters = new AccountFiltersOperations(this);
            AssetFilters = new AssetFiltersOperations(this);
        }

        /// <inheritdoc/>
        public virtual IAccountOperations Account { get; private set; }

        /// <inheritdoc/>
        public virtual IStorageAccountsOperations StorageAccounts { get; private set; }

        /// <inheritdoc/>
        public virtual IAssetsOperations Assets { get; private set; }

        /// <inheritdoc/>
        public virtual ILiveEventsOperations LiveEvents { get; private set; }

        /// <inheritdoc/>
        public virtual ILiveOutputsOperations LiveOutputs { get; private set; }

        /// <inheritdoc/>
        public virtual IJobsOperations Jobs { get; private set; }

        /// <inheritdoc/>
        public virtual IStreamingEndpointsOperations StreamingEndpoints { get; private set; }

        /// <inheritdoc/>
        public virtual ITransformsOperations Transforms { get; private set; }

        /// <inheritdoc/>
        public virtual IStreamingLocatorsOperations StreamingLocators { get; private set; }

        /// <inheritdoc/>
        public virtual IContentKeyPoliciesOperations ContentKeyPolicies { get; private set; }

        /// <inheritdoc/>
        public virtual IAccountFiltersOperations AccountFilters { get; private set; }

        /// <inheritdoc/>
        public virtual IAssetFiltersOperations AssetFilters { get; private set; }

        internal string GenerateApiUrl(string urlPath, string objectName1, string objectName2)
        {
            return _baseUrl + string.Format(urlPath, _subscriptionName, objectName1, objectName2);
        }
        internal string GenerateApiUrl(string urlPath, string objectName)
        {
            return _baseUrl + string.Format(urlPath, _subscriptionName, objectName);
        }
        internal string GenerateApiUrl(string urlPath)
        {
            return _baseUrl + string.Format(urlPath, _subscriptionName);
        }

        internal async Task<string> GetObjectContentAsync(string url)
        {
            return await ObjectContentAsync(url, HttpMethod.Get);
        }

        internal async Task<string> GetObjectPostContentAsync(string url)
        {
            return await ObjectContentAsync(url, HttpMethod.Post);
        }

        internal async Task<string> ObjectContentAsync(string url, HttpMethod httpMethod)
        {
            using HttpRequestMessage request = new()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _apiToken);

            using HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            AnalyzeResponseAndThrowIfNeeded(amsRequestResult, responseContent);
            return responseContent;
        }

        internal async Task<string> CreateObjectPutAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Put);
        }

        internal async Task<string> CreateObjectPostAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Post);
        }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        internal async Task<string> UpdateObjectPatchAsync(string url, string amsJSONObject)
        {
            return await CreateObjectInternalAsync(url, amsJSONObject, HttpMethod.Patch);
        }
#endif

        internal async Task<string> CreateObjectInternalAsync(string url, string amsJSONObject, HttpMethod httpMethod)
        {
            using HttpRequestMessage request = new()
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
            };
            request.Headers.Add("x-mkio-token", _apiToken);
            request.Content = new StringContent(amsJSONObject, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage amsRequestResult = await _httpClient.SendAsync(request).ConfigureAwait(false);

            string responseContent = await amsRequestResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            AnalyzeResponseAndThrowIfNeeded(amsRequestResult, responseContent);

            if (amsRequestResult.StatusCode == HttpStatusCode.Accepted)
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

        private static void AnalyzeResponseAndThrowIfNeeded(HttpResponseMessage amsRequestResult, string responseContent)
        {
            var status_ = (int)amsRequestResult.StatusCode;

            var message = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (amsRequestResult.IsSuccessStatusCode)
            {
                if (message == null)
                {
                    // commented. In case of Account filter deletion, message is null, code 204. But all seems ok.
                    //throw new ApiException("Response was null which was not expected.", status_, null, null);
                }
            }
            else
            {
                string? errorDetail = null;
                if (message != null && message.ContainsKey("error"))
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
                if (status_ == 429)
                {
                    if (message == null)
                    {
                        throw new ApiException("Response was null which was not expected.", status_, null, null);
                    }
                    throw new ApiException("Too Many Requests" + errorDetail, status_, responseContent, null);
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

        internal static string AddParametersToUrl(string url, string name, List<string>? value = null)
        {
            if (value != null)
            {
                foreach (var v in value)
                {
                    url = AddParametersToUrl(url, name, v);
                }
            }

            return url;
        }

        internal static string AddParametersToUrl(string url, string name, string? value = null)
        {
            if (value != null)
            {
                UriBuilder baseUri = new(url);
                var queryString = WebUtility.UrlDecode(baseUri.Query).Split('&');

                if (queryString.Count() == 1 && string.IsNullOrEmpty(queryString[0]))
                {
                    url += '?';
                }
                else
                {
                    url += '&';
                }

                url += Uri.EscapeUriString(name + '=' + value);
            }

            return url;
        }
 

        /// <summary>
        /// Generates a unique name based on a prefix. Useful for creating unique names for assets, locators, etc.
        /// </summary>
        /// <param name="prefix">Prefix of the name</param>
        /// <param name="length">Lenght of the unique name (without the '-' before)</param>
        /// <returns></returns>
        public static string GenerateUniqueName(string prefix, int length = 8)
        {
            // return a string of length "length" containing random characters

            return prefix + "-" + Guid.NewGuid().ToString("N").Substring(0, length);
        }
    }
}