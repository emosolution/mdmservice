using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributes;
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
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{

    [Authorize(MdmServicePermissions.CustomerAttributeValues.Default)]
    public class CustomerAttributeValuesAppService : ApplicationService, ICustomerAttributeValuesAppService
    {
        private readonly IDistributedCache<CustomerAttributeValueExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;
        private readonly CustomerAttributeValueManager _customerAttributeValueManager;
        private readonly IRepository<CustomerAttribute, Guid> _customerAttributeRepository;

        public CustomerAttributeValuesAppService(ICustomerAttributeValueRepository customerAttributeValueRepository, CustomerAttributeValueManager customerAttributeValueManager, IDistributedCache<CustomerAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CustomerAttribute, Guid> customerAttributeRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerAttributeValueRepository = customerAttributeValueRepository;
            _customerAttributeValueManager = customerAttributeValueManager; _customerAttributeRepository = customerAttributeRepository;
        }

        public virtual async Task<PagedResultDto<CustomerAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttributeValuesInput input)
        {
            var totalCount = await _customerAttributeValueRepository.GetCountAsync(input.FilterText, input.Code, input.AttrValName, input.CustomerAttributeId, input.ParentId);
            var items = await _customerAttributeValueRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.AttrValName, input.CustomerAttributeId, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAttributeValueWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttributeValueWithNavigationProperties>, List<CustomerAttributeValueWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttributeValueWithNavigationProperties, CustomerAttributeValueWithNavigationPropertiesDto>
                (await _customerAttributeValueRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(await _customerAttributeValueRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerAttributeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrName != null &&
                         x.AttrName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerAttribute>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttribute>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttributeValue>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerAttributeValues.Create)]
        public virtual async Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var customerAttributeValue = await _customerAttributeValueManager.CreateAsync(
            input.CustomerAttributeId, input.ParentId, input.Code, input.AttrValName
            );

            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(customerAttributeValue);
        }

        [Authorize(MdmServicePermissions.CustomerAttributeValues.Edit)]
        public virtual async Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var customerAttributeValue = await _customerAttributeValueManager.UpdateAsync(
            id,
            input.CustomerAttributeId, input.ParentId, input.Code, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAttributeValue, CustomerAttributeValueDto>(customerAttributeValue);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeValueExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerAttributeValues = await _customerAttributeValueRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.AttrValName);
            var items = customerAttributeValues.Select(item => new
            {
                Code = item.CustomerAttributeValue.Code,
                AttrValName = item.CustomerAttributeValue.AttrValName,

                CustomerAttributeAttrName = item.CustomerAttribute?.AttrName,
                CustomerAttributeValueAttrValName = item.CustomerAttributeValue?.AttrValName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAttributeValues.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAttributeValueExcelDownloadTokenCacheItem { Token = token },
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