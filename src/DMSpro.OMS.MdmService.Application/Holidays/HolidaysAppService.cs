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
using DMSpro.OMS.MdmService.Holidays;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Holidays
{

    [Authorize(MdmServicePermissions.Holidays.Default)]
    public partial class HolidaysAppService : ApplicationService, IHolidaysAppService
    {
        private readonly IDistributedCache<HolidayExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IHolidayRepository _holidayRepository;
        private readonly HolidayManager _holidayManager;

        public HolidaysAppService(IHolidayRepository holidayRepository, HolidayManager holidayManager, IDistributedCache<HolidayExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _holidayRepository = holidayRepository;
            _holidayManager = holidayManager;
        }

        public virtual async Task<PagedResultDto<HolidayDto>> GetListAsync(GetHolidaysInput input)
        {
            var totalCount = await _holidayRepository.GetCountAsync(input.FilterText, input.YearMin, input.YearMax, input.Description);
            var items = await _holidayRepository.GetListAsync(input.FilterText, input.YearMin, input.YearMax, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<HolidayDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Holiday>, List<HolidayDto>>(items)
            };
        }

        public virtual async Task<HolidayDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Holiday, HolidayDto>(await _holidayRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Holidays.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _holidayRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Holidays.Create)]
        public virtual async Task<HolidayDto> CreateAsync(HolidayCreateDto input)
        {

            var holiday = await _holidayManager.CreateAsync(
            input.Year, input.Description
            );

            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }

        [Authorize(MdmServicePermissions.Holidays.Edit)]
        public virtual async Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input)
        {

            var holiday = await _holidayManager.UpdateAsync(
            id,
            input.Year, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _holidayRepository.GetListAsync(input.FilterText, input.YearMin, input.YearMax, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Holiday>, List<HolidayExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Holidays.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new HolidayExcelDownloadTokenCacheItem { Token = token },
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