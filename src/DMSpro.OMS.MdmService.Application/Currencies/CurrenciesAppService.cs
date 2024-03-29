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

namespace DMSpro.OMS.MdmService.Currencies
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.Currencies.Default)]
    public partial class CurrenciesAppService 
    {
        public virtual async Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            var totalCount = await _currencyRepository.GetCountAsync(input.FilterText, input.Code, input.Name);
            var items = await _currencyRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CurrencyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(items)
            };
        }

        public virtual async Task<CurrencyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Currency, CurrencyDto>(await _currencyRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Currencies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _currencyRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Currencies.Create)]
        public virtual async Task<CurrencyDto> CreateAsync(CurrencyCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var currency = await _currencyManager.CreateAsync(
            input.Code, input.Name
            );

            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [Authorize(MdmServicePermissions.Currencies.Edit)]
        public virtual async Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var currency = await _currencyManager.UpdateAsync(
            id,
            input.Code, input.Name, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CurrencyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _currencyRepository.GetListAsync(input.FilterText, input.Code, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Currency>, List<CurrencyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Currencies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CurrencyExcelDownloadTokenCacheItem { Token = token },
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