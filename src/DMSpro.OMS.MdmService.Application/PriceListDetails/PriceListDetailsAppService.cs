using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.UOMs;
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
using DMSpro.OMS.MdmService.PriceListDetails;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.PriceListDetails
{

    [Authorize(MdmServicePermissions.PriceListDetails.Default)]
    public class PriceListDetailsAppService : ApplicationService, IPriceListDetailsAppService
    {
        private readonly IDistributedCache<PriceListDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly PriceListDetailManager _priceListDetailManager;
        private readonly IRepository<PriceList, Guid> _priceListRepository;
        private readonly IRepository<UOM, Guid> _uOMRepository;

        public PriceListDetailsAppService(IPriceListDetailRepository priceListDetailRepository, PriceListDetailManager priceListDetailManager, IDistributedCache<PriceListDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<PriceList, Guid> priceListRepository, IRepository<UOM, Guid> uOMRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _priceListDetailRepository = priceListDetailRepository;
            _priceListDetailManager = priceListDetailManager; _priceListRepository = priceListRepository;
            _uOMRepository = uOMRepository;
        }

        public virtual async Task<PagedResultDto<PriceListDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceListDetailsInput input)
        {
            var totalCount = await _priceListDetailRepository.GetCountAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description, input.PriceListId, input.UOMId);
            var items = await _priceListDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description, input.PriceListId, input.UOMId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PriceListDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceListDetailWithNavigationProperties>, List<PriceListDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PriceListDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PriceListDetailWithNavigationProperties, PriceListDetailWithNavigationPropertiesDto>
                (await _priceListDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _priceListDetailRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<PriceListDetail>, IEnumerable<PriceListDetailDto>>(results.data.Cast<PriceListDetail>());
            
            return results;
                
        }
        public virtual async Task<PriceListDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceListDetail, PriceListDetailDto>(await _priceListDetailRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOM>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOM>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceListDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceListDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceListDetails.Create)]
        public virtual async Task<PriceListDetailDto> CreateAsync(PriceListDetailCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.UOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var priceListDetail = await _priceListDetailManager.CreateAsync(
            input.PriceListId, input.UOMId, input.Price, input.Description, input.BasedOnPrice
            );

            return ObjectMapper.Map<PriceListDetail, PriceListDetailDto>(priceListDetail);
        }

        [Authorize(MdmServicePermissions.PriceListDetails.Edit)]
        public virtual async Task<PriceListDetailDto> UpdateAsync(Guid id, PriceListDetailUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.UOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var priceListDetail = await _priceListDetailManager.UpdateAsync(
            id,
            input.PriceListId, input.UOMId, input.Price, input.Description, input.BasedOnPrice, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PriceListDetail, PriceListDetailDto>(priceListDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _priceListDetailRepository.GetListAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PriceListDetail>, List<PriceListDetailExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PriceListDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PriceListDetailExcelDownloadTokenCacheItem { Token = token },
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