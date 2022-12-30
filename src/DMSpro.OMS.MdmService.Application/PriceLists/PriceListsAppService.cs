using DMSpro.OMS.MdmService.Shared;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.PriceLists
{

    [Authorize(MdmServicePermissions.PriceLists.Default)]
    public class PriceListsAppService : ApplicationService, IPriceListsAppService
    {
        private readonly IDistributedCache<PriceListExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPriceListRepository _priceListRepository;
        private readonly PriceListManager _priceListManager;

        public PriceListsAppService(IPriceListRepository priceListRepository, PriceListManager priceListManager, IDistributedCache<PriceListExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _priceListRepository = priceListRepository;
            _priceListManager = priceListManager;
        }

        public virtual async Task<PagedResultDto<PriceListWithNavigationPropertiesDto>> GetListAsync(GetPriceListsInput input)
        {
            var totalCount = await _priceListRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList, input.BasePriceListId);
            var items = await _priceListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList, input.BasePriceListId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PriceListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceListWithNavigationProperties>, List<PriceListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PriceListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PriceListWithNavigationProperties, PriceListWithNavigationPropertiesDto>
                (await _priceListRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _priceListRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<PriceList>, IEnumerable<PriceListDto>>(results.data.Cast<PriceList>());
            
            return results;
                
        }
        public virtual async Task<PriceListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceList, PriceListDto>(await _priceListRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceList>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceLists.Create)]
        public virtual async Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {

            var priceList = await _priceListManager.CreateAsync(
            input.BasePriceListId, input.Code, input.Name, input.Active, input.IsFirstPriceList, input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType
            );

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [Authorize(MdmServicePermissions.PriceLists.Edit)]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {

            var priceList = await _priceListManager.UpdateAsync(
            id,
            input.BasePriceListId, input.Code, input.Name, input.Active, input.IsFirstPriceList, input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _priceListRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PriceList>, List<PriceListExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PriceLists.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PriceListExcelDownloadTokenCacheItem { Token = token },
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