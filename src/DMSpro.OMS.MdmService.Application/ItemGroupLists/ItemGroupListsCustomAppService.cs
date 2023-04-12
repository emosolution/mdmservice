using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{

    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupListsAppService
    {
        public virtual async Task<ItemGroupListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(await _itemGroupListRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var itemGroupList = await _itemGroupListRepository.GetAsync(id);
            await CheckItemGroup(itemGroupList.ItemGroupId);
            await _itemGroupListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input)
        {
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            await CheckItemGroup(input.ItemGroupId);
            await CheckItem(input.ItemId);
            var itemGroupList = await _itemGroupListManager.CreateAsync(
                input.ItemGroupId, input.ItemId, null, null, null);

            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(itemGroupList);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            var itemGroupList = await _itemGroupListRepository.GetAsync(id);
            await CheckItemGroup(itemGroupList.ItemGroupId);
            await CheckItem(input.ItemId);
            await _itemGroupListManager.UpdateAsync(
                id,
                input.ItemId, null, null, null,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<ItemGroupList, ItemGroupListDto>(itemGroupList);
        }

        private async Task CheckItemGroup(Guid itemGroupId)
        {
            var itemGroup = await _itemGroupRepository.GetAsync(itemGroupId);
            if (itemGroup.Status != GroupStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupListsAppService:550"], code: "0");
            }
            if (itemGroup.Type != GroupType.LIST)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupListsAppService:551"], code: "1");
            }
        }

        private async Task CheckItem(Guid itemId)
        {
            var item = await _itemRepository.GetAsync(itemId);
            if (!item.Active)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupListsAppService:552"], code: "1");
            }
        }
    }
}