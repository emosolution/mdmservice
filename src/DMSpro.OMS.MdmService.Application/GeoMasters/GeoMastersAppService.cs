using DMSpro.OMS.MdmService.Shared;
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
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.GeoMasters.Default)]
    public partial class GeoMastersAppService
    { 
        public virtual async Task<PagedResultDto<GeoMasterWithNavigationPropertiesDto>> GetListAsync(GetGeoMastersInput input)
        {
            var totalCount = await _geoMasterRepository.GetCountAsync(input.FilterText, input.Code, input.ERPCode, input.Name, input.LevelMin, input.LevelMax, input.ParentId);
            var items = await _geoMasterRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.ERPCode, input.Name, input.LevelMin, input.LevelMax, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<GeoMasterWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<GeoMasterWithNavigationProperties>, List<GeoMasterWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<GeoMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<GeoMasterWithNavigationProperties, GeoMasterWithNavigationPropertiesDto>
                (await _geoMasterRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<GeoMasterDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<GeoMaster, GeoMasterDto>(await _geoMasterRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.GeoMasters.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _geoMasterRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.GeoMasters.Create)]
        public virtual async Task<GeoMasterDto> CreateAsync(GeoMasterCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var geoMaster = await _geoMasterManager.CreateAsync(
            input.ParentId, input.Code, input.ERPCode, input.Name, input.Level
            );

            return ObjectMapper.Map<GeoMaster, GeoMasterDto>(geoMaster);
        }

        [Authorize(MdmServicePermissions.GeoMasters.Edit)]
        public virtual async Task<GeoMasterDto> UpdateAsync(Guid id, GeoMasterUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var geoMaster = await _geoMasterManager.UpdateAsync(
            id,
            input.ParentId, input.Code, input.ERPCode, input.Name, input.Level, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<GeoMaster, GeoMasterDto>(geoMaster);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(GeoMasterExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var geoMasters = await _geoMasterRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.ERPCode, input.Name, input.LevelMin, input.LevelMax);
            var items = geoMasters.Select(item => new
            {
                Code = item.GeoMaster.Code,
                ERPCode = item.GeoMaster.ERPCode,
                Name = item.GeoMaster.Name,
                Level = item.GeoMaster.Level,

                GeoMasterCode = item.GeoMaster?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "GeoMasters.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new GeoMasterExcelDownloadTokenCacheItem { Token = token },
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