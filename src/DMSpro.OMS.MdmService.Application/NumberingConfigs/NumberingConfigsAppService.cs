using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigsAppService
    {
        public virtual async Task<PagedResultDto<NumberingConfigWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigsInput input)
        {
            var totalCount = await _numberingConfigRepository.GetCountAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax, input.Active, input.Description, input.SystemDataId);
            var items = await _numberingConfigRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax, input.Active, input.Description, input.SystemDataId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<NumberingConfigWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<NumberingConfigWithNavigationProperties>, List<NumberingConfigWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<NumberingConfigWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfigWithNavigationProperties, NumberingConfigWithNavigationPropertiesDto>
                (await _numberingConfigRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<NumberingConfigDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(await _numberingConfigRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _numberingConfigRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Create)]
        public virtual async Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input)
        {

            var numberingConfig = await _numberingConfigManager.CreateAsync(
            input.SystemDataId, input.StartNumber, input.Prefix, input.Suffix, input.Length, input.Active, input.Description
            );

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {

            var numberingConfig = await _numberingConfigManager.UpdateAsync(
            id,
            input.SystemDataId, input.StartNumber, input.Prefix, input.Suffix, input.Length, input.Active, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var numberingConfigs = await _numberingConfigRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax, input.Active, input.Description);
            var items = numberingConfigs.Select(item => new
            {
                StartNumber = item.NumberingConfig.StartNumber,
                Prefix = item.NumberingConfig.Prefix,
                Suffix = item.NumberingConfig.Suffix,
                Length = item.NumberingConfig.Length,
                Active = item.NumberingConfig.Active,
                Description = item.NumberingConfig.Description,

                SystemDataCode = item.SystemData?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "NumberingConfigs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new NumberingConfigExcelDownloadTokenCacheItem { Token = token },
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