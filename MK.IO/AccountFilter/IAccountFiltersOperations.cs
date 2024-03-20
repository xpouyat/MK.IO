using MK.IO.Models;

namespace MK.IO.Operations
{
    public interface IAccountFiltersOperations
    {
        /// <summary>
        /// Retrieves a list of account filters in the subscription.
        /// </summary>
        /// <returns></returns>
        List<AccountFilterSchema> List();

        /// <summary>
        /// Retrieves a list of account filters in the subscription.
        /// </summary>
        /// <returns></returns>
        Task<List<AccountFilterSchema>> ListAsync();

        /// <summary>
        /// Delete the account filter.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <returns></returns>
        void Delete(string accountFilterName);

        /// <summary>
        /// Delete the account filter.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <returns></returns>
        Task DeleteAsync(string accountFilterName);

        /// <summary>
        /// Get an account filter by name.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <returns></returns>
        AccountFilterSchema Get(string accountFilterName);

        /// <summary>
        /// Get an account filter by name.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <returns></returns>
        Task<AccountFilterSchema> GetAsync(string accountFilterName);

        /// <summary>
        /// Create or Update an account filter.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        AccountFilterSchema CreateOrUpdate(string accountFilterName, MediaFilterProperties properties);

        /// <summary>
        /// Create or Update an account filter.
        /// </summary>
        /// <param name="accountFilterName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<AccountFilterSchema> CreateOrUpdateAsync(string accountFilterName, MediaFilterProperties properties);

    }
}
