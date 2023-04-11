using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListManager : DomainService
    {
        private readonly IItemGroupListRepository _itemGroupListRepository;

        public ItemGroupListManager(IItemGroupListRepository itemGroupListRepository)
        {
            _itemGroupListRepository = itemGroupListRepository;
        }

        public async Task<ItemGroupList> CreateAsync(
        Guid itemGroupId, Guid itemId, Guid? uomId, int? rate = null, decimal? price = null)
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(itemId, nameof(itemId));

            var itemGroupList = new ItemGroupList(
             GuidGenerator.Create(),
             itemGroupId, itemId, uomId, rate, price
             );

            return await _itemGroupListRepository.InsertAsync(itemGroupList);
        }

        public async Task<ItemGroupList> UpdateAsync(
            Guid id,
            Guid itemId, Guid? uomId, int? rate = null, decimal? price = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemId, nameof(itemId));

            var itemGroupList = await _itemGroupListRepository.GetAsync(id);

            itemGroupList.ItemId = itemId;
            itemGroupList.UomId = uomId;
            itemGroupList.Rate = rate;
            itemGroupList.Price = price;

            itemGroupList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupListRepository.UpdateAsync(itemGroupList);
        }

    }
}