using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdates;
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

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{

    [Authorize(MdmServicePermissions.PriceUpdateDetails.Default)]
    public partial class PriceUpdateDetailsAppService 
    {
        public virtual async Task<PagedResultDto<PriceUpdateDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdateDetailsInput input)
        {
            var totalCount = await _priceUpdateDetailRepository.GetCountAsync(input.FilterText, input.PriceBeforeUpdateMin, input.PriceBeforeUpdateMax, input.NewPriceMin, input.NewPriceMax, input.UpdatedDateMin, input.UpdatedDateMax, input.PriceUpdateId, input.PriceListDetailId);
            var items = await _priceUpdateDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceBeforeUpdateMin, input.PriceBeforeUpdateMax, input.NewPriceMin, input.NewPriceMax, input.UpdatedDateMin, input.UpdatedDateMax, input.PriceUpdateId, input.PriceListDetailId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PriceUpdateDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceUpdateDetailWithNavigationProperties>, List<PriceUpdateDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PriceUpdateDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdateDetailWithNavigationProperties, PriceUpdateDetailWithNavigationPropertiesDto>
                (await _priceUpdateDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PriceUpdateDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(await _priceUpdateDetailRepository.GetAsync(id));
        }

        
        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceUpdateLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceUpdateRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceUpdate>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceUpdate>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListDetailLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListDetailRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Description != null &&
                         x.Description.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceListDetail>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceListDetail>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceUpdateDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceUpdateDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceUpdateDetails.Create)]
        public virtual async Task<PriceUpdateDetailDto> CreateAsync(PriceUpdateDetailCreateDto input)
        {
            if (input.PriceUpdateId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceUpdate"]]);
            }
            if (input.PriceListDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceListDetail"]]);
            }

            var priceUpdateDetail = await _priceUpdateDetailManager.CreateAsync(
            input.PriceUpdateId, input.PriceListDetailId, input.PriceBeforeUpdate, input.NewPrice, input.UpdatedDate
            );

            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(priceUpdateDetail);
        }

        [Authorize(MdmServicePermissions.PriceUpdateDetails.Edit)]
        public virtual async Task<PriceUpdateDetailDto> UpdateAsync(Guid id, PriceUpdateDetailUpdateDto input)
        {
            if (input.PriceUpdateId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceUpdate"]]);
            }
            if (input.PriceListDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceListDetail"]]);
            }

            var priceUpdateDetail = await _priceUpdateDetailManager.UpdateAsync(
            id,
            input.PriceUpdateId, input.PriceListDetailId, input.PriceBeforeUpdate, input.NewPrice, input.UpdatedDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(priceUpdateDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _priceUpdateDetailRepository.GetListAsync(input.FilterText, input.PriceBeforeUpdateMin, input.PriceBeforeUpdateMax, input.NewPriceMin, input.NewPriceMax, input.UpdatedDateMin, input.UpdatedDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PriceUpdateDetail>, List<PriceUpdateDetailExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PriceUpdateDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PriceUpdateDetailExcelDownloadTokenCacheItem { Token = token },
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