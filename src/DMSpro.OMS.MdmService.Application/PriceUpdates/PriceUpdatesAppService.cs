using DMSpro.OMS.MdmService.PriceLists;
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
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.PriceUpdates
{

    [Authorize(MdmServicePermissions.PriceUpdates.Default)]
    public partial class PriceUpdatesAppService : ApplicationService, IPriceUpdatesAppService
    {
        private readonly IDistributedCache<PriceUpdateExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPriceUpdateRepository _priceUpdateRepository;
        private readonly PriceUpdateManager _priceUpdateManager;
        private readonly IRepository<PriceList, Guid> _priceListRepository;

        public PriceUpdatesAppService(IPriceUpdateRepository priceUpdateRepository, PriceUpdateManager priceUpdateManager, IDistributedCache<PriceUpdateExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<PriceList, Guid> priceListRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _priceUpdateRepository = priceUpdateRepository;
            _priceUpdateManager = priceUpdateManager; _priceListRepository = priceListRepository;
        }

        public virtual async Task<PagedResultDto<PriceUpdateWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdatesInput input)
        {
            var totalCount = await _priceUpdateRepository.GetCountAsync(input.FilterText, input.Code, input.Description, input.EffectiveDateMin, input.EffectiveDateMax, input.Status, input.UpdateStatusDateMin, input.UpdateStatusDateMax, input.PriceListId);
            var items = await _priceUpdateRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Description, input.EffectiveDateMin, input.EffectiveDateMax, input.Status, input.UpdateStatusDateMin, input.UpdateStatusDateMax, input.PriceListId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PriceUpdateWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceUpdateWithNavigationProperties>, List<PriceUpdateWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PriceUpdateWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdateWithNavigationProperties, PriceUpdateWithNavigationPropertiesDto>
                (await _priceUpdateRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PriceUpdateDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(await _priceUpdateRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceList>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceUpdateRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Create)]
        public virtual async Task<PriceUpdateDto> CreateAsync(PriceUpdateCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }

            var priceUpdate = await _priceUpdateManager.CreateAsync(
            input.PriceListId, input.Code, input.Description, input.EffectiveDate, input.Status, input.UpdateStatusDate
            );

            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(priceUpdate);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Edit)]
        public virtual async Task<PriceUpdateDto> UpdateAsync(Guid id, PriceUpdateUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }

            var priceUpdate = await _priceUpdateManager.UpdateAsync(
            id,
            input.PriceListId, input.Code, input.Description, input.EffectiveDate, input.Status, input.UpdateStatusDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(priceUpdate);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _priceUpdateRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.EffectiveDateMin, input.EffectiveDateMax, input.Status, input.UpdateStatusDateMin, input.UpdateStatusDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PriceUpdate>, List<PriceUpdateExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PriceUpdates.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PriceUpdateExcelDownloadTokenCacheItem { Token = token },
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