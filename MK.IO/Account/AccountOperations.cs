// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    internal class AccountOperations : IAccountOperations
    {
        //
        // subscription operations
        //
        private const string _accountProfileApiUrl = "api/profile/";
        private const string _accountStatsApiUrl = "api/ams/{0}/stats/";
        private const string _accountApiUrl = "api/accounts/{0}/";
        private const string _accountSubscriptionsUrl = _accountApiUrl + "subscriptions/";
        private const string _accountSubscriptionUrl = _accountSubscriptionsUrl + "{1}";
        private const string _locationsApiUrl = "api/locations/";

        /// <summary>
        /// Gets a reference to the AzureMediaServicesClient
        /// </summary>
        private MKIOClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the SubscriptionOperations class.
        /// </summary>
        /// <param name='client'>
        /// Reference to the service client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        internal AccountOperations(MKIOClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public AccountStats GetSubscriptionStats()
        {
            var task = Task.Run(async () => await GetSubscriptionStatsAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<AccountStats> GetSubscriptionStatsAsync()
        {
            var url = Client.GenerateApiUrl(_accountStatsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(url);
            return AccountStats.FromJson(responseContent);
        }

        /// <inheritdoc/>
        public UserInfo GetUserProfile()
        {
            var task = Task.Run(async () => await GetUserProfileAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<UserInfo> GetUserProfileAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(Client._baseUrl + _accountProfileApiUrl);
            return AccountProfile.FromJson(responseContent).Spec;
        }

        /// <inheritdoc/>
        public List<SubscriptionResponseSchema> ListAllSubscriptions()
        {
            var task = Task.Run(async () => await ListAllSubscriptionsAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<SubscriptionResponseSchema>> ListAllSubscriptionsAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(GenerateAccountApiUrl(_accountSubscriptionsUrl));
            return SubscriptionListResponseSchema.FromJson(responseContent).Items;
        }

        /// <inheritdoc/>
        public SubscriptionResponseSchema GetSubscription()
        {
            var task = Task.Run(async () => await GetSubscriptionAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<SubscriptionResponseSchema> GetSubscriptionAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(GenerateSubscriptionApiUrl(_accountSubscriptionUrl));
            return SubscriptionResponseSchema.FromJson(responseContent);
        }

        /// <inheritdoc/>
        public List<LocationResponseSchema> ListAllLocations()
        {
            var task = Task.Run(async () => await ListAllLocationsAsync());
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public async Task<List<LocationResponseSchema>> ListAllLocationsAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(Client._baseUrl + _locationsApiUrl);
            return LocationListResponseSchema.FromJson(responseContent).Items;
        }


        internal string GenerateAccountApiUrl(string urlPath)
        {
            return Client._baseUrl + string.Format(urlPath, Client.GetCustomerId());
        }

        internal string GenerateSubscriptionApiUrl(string urlPath)
        {
            return Client._baseUrl + string.Format(urlPath, Client.GetCustomerId(), Client.GetSubscriptionId());
        }
    }
}