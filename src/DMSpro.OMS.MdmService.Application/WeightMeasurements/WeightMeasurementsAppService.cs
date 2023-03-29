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

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.WeightMeasurements.Default)]
    public partial class WeightMeasurementsAppService 
    {
        public virtual async Task<PagedResultDto<WeightMeasurementDto>> GetListAsync(GetWeightMeasurementsInput input)
        {
            var totalCount = await _weightMeasurementRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax);
            var items = await _weightMeasurementRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WeightMeasurementDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WeightMeasurement>, List<WeightMeasurementDto>>(items)
            };
        }

        public virtual async Task<WeightMeasurementDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<WeightMeasurement, WeightMeasurementDto>(await _weightMeasurementRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.WeightMeasurements.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _weightMeasurementRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.WeightMeasurements.Create)]
        public virtual async Task<WeightMeasurementDto> CreateAsync(WeightMeasurementCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);

            var weightMeasurement = await _weightMeasurementManager.CreateAsync(
            input.Code, input.Name, input.Value
            );

            return ObjectMapper.Map<WeightMeasurement, WeightMeasurementDto>(weightMeasurement);
        }

        [Authorize(MdmServicePermissions.WeightMeasurements.Edit)]
        public virtual async Task<WeightMeasurementDto> UpdateAsync(Guid id, WeightMeasurementUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);

            var weightMeasurement = await _weightMeasurementManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Value, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<WeightMeasurement, WeightMeasurementDto>(weightMeasurement);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WeightMeasurementExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _weightMeasurementRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<WeightMeasurement>, List<WeightMeasurementExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "WeightMeasurements.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new WeightMeasurementExcelDownloadTokenCacheItem { Token = token },
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