using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.SystemConfigs
{

    [Authorize(MdmServicePermissions.SystemConfig.Default)]
    public partial class SystemConfigsAppService
    {
        public virtual async Task<PagedResultDto<SystemConfigDto>> GetListAsync(GetSystemConfigsInput input)
        {
            var totalCount = await _systemConfigRepository.GetCountAsync(input.FilterText, input.Code, input.Description, input.Value, input.DefaultValue, input.EditableByTenant, input.ControlType, input.DataSource);
            var items = await _systemConfigRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.Value, input.DefaultValue, input.EditableByTenant, input.ControlType, input.DataSource, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SystemConfigDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemConfig>, List<SystemConfigDto>>(items)
            };
        }

        public virtual async Task<SystemConfigDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SystemConfig, SystemConfigDto>(await _systemConfigRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SystemConfig.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _systemConfigRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SystemConfig.Create)]
        public virtual async Task<SystemConfigDto> CreateAsync(SystemConfigCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);

            var systemConfig = await _systemConfigManager.CreateAsync(
            input.Code, input.Description, input.Value, input.DefaultValue, input.EditableByTenant, input.ControlType, input.DataSource
            );

            return ObjectMapper.Map<SystemConfig, SystemConfigDto>(systemConfig);
        }

        [Authorize(MdmServicePermissions.SystemConfig.Edit)]
        public virtual async Task<SystemConfigDto> UpdateAsync(Guid id, SystemConfigUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);

            var systemConfig = await _systemConfigManager.UpdateAsync(
            id,
            input.Code, input.Description, input.Value, input.DefaultValue, input.EditableByTenant, input.ControlType, input.DataSource, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SystemConfig, SystemConfigDto>(systemConfig);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemConfigExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _systemConfigRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.Value, input.DefaultValue, input.EditableByTenant, input.ControlType, input.DataSource);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SystemConfig>, List<SystemConfigExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SystemConfigs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SystemConfigExcelDownloadTokenCacheItem { Token = token },
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