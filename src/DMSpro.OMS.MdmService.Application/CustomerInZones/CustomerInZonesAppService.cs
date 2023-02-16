using DMSpro.OMS.MdmService.SalesOrgHierarchies;
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

namespace DMSpro.OMS.MdmService.CustomerInZones
{

    [Authorize(MdmServicePermissions.CustomerInZones.Default)]
    public partial class CustomerInZonesAppService 
    {
        public virtual async Task<PagedResultDto<CustomerInZoneWithNavigationPropertiesDto>> GetListAsync(GetCustomerInZonesInput input)
        {
            var totalCount = await _customerInZoneRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.CustomerId);
            var items = await _customerInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerInZoneWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerInZoneWithNavigationProperties>, List<CustomerInZoneWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerInZoneWithNavigationProperties, CustomerInZoneWithNavigationPropertiesDto>
                (await _customerInZoneRepository.GetWithNavigationPropertiesAsync(id));
        }
        
        public virtual async Task<CustomerInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(await _customerInZoneRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            var query = (await _salesOrgHierarchyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SalesOrgHierarchy>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHierarchy>, List<LookupDto<Guid>>>(lookupData)
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

        [Authorize(MdmServicePermissions.CustomerInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerInZones.Create)]
        public virtual async Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }else{
                var salesOrgHierarchy = await _salesOrgHierarchyRepository.GetAsync(input.SalesOrgHierarchyId);
                if(salesOrgHierarchy == null){
                    throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
                }
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }else{
                var customer = await _customerRepository.GetAsync(input.CustomerId);
                if(customer == null){
                    throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
                }
            }


            var customerInZone = await _customerInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.CustomerId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(customerInZone);
        }

        [Authorize(MdmServicePermissions.CustomerInZones.Edit)]
        public virtual async Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerInZone = await _customerInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.CustomerId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(customerInZone);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerInZoneExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerInZoneRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerInZone>, List<CustomerInZoneExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerInZones.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerInZoneExcelDownloadTokenCacheItem { Token = token },
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