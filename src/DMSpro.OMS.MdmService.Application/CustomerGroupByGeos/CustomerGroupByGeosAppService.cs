using DMSpro.OMS.MdmService.GeoMasters;
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

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{

    [Authorize(MdmServicePermissions.CustomerGroupByGeos.Default)]
    public partial class CustomerGroupByGeosAppService
    {
        public virtual async Task<PagedResultDto<CustomerGroupByGeoWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByGeosInput input)
        {
            var totalCount = await _customerGroupByGeoRepository.GetCountAsync(input.FilterText, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.CustomerGroupId, input.GeoMasterId);
            var items = await _customerGroupByGeoRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.CustomerGroupId, input.GeoMasterId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupByGeoWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroupByGeoWithNavigationProperties>, List<CustomerGroupByGeoWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerGroupByGeoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByGeoWithNavigationProperties, CustomerGroupByGeoWithNavigationPropertiesDto>
                (await _customerGroupByGeoRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerGroupByGeoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByGeo, CustomerGroupByGeoDto>(await _customerGroupByGeoRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _geoMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<GeoMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<GeoMaster>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerGroupByGeos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupByGeoRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByGeos.Create)]
        public virtual async Task<CustomerGroupByGeoDto> CreateAsync(CustomerGroupByGeoCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.GeoMasterId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            var customerGroupByGeo = await _customerGroupByGeoManager.CreateAsync(
            input.CustomerGroupId, input.GeoMasterId, input.Active, input.EffectiveDate
            );

            return ObjectMapper.Map<CustomerGroupByGeo, CustomerGroupByGeoDto>(customerGroupByGeo);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByGeos.Edit)]
        public virtual async Task<CustomerGroupByGeoDto> UpdateAsync(Guid id, CustomerGroupByGeoUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.GeoMasterId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            var customerGroupByGeo = await _customerGroupByGeoManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.GeoMasterId, input.Active, input.EffectiveDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupByGeo, CustomerGroupByGeoDto>(customerGroupByGeo);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByGeoExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerGroupByGeoRepository.GetListAsync(input.FilterText, input.Active, input.EffectiveDateMin, input.EffectiveDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerGroupByGeo>, List<CustomerGroupByGeoExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroupByGeos.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupByGeoExcelDownloadTokenCacheItem { Token = token },
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