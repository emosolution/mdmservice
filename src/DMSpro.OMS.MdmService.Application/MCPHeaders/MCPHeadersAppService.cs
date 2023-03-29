using DMSpro.OMS.MdmService.ItemGroups;
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

namespace DMSpro.OMS.MdmService.MCPHeaders
{

    [Authorize(MdmServicePermissions.MCPs.Default)]
    public partial class MCPHeadersAppService
    {
        public virtual async Task<PagedResultDto<MCPHeaderWithNavigationPropertiesDto>> GetListAsync(GetMCPHeadersInput input)
        {
            var totalCount = await _mCPHeaderRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.CompanyId, input.ItemGroupId);
            var items = await _mCPHeaderRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.CompanyId, input.ItemGroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MCPHeaderWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MCPHeaderWithNavigationProperties>, List<MCPHeaderWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<MCPHeaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<MCPHeaderWithNavigationProperties, MCPHeaderWithNavigationPropertiesDto>
                (await _mCPHeaderRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<MCPHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(await _mCPHeaderRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.MCPs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mCPHeaderRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.MCPs.Create)]
        public virtual async Task<MCPHeaderDto> CreateAsync(MCPHeaderCreateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            await CheckCodeUniqueness(input.Code);
            var mCPHeader = await _mCPHeaderManager.CreateAsync(
            input.RouteId, input.CompanyId, input.ItemGroupId, input.Code, input.Name, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(mCPHeader);
        }

        [Authorize(MdmServicePermissions.MCPs.Edit)]
        public virtual async Task<MCPHeaderDto> UpdateAsync(Guid id, MCPHeaderUpdateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            await CheckCodeUniqueness(input.Code, id);
            var mCPHeader = await _mCPHeaderManager.UpdateAsync(
            id,
            input.RouteId, input.CompanyId, input.ItemGroupId, input.Code, input.Name, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(mCPHeader);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPHeaderExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var mCPHeaders = await _mCPHeaderRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);
            var items = mCPHeaders.Select(item => new
            {
                Code = item.MCPHeader.Code,
                Name = item.MCPHeader.Name,
                EffectiveDate = item.MCPHeader.EffectiveDate,
                EndDate = item.MCPHeader.EndDate,

                SalesOrgHierarchyCode = item.SalesOrgHierarchy?.Code,
                CompanyCode = item.Company?.Code,
                ItemGroupCode = item.ItemGroup?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MCPHeaders.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MCPHeaderExcelDownloadTokenCacheItem { Token = token },
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