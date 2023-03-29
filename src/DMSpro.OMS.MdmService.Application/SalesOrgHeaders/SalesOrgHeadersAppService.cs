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

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{

    [Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
    public partial class SalesOrgHeadersAppService
    {
        public virtual async Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input)
        {
            var totalCount = await _salesOrgHeaderRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Active);
            var items = await _salesOrgHeaderRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalesOrgHeaderDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHeader>, List<SalesOrgHeaderDto>>(items)
            };
        }

        public virtual async Task<SalesOrgHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(await _salesOrgHeaderRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgHeaderRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Create)]
        public virtual async Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var salesOrgHeader = await _salesOrgHeaderManager.CreateAsync(
            input.Code, input.Name, input.Active
            );

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Edit)]
        public virtual async Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var salesOrgHeader = await _salesOrgHeaderManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _salesOrgHeaderRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SalesOrgHeader>, List<SalesOrgHeaderExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SalesOrgHeaders.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalesOrgHeaderExcelDownloadTokenCacheItem { Token = token },
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