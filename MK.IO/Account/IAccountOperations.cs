// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IAccountOperations
    {
        /// <summary>
        /// Get statistics for the current MK/IO subscription.
        /// </summary>
        /// <returns></returns>
        AccountStats GetSubscriptionStats();

        /// <summary>
        /// Get statistics for the current MK/IO subscription.
        /// </summary>
        /// <returns></returns>
        Task<AccountStats> GetSubscriptionStatsAsync();

        /// <summary>
        /// Get user profile information.
        /// </summary>
        /// <returns></returns>
        UserInfo GetUserProfile();

        /// <summary>
        /// Get user profile information.
        /// </summary>
        /// <returns></returns>
        Task<UserInfo> GetUserProfileAsync();

        /// <summary>
        /// Get the list of all MK/IO subscriptions for the account.
        /// </summary>
        /// <returns></returns>
        List<SubscriptionResponseSchema> ListAllSubscriptions();

        /// <summary>
        /// Get the list of all MK/IO subscriptions for the account.
        /// </summary>
        /// <returns></returns>
        Task<List<SubscriptionResponseSchema>> ListAllSubscriptionsAsync();

        /// <summary>
        /// Get the current MK/IO subscription.
        /// </summary>
        /// <returns></returns>
        SubscriptionResponseSchema GetSubscription();

        /// <summary>
        /// Get the current MK/IO subscription.
        /// </summary>
        /// <returns></returns>
        Task<SubscriptionResponseSchema> GetSubscriptionAsync();

        /// <summary>
        /// List all possible locations for MK/IO (Ids and names).
        /// </summary>
        /// <returns></returns>
        List<LocationResponseSchema> ListAllLocations();

        /// <summary>
        /// List all possible locations for MK/IO (Ids and names).
        /// </summary>
        /// <returns></returns>
        Task<List<LocationResponseSchema>> ListAllLocationsAsync();
    }
}