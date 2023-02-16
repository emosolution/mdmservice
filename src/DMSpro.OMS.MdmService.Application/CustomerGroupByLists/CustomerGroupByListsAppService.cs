using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{

    [Authorize(MdmServicePermissions.CustomerGroupByLists.Default)]
    public partial class CustomerGroupByListsAppService 
    {
        public virtual async Task<PagedResultDto<CustomerGroupByListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByListsInput input)
        {
            var totalCount = await _customerGroupByListRepository.GetCountAsync(input.FilterText, input.Active, input.CustomerGroupId, input.CustomerId);
            var items = await _customerGroupByListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Active, input.CustomerGroupId, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupByListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroupByListWithNavigationProperties>, List<CustomerGroupByListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerGroupByListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByListWithNavigationProperties, CustomerGroupByListWithNavigationPropertiesDto>
                (await _customerGroupByListRepository.GetWithNavigationPropertiesAsync(id));
        }


        public virtual async Task<CustomerGroupByListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByList, CustomerGroupByListDto>(await _customerGroupByListRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroup>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(MdmServicePermissions.CustomerGroupByLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupByListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByLists.Create)]
        public virtual async Task<CustomerGroupByListDto> CreateAsync(CustomerGroupByListCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerGroupByList = await _customerGroupByListManager.CreateAsync(
            input.CustomerGroupId, input.CustomerId, input.Active
            );

            return ObjectMapper.Map<CustomerGroupByList, CustomerGroupByListDto>(customerGroupByList);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByLists.Edit)]
        public virtual async Task<CustomerGroupByListDto> UpdateAsync(Guid id, CustomerGroupByListUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerGroupByList = await _customerGroupByListManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.CustomerId, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupByList, CustomerGroupByListDto>(customerGroupByList);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByListExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerGroupByListRepository.GetListAsync(input.FilterText, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerGroupByList>, List<CustomerGroupByListExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroupByLists.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupByListExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}