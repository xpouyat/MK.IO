// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public interface IAccountOperations
    {
        AccountStats GetStats();
        Task<AccountStats> GetStatsAsync();
        UserInfo GetUserInfo();
        Task<UserInfo> GetUserInfoAsync();
    }
}