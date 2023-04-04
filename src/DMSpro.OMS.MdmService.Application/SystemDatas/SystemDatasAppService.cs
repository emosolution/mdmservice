using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SystemDatas
{

    [Authorize(MdmServicePermissions.SystemData.Default)]
    public partial class SystemDatasAppService
    {
        public virtual async Task<PagedResultDto<SystemDataDto>> GetListAsync(GetSystemDatasInput input)
        {
            var totalCount = await _systemDataRepository.GetCountAsync(input.FilterText, input.Code, input.ValueCode, input.ValueName);
            var items = await _systemDataRepository.GetListAsync(input.FilterText, input.Code, input.ValueCode, input.ValueName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SystemDataDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<SystemDataDto>>(items)
            };
        }

        public virtual async Task<SystemDataDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SystemData, SystemDataDto>(await _systemDataRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SystemData.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _systemDataRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SystemData.Create)]
        public virtual async Task<SystemDataDto> CreateAsync(SystemDataCreateDto input)
        {
            await CheckUniqueValueCode(input.Code, input.ValueCode);

            var systemData = await _systemDataManager.CreateAsync(
            input.Code, input.ValueCode, input.ValueName
            );

            return ObjectMapper.Map<SystemData, SystemDataDto>(systemData);
        }

        private async Task CheckUniqueValueCode(string code, string valueCode, Guid? id = null)
        {
            if (await _systemDataRepository.AnyAsync(x => 
                    (x.Code == code && x.ValueCode == valueCode && id == null) ||
                    (x.Code == code && x.ValueCode == valueCode && x.Id != id && id != null)
               ))
            {
                throw new UserFriendlyException(message: L["Error:PartialCheckSystemDataCodeUniquenessAppService:554"], code: "1");
            }
        }

        [Authorize(MdmServicePermissions.SystemData.Edit)]
        public virtual async Task<SystemDataDto> UpdateAsync(Guid id, SystemDataUpdateDto input)
        {
            
            await CheckUniqueValueCode(input.Code, input.ValueCode,id);

            var systemData = await _systemDataManager.UpdateAsync(
            id,
            input.Code, input.ValueCode, input.ValueName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SystemData, SystemDataDto>(systemData);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemDataExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _systemDataRepository.GetListAsync(input.FilterText, input.Code, input.ValueCode, input.ValueName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SystemData>, List<SystemDataExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SystemDatas.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SystemDataExcelDownloadTokenCacheItem { Token = token },
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