using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemGroups;
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

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{

    [Authorize(MdmServicePermissions.ItemGroupInZones.Default)]
    public partial class ItemGroupInZonesAppService
    {
        public virtual async Task<PagedResultDto<ItemGroupInZoneWithNavigationPropertiesDto>> GetListAsync(GetItemGroupInZonesInput input)
        {
            var totalCount = await _itemGroupInZoneRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.Active, input.Description, input.SellingZoneId, input.ItemGroupId);
            var items = await _itemGroupInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.Active, input.Description, input.SellingZoneId, input.ItemGroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemGroupInZoneWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroupInZoneWithNavigationProperties>, List<ItemGroupInZoneWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemGroupInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupInZoneWithNavigationProperties, ItemGroupInZoneWithNavigationPropertiesDto>
                (await _itemGroupInZoneRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ItemGroupInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemGroupInZone, ItemGroupInZoneDto>(await _itemGroupInZoneRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.ItemGroupInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemGroupInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemGroupInZones.Create)]
        public virtual async Task<ItemGroupInZoneDto> CreateAsync(ItemGroupInZoneCreateDto input)
        {
            if (input.SellingZoneId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupInZone = await _itemGroupInZoneManager.CreateAsync(
            input.SellingZoneId, input.ItemGroupId, input.EffectiveDate, input.Active, input.Description, input.EndDate
            );

            return ObjectMapper.Map<ItemGroupInZone, ItemGroupInZoneDto>(itemGroupInZone);
        }

        [Authorize(MdmServicePermissions.ItemGroupInZones.Edit)]
        public virtual async Task<ItemGroupInZoneDto> UpdateAsync(Guid id, ItemGroupInZoneUpdateDto input)
        {
            if (input.SellingZoneId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }

            var itemGroupInZone = await _itemGroupInZoneManager.UpdateAsync(
            id,
            input.SellingZoneId, input.ItemGroupId, input.EffectiveDate, input.Active, input.Description, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemGroupInZone, ItemGroupInZoneDto>(itemGroupInZone);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupInZoneExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var itemGroupInZones = await _itemGroupInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.Active, input.Description);
            var items = itemGroupInZones.Select(item => new
            {
                EffectiveDate = item.ItemGroupInZone.EffectiveDate,
                EndDate = item.ItemGroupInZone.EndDate,
                Active = item.ItemGroupInZone.Active,
                Description = item.ItemGroupInZone.Description,

                SalesOrgHierarchyCode = item.SalesOrgHierarchy?.Code,
                ItemGroupCode = item.ItemGroup?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemGroupInZones.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemGroupInZoneExcelDownloadTokenCacheItem { Token = token },
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