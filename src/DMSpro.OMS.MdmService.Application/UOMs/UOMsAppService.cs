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
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.UOMs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.UOMs.Default)]
    public partial class UOMsAppService 
    {
        public virtual async Task<PagedResultDto<UOMDto>> GetListAsync(GetUOMsInput input)
        {
            var totalCount = await _uOMRepository.GetCountAsync(input.FilterText, input.Code, input.Name);
            var items = await _uOMRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UOMDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOM>, List<UOMDto>>(items)
            };
        }

        public virtual async Task<UOMDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<UOM, UOMDto>(await _uOMRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.UOMs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            if (await _uOMGroupDetailRepository.AnyAsync(x => x.AltUOMId == id))
            {
                throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
            }
            
            await _uOMRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.UOMs.Create)]
        public virtual async Task<UOMDto> CreateAsync(UOMCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);

            var uOM = await _uOMManager.CreateAsync(
            input.Code, input.Name
            );

            return ObjectMapper.Map<UOM, UOMDto>(uOM);
        }

        [Authorize(MdmServicePermissions.UOMs.Edit)]
        public virtual async Task<UOMDto> UpdateAsync(Guid id, UOMUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);

            var uOM = await _uOMManager.UpdateAsync(
            id,
            input.Code, input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<UOM, UOMDto>(uOM);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _uOMRepository.GetListAsync(input.FilterText, input.Code, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UOM>, List<UOMExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "UOMs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UOMExcelDownloadTokenCacheItem { Token = token },
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