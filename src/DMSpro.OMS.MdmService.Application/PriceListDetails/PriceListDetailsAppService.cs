using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Items;
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
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.PriceListDetails
{

    [Authorize(MdmServicePermissions.PriceListDetails.Default)]
    public partial class PriceListDetailsAppService
    {
        public virtual async Task<PagedResultDto<PriceListDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceListDetailsInput input)
        {
            var totalCount = await _priceListDetailRepository.GetCountAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description, input.PriceListId, input.UOMId, input.ItemId);
            var items = await _priceListDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description, input.PriceListId, input.UOMId, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Item>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Item>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceListDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var detail = await _priceListDetailRepository.GetAsync(id);
            await CheckHeader(detail.PriceListId);
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
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            await CheckHeader(input.PriceListId);
            var priceListDetail = await _priceListDetailManager.CreateAsync(
            input.PriceListId, input.UOMId, input.ItemId, input.Price, input.Description, input.BasedOnPrice
            );

            return ObjectMapper.Map<PriceListDetail, PriceListDetailDto>(priceListDetail);
        }

        [Authorize(MdmServicePermissions.PriceListDetails.Edit)]
        public virtual async Task<PriceListDetailDto> UpdateAsync(Guid id, PriceListDetailUpdateDto input)
        {
            if (input.UOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }
            var detail = await _priceListDetailRepository.GetAsync(id);
            await CheckHeader(detail.PriceListId);
            var record = await _priceListDetailManager.UpdateAsync(
                id,
                input.UOMId, input.ItemId, input.Price, input.Description, input.BasedOnPrice, 
                input.ConcurrencyStamp);

            return ObjectMapper.Map<PriceListDetail, PriceListDetailDto>(record);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var priceListDetails = await _priceListDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceMin, input.PriceMax, input.BasedOnPriceMin, input.BasedOnPriceMax, input.Description);
            var items = priceListDetails.Select(item => new
            {
                Price = item.PriceListDetail.Price,
                BasedOnPrice = item.PriceListDetail.BasedOnPrice,
                Description = item.PriceListDetail.Description,

                PriceListCode = item.PriceList?.Code,
                UOMCode = item.UOM?.Code,
                ItemCode = item.Item?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
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

        private async Task CheckHeader(Guid headerId)
        {
            var header = await _priceListRepository.GetAsync(headerId);
            if (header.IsReleased == true)
            {
                throw new UserFriendlyException(message: L["Error:PriceListsAppService:550"], code: "1");
            }
        }
    }
}