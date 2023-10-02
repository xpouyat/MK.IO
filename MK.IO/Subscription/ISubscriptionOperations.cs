// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public interface ISubscriptionOperations
    {
        /// <summary>
        /// Get statistics for the subscription.
        /// </summary>
        /// <returns></returns>
        AccountStats GetStats();

        /// <summary>
        /// Get statistics for the subscription.
        /// </summary>
        /// <returns></returns>
        Task<AccountStats> GetStatsAsync();

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <returns></returns>
        UserInfo GetUserInfo();

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoAsync();
    }
}