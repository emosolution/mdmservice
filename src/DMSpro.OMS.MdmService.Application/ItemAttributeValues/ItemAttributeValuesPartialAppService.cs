using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    [Authorize(MdmServicePermissions.ItemAttributeValues.Default)]
    public partial class ItemAttributeValuesAppService : PartialAppService<ItemAttributeValue, ItemAttributeValueWithDetailsDto, IItemAttributeValueRepository>,
        IItemAttributeValuesAppService
    {
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache<ItemAttributeValueExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemAttributeValueManager _itemAttributeValueManager;

        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeValuesAppService(ICurrentTenant currentTenant,
            IItemAttributeValueRepository repository,
            IItemRepository itemRepository,
            ItemAttributeValueManager itemAttributeValueManager,
            IConfiguration settingProvider,
            IItemAttributeRepository itemAttributeRepository,
            IDistributedCache<ItemAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemAttributeValues.Default)
        {
            _itemAttributeValueRepository = repository;
            _itemRepository = itemRepository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemAttributeValueManager = itemAttributeValueManager;

            _itemAttributeRepository = itemAttributeRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeRepository", _itemAttributeRepository));
        }
    }
}