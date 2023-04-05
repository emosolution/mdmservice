using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemGroupAttributes;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    [Authorize(MdmServicePermissions.ItemAttributes.Default)]
    public partial class ItemAttributeValuesAppService : PartialAppService<ItemAttributeValue, ItemAttributeValueWithDetailsDto, IItemAttributeValueRepository>,
        IItemAttributeValuesAppService
    {
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;

        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeValuesAppService(ICurrentTenant currentTenant,
            IItemAttributeValueRepository repository,
            IItemRepository itemRepository,
            IItemGroupAttributeRepository itemGroupAttributeRepository,
            IConfiguration settingProvider,
            IItemAttributeRepository itemAttributeRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.ItemAttributes.Default)
        {
            _itemAttributeValueRepository = repository;
            _itemRepository = itemRepository;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;

            _itemAttributeRepository = itemAttributeRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeValueRepository", _itemAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttributeRepository", _itemAttributeRepository));
        }
    }
}