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
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Streets;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Streets
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.Streets.Default)]
    public partial class StreetsAppService : ApplicationService, IStreetsAppService
    {
        private readonly IDistributedCache<StreetExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IStreetRepository _streetRepository;
        private readonly StreetManager _streetManager;

        public StreetsAppService(IStreetRepository streetRepository, StreetManager streetManager, IDistributedCache<StreetExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _streetRepository = streetRepository;
            _streetManager = streetManager;
        }

        public virtual async Task<PagedResultDto<StreetDto>> GetListAsync(GetStreetsInput input)
        {
            var totalCount = await _streetRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await _streetRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<StreetDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Street>, List<StreetDto>>(items)
            };
        }

        public virtual async Task<StreetDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Street, StreetDto>(await _streetRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Streets.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _streetRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Streets.Create)]
        public virtual async Task<StreetDto> CreateAsync(StreetCreateDto input)
        {

            var street = await _streetManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<Street, StreetDto>(street);
        }

        [Authorize(MdmServicePermissions.Streets.Edit)]
        public virtual async Task<StreetDto> UpdateAsync(Guid id, StreetUpdateDto input)
        {

            var street = await _streetManager.UpdateAsync(
            id,
            input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Street, StreetDto>(street);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(StreetExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _streetRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Street>, List<StreetExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Streets.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new StreetExcelDownloadTokenCacheItem { Token = token },
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