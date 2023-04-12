using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupsAppService
    {
        public virtual async Task<ItemGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(await _itemGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task ReleaseAsync(Guid id)
        {
            var itemGroup = await _itemGroupRepository.GetAsync(id);
            if (itemGroup.Status != GroupStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupsAppService:551"], code: "1");
            }
            if (itemGroup.Type == GroupType.LIST &&
                await _itemGroupListRepository.AnyAsync(x => x.ItemGroupId == id))
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupsAppService:552"], code: "1");
            }
            else if (itemGroup.Type == GroupType.ATTRIBUTE &&
                await _itemGroupAttributeRepository.AnyAsync(x => x.ItemGroupId == id))
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupsAppService:553"], code: "1");
            }
            else if (itemGroup.Type != GroupType.ATTRIBUTE &&
                itemGroup.Type != GroupType.LIST)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupsAppService:554"], code: "1");
            }
            itemGroup.Status = GroupStatus.RELEASED;
            await _itemGroupRepository.UpdateAsync(itemGroup);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var itemGroup = await _itemGroupManager.CreateAsync(
                input.Code, input.Name, input.Description,
                input.Type, input.Selectable);

            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
        {
            var itemGroup = await _itemGroupRepository.GetAsync(id);
            if (itemGroup.Status != GroupStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:ItemGroupsAppService:550"], code: "1");
            }
            await _itemGroupManager.UpdateAsync(id,
                input.Name, input.Description, 
                input.Type, input.Selectable,
                input.ConcurrencyStamp);
            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }
    }
}