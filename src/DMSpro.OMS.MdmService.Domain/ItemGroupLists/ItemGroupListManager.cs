using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid itemGroupId, Guid itemId, Guid uOMId, int rate)
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNull(uOMId, nameof(uOMId));

            var itemGroupList = new ItemGroupList(
             GuidGenerator.Create(),
             itemGroupId, itemId, uOMId, rate
             );

            return await _itemGroupListRepository.InsertAsync(itemGroupList);
        }

        public async Task<ItemGroupList> UpdateAsync(
            Guid id,
            Guid itemGroupId, Guid itemId, Guid uOMId, int rate, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(itemId, nameof(itemId));
            Check.NotNull(uOMId, nameof(uOMId));

            var queryable = await _itemGroupListRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemGroupList = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemGroupList.ItemGroupId = itemGroupId;
            itemGroupList.ItemId = itemId;
            itemGroupList.UOMId = uOMId;
            itemGroupList.Rate = rate;

            itemGroupList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupListRepository.UpdateAsync(itemGroupList);
        }

    }
}