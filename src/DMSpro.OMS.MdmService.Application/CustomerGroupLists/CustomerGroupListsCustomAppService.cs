using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupListsAppService
    {
        public virtual async Task<CustomerGroupListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(await _customerGroupListRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var customerGroupList = await _customerGroupListRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupList.CustomerGroupId);
            await _customerGroupListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupListDto> CreateAsync(CustomerGroupListCreateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            await CheckCustomerGroup(input.CustomerGroupId);
            await CheckCustomer(input.CustomerId);
            var customerGroupList = await _customerGroupListManager.CreateAsync(
                input.CustomerId, input.CustomerGroupId, null, true);
            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(customerGroupList);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            var customerGroupList = await _customerGroupListRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupList.CustomerGroupId);
            await CheckCustomer(input.CustomerId);
            await _customerGroupListManager.UpdateAsync(
                id,
                input.CustomerId, input.ConcurrencyStamp);

            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(customerGroupList);
        }

        private async Task CheckCustomerGroup(Guid customerId)
        {
            var customerGroup = await _customerGroupRepository.GetAsync(customerId);
            if (customerGroup.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupListsAppService:550"], code: "0");
            }
            if (customerGroup.GroupBy != CustomerGroups.Type.LIST)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupListsAppService:551"], code: "1");
            }
        }

        private async Task CheckCustomer(Guid customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            if (!customer.Active)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupListsAppService:552"], code: "1");
            }
        }
    }
}