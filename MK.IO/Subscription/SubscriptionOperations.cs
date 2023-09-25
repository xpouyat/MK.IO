// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    /// <summary>
    /// REST Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    internal class SubscriptionOperations : ISubscriptionOperations
    {
        //
        // account
        //
        private const string accountProfileApiUrl = "api/profile";
        private const string accountStatsApiUrl = "api/ams/{0}/stats";

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
        internal SubscriptionOperations(MKIOClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;

            // let's call the two API to get info on subscription and exposed to the user and also to be used by the MKIO client.
            SubscriptionId = GetStats().Extra.SubscriptionId;
            CustomerId = GetUserInfo().CustomerId;
            SubscriptionName = client._subscriptionName;
        }

        public string SubscriptionName { get; private set; }
        public Guid SubscriptionId { get; private set; }
        public Guid CustomerId { get; private set; }

        public AccountStats GetStats()
        {
            Task<AccountStats> task = Task.Run<AccountStats>(async () => await GetStatsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<AccountStats> GetStatsAsync()
        {
            string URL = Client.GenerateApiUrl(accountStatsApiUrl);
            string responseContent = await Client.GetObjectContentAsync(URL);
            return AccountStats.FromJson(responseContent);
        }

        public UserInfo GetUserInfo()
        {
            Task<UserInfo> task = Task.Run<UserInfo>(async () => await GetUserInfoAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<UserInfo> GetUserInfoAsync()
        {
            string responseContent = await Client.GetObjectContentAsync(Client.baseUrl + accountProfileApiUrl);
            return AccountProfile.FromJson(responseContent).Spec;
        }
    }
}