using MK.IO.Models;
using System.Net.NetworkInformation;

namespace MK.IO.Asset
{
    public interface IAssetsOperations
    {
        /// <summary>
        /// Retrieves a list of assets in the subscription.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <returns></returns>
        List<AssetSchema> List(string? orderBy = null, int? top = null, string? filter = null);

        /// <summary>
        /// Retrieves a list of assets in the subscription.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <returns></returns>
        Task<List<AssetSchema>> ListAsync(string? orderBy = null, int? top = null, string? filter = null);

        /// <summary>
        /// Retrieves a list of assets in the subscription using pages.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <returns></returns>
        PagedResult<AssetSchema> ListAsPage(string? orderBy = null, int? top = null, string? filter = null);

        /// <summary>
        /// Retrieves a list of assets in the subscription using pages.
        /// </summary>
        /// <param name="orderBy">Specifies the key by which the result collection should be ordered.</param>
        /// <param name="top">Specifies a non-negative integer that limits the number of items returned from a collection. The service returns the number of available items up to but not greater than the specified value top.</param>
        /// <param name="filter">Restricts the set of items returned.</param>
        /// <returns></returns>
        Task<PagedResult<AssetSchema>> ListAsPageAsync(string? orderBy = null, int? top = null, string? filter = null);

        /// <summary>
        /// Retrieves a list of assets in the subscription using pages.
        /// </summary>
        /// <param name="nextPageLink">Next page link.</param>
        /// <returns></returns>
        PagedResult<AssetSchema> ListAsPageNext(string? nextPageLink);


        /// <summary>
        /// Retrieves a list of assets in the subscription using pages.
        /// </summary>
        /// <param name="nextPageLink">Next page link.</param>
        /// <returns></returns>
        Task<PagedResult<AssetSchema>> ListAsPageNextAsync(string? nextPageLink);

        /// <summary>
        /// Delete Asset.
        /// When you delete an asset, the underlying storage container will be deleted too.You can control this behavior by assigning a DeletionPolicy to your asset.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        void Delete(string assetName);

        /// <summary>
        /// Delete Asset.
        /// When you delete an asset, the underlying storage container will be deleted too.You can control this behavior by assigning a DeletionPolicy to your asset.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        Task DeleteAsync(string assetName);

        /// <summary>
        /// Get an asset by name.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        AssetSchema Get(string assetName);

        /// <summary>
        /// Get an asset by name.
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        Task<AssetSchema> GetAsync(string assetName);

        /// <summary>
        /// <para>Create or Update Asset.</para>
        /// <para>When creating an asset, that asset needs to have a unique container & storage account within the project.
        /// When updating an asset, only the description column can be updated.</para>
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <param name="containerName">The name of the asset blob container.</param>
        /// <param name="storageName">The name of the storage account.</param>
        /// <param name="description">The Asset description.</param>
        /// <param name="containerDeletionPolicy">Deletion policy for the underlying storage container. This determines the behavior when an asset record is deleted. A deletion policy of 'Delete' will result in the associated storage container and all its contents being removed from storage. A deletion policy of 'Retain' will leave the content in-place in your storage account.</param>
        /// <param name="alternateId">An alternate ID of the asset.</param>
        /// <returns></returns>
        AssetSchema CreateOrUpdate(string assetName, string containerName, string storageName, string? description = null, AssetContainerDeletionPolicyType containerDeletionPolicy = AssetContainerDeletionPolicyType.Retain, string? alternateId = null);

        /// <summary>
        /// <para>Create or Update Asset.</para>
        /// <para>When creating an asset, that asset needs to have a unique container & storage account within the project.
        /// When updating an asset, only the description column can be updated.</para>
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <param name="containerName">The name of the asset blob container.</param>
        /// <param name="storageName">The name of the storage account.</param>
        /// <param name="description">The Asset description.</param>
        /// <param name="containerDeletionPolicy">Deletion policy for the underlying storage container. This determines the behavior when an asset record is deleted. A deletion policy of 'Delete' will result in the associated storage container and all its contents being removed from storage. A deletion policy of 'Retain' will leave the content in-place in your storage account.</param>
        /// <param name="alternateId">An alternate ID of the asset.</param>
        /// <returns></returns>
        Task<AssetSchema> CreateOrUpdateAsync(string assetName, string containerName, string storageName, string? description = null, AssetContainerDeletionPolicyType containerDeletionPolicy = AssetContainerDeletionPolicyType.Retain, string? alternateId = null);

        /// <summary>
        /// List Streaming Locators for Asset. This API call is a convenience method to retrieve
        /// any and all playback configuations, or StreamingLocators, that are assocaited with this asset.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        List<AssetStreamingLocator> ListStreamingLocators(string assetName);

        /// <summary>
        /// List Streaming Locators for Asset. This API call is a convenience method to retrieve
        /// any and all playback configuations, or StreamingLocators, that are assocaited with this asset.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        Task<List<AssetStreamingLocator>> ListStreamingLocatorsAsync(string assetName);

        /// <summary>
        /// <para>The Azure Storage Data Retrieval API is designed to provide restricted information about data stored in Azure Storage containers.
        /// This API allows users to interact with data in a secure and efficient manner, without directly altering the stored data.
        /// Here are key features and purposes of this API:</para>
        ///
        /// <para>List Container Contents: It enumerates the contents of an Azure Storage container.This operation returns both files and folders
        /// within the specified container, enabling users to navigate through the hierarchical data structure.</para>
        ///
        /// <para>View Track Listings: This API will generate available track listings for any media files contained within the specified container. Track listings
        /// are inclusive of video, audio, and text tracks.If available, the API will also return the track's language and bitrate.</para>
        ///
        /// <para>Security: This API is scoped only to the configured storage account and is only accessible to users who have access to the subscription in
        /// which the asset resides. It is a readonly API that does not allow for any mutation or creation operations, and which cannot be used to retrieve
        /// any data from the storage account.</para>
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        AssetStorageResponseSchema ListTracksAndDirListing(string assetName);

        /// <summary>
        /// <para>The Azure Storage Data Retrieval API is designed to provide restricted information about data stored in Azure Storage containers.
        /// This API allows users to interact with data in a secure and efficient manner, without directly altering the stored data.
        /// Here are key features and purposes of this API:</para>
        ///
        /// <para>List Container Contents: It enumerates the contents of an Azure Storage container.This operation returns both files and folders
        /// within the specified container, enabling users to navigate through the hierarchical data structure.</para>
        ///
        /// <para>View Track Listings: This API will generate available track listings for any media files contained within the specified container. Track listings
        /// are inclusive of video, audio, and text tracks.If available, the API will also return the track's language and bitrate.</para>
        ///
        /// <para>Security: This API is scoped only to the configured storage account and is only accessible to users who have access to the subscription in
        /// which the asset resides. It is a readonly API that does not allow for any mutation or creation operations, and which cannot be used to retrieve
        /// any data from the storage account.</para>
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <returns></returns>
        Task<AssetStorageResponseSchema> ListTracksAndDirListingAsync(string assetName);
    }
}
