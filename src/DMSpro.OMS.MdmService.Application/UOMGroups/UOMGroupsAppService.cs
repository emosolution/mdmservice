using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.UOMGroups.Default)]
    public partial class UOMGroupsAppService 
    {
        public virtual async Task<PagedResultDto<UOMGroupDto>> GetListAsync(GetUOMGroupsInput input)
        {
            var totalCount = await _uOMGroupRepository.GetCountAsync(input.FilterText, input.Code, input.Name);
            var items = await _uOMGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UOMGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroup>, List<UOMGroupDto>>(items)
            };
        }

        public virtual async Task<UOMGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<UOMGroup, UOMGroupDto>(await _uOMGroupRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.UOMGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _uOMGroupDetailRepository.DeleteAsync(x => x.UOMGroupId == id);
            await _uOMGroupRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.UOMGroups.Create)]
        public virtual async Task<UOMGroupDto> CreateAsync(UOMGroupCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);

            var uOMGroup = await _uOMGroupManager.CreateAsync(
            input.Code, input.Name
            );

            return ObjectMapper.Map<UOMGroup, UOMGroupDto>(uOMGroup);
        }

        [Authorize(MdmServicePermissions.UOMGroups.Edit)]
        public virtual async Task<UOMGroupDto> UpdateAsync(Guid id, UOMGroupUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);

            var uOMGroup = await _uOMGroupManager.UpdateAsync(
            id,
            input.Code, input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<UOMGroup, UOMGroupDto>(uOMGroup);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _uOMGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UOMGroup>, List<UOMGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "UOMGroups.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UOMGroupExcelDownloadTokenCacheItem { Token = token },
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