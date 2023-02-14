using DMSpro.OMS.MdmService.Companies;
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

namespace DMSpro.OMS.MdmService.CompanyInZones
{

    [Authorize(MdmServicePermissions.CompanyInZones.Default)]
    public partial class CompanyInZonesAppService 
    {
        public virtual async Task<PagedResultDto<CompanyInZoneWithNavigationPropertiesDto>> GetListAsync(GetCompanyInZonesInput input)
        {
            var totalCount = await _companyInZoneRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsBase, input.SalesOrgHierarchyId, input.CompanyId);
            var items = await _companyInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsBase, input.SalesOrgHierarchyId, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyInZoneWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyInZoneWithNavigationProperties>, List<CompanyInZoneWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CompanyInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyInZoneWithNavigationProperties, CompanyInZoneWithNavigationPropertiesDto>
                (await _companyInZoneRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CompanyInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(await _companyInZoneRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Create)]
        public virtual async Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyInZone = await _companyInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.CompanyId, input.EffectiveDate, input.IsBase, input.EndDate
            );

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(companyInZone);
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Edit)]
        public virtual async Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyInZone = await _companyInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.CompanyId, input.EffectiveDate, input.IsBase, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(companyInZone);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyInZoneExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _companyInZoneRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsBase);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CompanyInZone>, List<CompanyInZoneExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CompanyInZones.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CompanyInZoneExcelDownloadTokenCacheItem { Token = token },
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