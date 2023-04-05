using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public class CustomerGroupListsAppService : ApplicationService, ICustomerGroupListsAppService
    {
        private readonly IDistributedCache<CustomerGroupListExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerGroupListRepository _customerGroupListRepository;
        private readonly CustomerGroupListManager _customerGroupListManager;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<CustomerGroup, Guid> _customerGroupRepository;

        public CustomerGroupListsAppService(ICustomerGroupListRepository customerGroupListRepository, CustomerGroupListManager customerGroupListManager, IDistributedCache<CustomerGroupListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Customer, Guid> customerRepository, IRepository<CustomerGroup, Guid> customerGroupRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerGroupListRepository = customerGroupListRepository;
            _customerGroupListManager = customerGroupListManager; _customerRepository = customerRepository;
            _customerGroupRepository = customerGroupRepository;
        }

        public virtual async Task<PagedResultDto<CustomerGroupListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupListsInput input)
        {
            var totalCount = await _customerGroupListRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.CustomerId, input.CustomerGroupId);
            var items = await _customerGroupListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.CustomerId, input.CustomerGroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroupListWithNavigationProperties>, List<CustomerGroupListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupListWithNavigationProperties, CustomerGroupListWithNavigationPropertiesDto>
                (await _customerGroupListRepository.GetWithNavigationPropertiesAsync(id));
        }

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

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupListExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerGroupLists = await _customerGroupListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active);
            var items = customerGroupLists.Select(item => new
            {
                Description = item.CustomerGroupList.Description,
                Active = item.CustomerGroupList.Active,

                CustomerCode = item.Customer?.Code,
                CustomerGroupCode = item.CustomerGroup?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroupLists.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupListExcelDownloadTokenCacheItem { Token = token },
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