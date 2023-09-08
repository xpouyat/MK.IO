// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

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
        // account
        //
        private const string accountProfileApiUrl = "api/profile";
        private const string accountStatsApiUrl = "api/ams/{0}/stats";

        public AccountStats GetStats()
        {
            Task<AccountStats> task = Task.Run<AccountStats>(async () => await GetStatsAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<AccountStats> GetStatsAsync()
        {
            string URL = GenerateApiUrl(accountStatsApiUrl);
            string responseContent = await GetObjectContentAsync(URL);
            return AccountStats.FromJson(responseContent);
        }


        public UserInfo GetUserInfo()
        {
            Task<UserInfo> task = Task.Run<UserInfo>(async () => await GetUserInfoAsync());
            return task.GetAwaiter().GetResult();
        }

        public async Task<UserInfo> GetUserInfoAsync()
        {
            string responseContent = await GetObjectContentAsync(baseUrl + accountProfileApiUrl);
            return AccountProfile.FromJson(responseContent).Spec;
        }

    }
}