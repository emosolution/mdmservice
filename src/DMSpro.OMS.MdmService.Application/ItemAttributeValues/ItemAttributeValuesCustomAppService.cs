using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{

    [Authorize(MdmServicePermissions.ItemAttributeValues.Default)]
    public partial class ItemAttributeValuesAppService
    {
        public virtual async Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(await _itemAttributeValueRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateRootAsync(ItemAttributeValueCreateRootDto input)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<ItemAttributeValueDto> CreateHierarchyAsync(ItemAttributeValueCreateHierarchyDto input)
        {
            throw new System.NotImplementedException();
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Edit)]
        public virtual async Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input)
        {
            throw new System.NotImplementedException();
        }
    }
}