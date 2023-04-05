using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupListsAppService
    { 
        public virtual async Task<CustomerGroupListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(await _customerGroupListRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Customer>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
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

            var customerGroupList = await _customerGroupListManager.CreateAsync(
            input.CustomerId, input.CustomerGroupId, input.Description, input.Active
            );

            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(customerGroupList);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            var customerGroupList = await _customerGroupListManager.UpdateAsync(
            id,
            input.CustomerId, input.CustomerGroupId, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupList, CustomerGroupListDto>(customerGroupList);
        }
    }
}