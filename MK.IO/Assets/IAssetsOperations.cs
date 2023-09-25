using MK.IO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.IO.Asset
{
    public interface IAssetsOperations
    {
        List<AssetSchema> List(string? orderBy = null, int? top = null);
        Task<List<AssetSchema>> ListAsync(string? orderBy = null, int? top = null);

        PagedResult<AssetSchema> ListAsPage(string? orderBy = null, int? top = null);
        Task<PagedResult<AssetSchema>> ListAsPageAsync(string? orderBy = null, int? top = null);

        PagedResult<AssetSchema> ListAsPageNext(string? nextPageLink);
        Task<PagedResult<AssetSchema>> ListAsPageNextAsync(string? nextPageLink);

        AssetSchema Get(string assetName);
        Task<AssetSchema> GetAsync(string assetName);

        void Delete(string assetName);
        Task DeleteAsync(string assetName);

        AssetSchema CreateOrUpdate(string assetName, string containerName, string storageName, string description = null);
        Task<AssetSchema> CreateOrUpdateAsync(string assetName, string containerName, string storageName, string description = null);

        List<AssetStreamingLocator> ListStreamingLocators(string assetName);
        Task<List<AssetStreamingLocator>> ListStreamingLocatorsAsync(string assetName);

        AssetStorageResponseSchema ListTracksAndDirListing(string assetName);
        Task<AssetStorageResponseSchema> ListTracksAndDirListingAsync(string assetName);
    }
}
