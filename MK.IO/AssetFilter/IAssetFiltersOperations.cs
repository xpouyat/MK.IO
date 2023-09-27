using MK.IO.Models;

namespace MK.IO.Asset
{
    public interface IAssetFiltersOperations
    {
        /// <summary>
        /// Retrieves a list of asset filters for a specified asset.
        /// </summary>
        /// <returns></returns>
        List<AssetFilterSchema> List(string assetName);

        /// <summary>
        /// Retrieves a list of asset filters for a specified asset.
        /// </summary>
        /// <returns></returns>
        Task<List<AssetFilterSchema>> ListAsync(string assetName);

        /// <summary>
        /// Delete the asset filter.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <returns></returns>
        void Delete(string assetName, string filterName);

        /// <summary>
        /// Delete the asset filter.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <returns></returns>
        Task DeleteAsync(string assetName, string filterName);

        /// <summary>
        /// Get an asset filter by name.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <returns></returns>
        AssetFilterSchema Get(string assetName, string filterName);

        /// <summary>
        /// Get an asset filter by name.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <returns></returns>
        Task<AssetFilterSchema> GetAsync(string assetName, string filterName);

        /// <summary>
        /// Create or Update an asset filter.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        AssetFilterSchema CreateOrUpdate(string assetName, string filterName, MediaFilterProperties properties);

        /// <summary>
        /// Create or Update an asset filter.
        /// </summary>
        /// <param name="assetFilterName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<AssetFilterSchema> CreateOrUpdateAsync(string assetName, string filterName, MediaFilterProperties properties);
    }
}
