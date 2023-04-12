using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroups
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupsAppService
    { 
        public virtual async Task<CustomerGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(await _customerGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupDto> ReleaseAsync(Guid id)
        {
            var customerGroup = await _customerGroupRepository.GetAsync(id);
            if (customerGroup.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:551"], code: "1");
            }
            if (customerGroup.GroupBy == Type.LIST &&
                !(await _customerGroupListRepository.AnyAsync(x => x.CustomerGroupId== id)))
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:552"], code: "1");
            }
            else if (customerGroup.GroupBy == Type.ATTRIBUTE &&
                !(await _customerGroupAttributeRepository.AnyAsync(x => x.CustomerGroupId == id)))
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:553"], code: "1");
            }
            else if (customerGroup.GroupBy == Type.GEO &&
                !(await _customerGroupGeoRepository.AnyAsync(x => x.CustomerGroupId == id)))
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:554"], code: "1");
            }
            else if (customerGroup.GroupBy != Type.ATTRIBUTE &&
                customerGroup.GroupBy != Type.LIST &&
                customerGroup.GroupBy != Type.GEO)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:555"], code: "1");
            }
            customerGroup.Status = Status.RELEASED;
            await _customerGroupRepository.UpdateAsync(customerGroup);
            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(customerGroup);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var customerGroup = await _customerGroupManager.CreateAsync(
                input.Code, input.Name, input.Selectable, input.GroupBy, input.Description);

            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(customerGroup);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input)
        {
            var customerGroup = await _customerGroupRepository.GetAsync(id);
            if (customerGroup.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupsAppService:550"], code: "1");
            }
            await _customerGroupManager.UpdateAsync(
                id,
                input.Name, input.Selectable, input.GroupBy, input.Description, 
                input.ConcurrencyStamp);

            return ObjectMapper.Map<CustomerGroup, CustomerGroupDto>(customerGroup);
        }
    }
}