// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public interface ISubscriptionOperations
    {
        string SubscriptionName { get; }
        Guid SubscriptionId { get; }
        Guid CustomerId { get; }

        AccountStats GetStats();
        Task<AccountStats> GetStatsAsync();
        UserInfo GetUserInfo();
        Task<UserInfo> GetUserInfoAsync();
    }
}