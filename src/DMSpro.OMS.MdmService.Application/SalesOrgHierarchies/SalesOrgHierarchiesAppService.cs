using DMSpro.OMS.MdmService.SalesOrgHeaders;
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

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{

    [Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
    public partial class SalesOrgHierarchiesAppService
    {
       public virtual async Task<PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgHierarchiesInput input)
        {
            var totalCount = await _salesOrgHierarchyRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.LevelMin, input.LevelMax, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active, input.SalesOrgHeaderId, input.ParentId);
            var items = await _salesOrgHierarchyRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.LevelMin, input.LevelMax, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active, input.SalesOrgHeaderId, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHierarchyWithNavigationProperties>, List<SalesOrgHierarchyWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<SalesOrgHierarchyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHierarchyWithNavigationProperties, SalesOrgHierarchyWithNavigationPropertiesDto>
                (await _salesOrgHierarchyRepository.GetWithNavigationPropertiesAsync(id));
        }
       
        public virtual async Task<SalesOrgHierarchyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(await _salesOrgHierarchyRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHeaderLookupAsync(LookupRequestDto input)
        {
            var query = (await _salesOrgHeaderRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SalesOrgHeader>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHeader>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            var query = (await _salesOrgHierarchyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SalesOrgHierarchy>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHierarchy>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgHierarchyManager.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateAsync(SalesOrgHierarchyCreateDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }

            var salesOrgHierarchy = await _salesOrgHierarchyManager.CreateAsync(
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Edit)]
        public virtual async Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }

            var salesOrgHierarchy = await _salesOrgHierarchyManager.UpdateAsync(
            id,
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHierarchyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _salesOrgHierarchyRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.LevelMin, input.LevelMax, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SalesOrgHierarchy>, List<SalesOrgHierarchyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SalesOrgHierarchies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalesOrgHierarchyExcelDownloadTokenCacheItem { Token = token },
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