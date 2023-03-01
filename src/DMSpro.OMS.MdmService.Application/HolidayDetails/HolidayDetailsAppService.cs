using DMSpro.OMS.MdmService.Holidays;
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
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.HolidayDetails
{

    [Authorize(MdmServicePermissions.HolidayDetails.Default)]
    public partial class HolidayDetailsAppService 
    {
        public virtual async Task<PagedResultDto<HolidayDetailWithNavigationPropertiesDto>> GetListAsync(GetHolidayDetailsInput input)
        {
            var totalCount = await _holidayDetailRepository.GetCountAsync(input.FilterText, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.Description, input.HolidayId);
            var items = await _holidayDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.Description, input.HolidayId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<HolidayDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<HolidayDetailWithNavigationProperties>, List<HolidayDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<HolidayDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<HolidayDetailWithNavigationProperties, HolidayDetailWithNavigationPropertiesDto>
                (await _holidayDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<HolidayDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(await _holidayDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetHolidayLookupAsync(LookupRequestDto input)
        {
            var query = (await _holidayRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Description != null &&
                         x.Description.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Holiday>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Holiday>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.HolidayDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _holidayDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.HolidayDetails.Create)]
        public virtual async Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input)
        {
            if (input.HolidayId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Holiday"]]);
            }

            var holidayDetail = await _holidayDetailManager.CreateAsync(
            input.HolidayId, input.StartDate, input.EndDate, input.Description
            );

            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(holidayDetail);
        }

        [Authorize(MdmServicePermissions.HolidayDetails.Edit)]
        public virtual async Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input)
        {
            if (input.HolidayId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Holiday"]]);
            }

            var holidayDetail = await _holidayDetailManager.UpdateAsync(
            id,
            input.HolidayId, input.StartDate, input.EndDate, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<HolidayDetail, HolidayDetailDto>(holidayDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var holidayDetails = await _holidayDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.Description);
            var items = holidayDetails.Select(item => new
            {
                StartDate = item.HolidayDetail.StartDate,
                EndDate = item.HolidayDetail.EndDate,
                Description = item.HolidayDetail.Description,

                HolidayDescription = item.Holiday?.Description,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "HolidayDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new HolidayDetailExcelDownloadTokenCacheItem { Token = token },
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