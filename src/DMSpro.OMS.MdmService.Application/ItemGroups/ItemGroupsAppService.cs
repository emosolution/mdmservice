using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ItemGroups.Default)]
    public partial class ItemGroupsAppService
    {
        public virtual async Task<ItemGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(await _itemGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Create)]
        public virtual async Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var itemGroup = await _itemGroupManager.CreateAsync(
                input.Code, input.Name, input.Description, 
                input.Type, input.Status, input.Selectable);

            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }

        [Authorize(MdmServicePermissions.ItemGroups.Edit)]
        public virtual async Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var itemGroup = await _itemGroupManager.UpdateAsync(
                id,
                input.Code, input.Name, input.Description, 
                input.Type, input.Status, input.Selectable,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<ItemGroup, ItemGroupDto>(itemGroup);
        }
    }
}