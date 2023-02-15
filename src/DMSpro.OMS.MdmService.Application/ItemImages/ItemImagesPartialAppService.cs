using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.ItemImages
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemImagesAppService : PartialAppService<ItemImage, ItemImageDto, IItemImageRepository>,
        IItemImagesAppService
    {
        private readonly IItemImageRepository _itemImageRepository;
        private readonly IDistributedCache<ItemImageExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemImageManager _itemImageManager;

        private readonly IItemRepository _itemRepository;

        public ItemImagesAppService(ICurrentTenant currentTenant,
            IItemImageRepository repository,
            ItemImageManager itemImageManager,
            IConfiguration settingProvider,
            IItemRepository itemRepository,
            IDistributedCache<ItemImageExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemImageRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemImageManager = itemImageManager;

            _itemRepository = itemRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemImageRepository", _itemImageRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
        }
    }
}