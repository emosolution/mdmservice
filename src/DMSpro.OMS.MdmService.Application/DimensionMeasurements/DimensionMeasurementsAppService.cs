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
using DMSpro.OMS.MdmService.DimensionMeasurements;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.DimensionMeasurements.Default)]
    public class DimensionMeasurementsAppService : ApplicationService, IDimensionMeasurementsAppService
    {
        private readonly IDistributedCache<DimensionMeasurementExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IDimensionMeasurementRepository _dimensionMeasurementRepository;
        private readonly DimensionMeasurementManager _dimensionMeasurementManager;

        public DimensionMeasurementsAppService(IDimensionMeasurementRepository dimensionMeasurementRepository, DimensionMeasurementManager dimensionMeasurementManager, IDistributedCache<DimensionMeasurementExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _dimensionMeasurementRepository = dimensionMeasurementRepository;
            _dimensionMeasurementManager = dimensionMeasurementManager;
        }

        public virtual async Task<PagedResultDto<DimensionMeasurementDto>> GetListAsync(GetDimensionMeasurementsInput input)
        {
            var totalCount = await _dimensionMeasurementRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax);
            var items = await _dimensionMeasurementRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DimensionMeasurementDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DimensionMeasurement>, List<DimensionMeasurementDto>>(items)
            };
        }

        public virtual async Task<DimensionMeasurementDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<DimensionMeasurement, DimensionMeasurementDto>(await _dimensionMeasurementRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.DimensionMeasurements.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _dimensionMeasurementRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.DimensionMeasurements.Create)]
        public virtual async Task<DimensionMeasurementDto> CreateAsync(DimensionMeasurementCreateDto input)
        {

            var dimensionMeasurement = await _dimensionMeasurementManager.CreateAsync(
            input.Code, input.Name, input.Value
            );

            return ObjectMapper.Map<DimensionMeasurement, DimensionMeasurementDto>(dimensionMeasurement);
        }

        [Authorize(MdmServicePermissions.DimensionMeasurements.Edit)]
        public virtual async Task<DimensionMeasurementDto> UpdateAsync(Guid id, DimensionMeasurementUpdateDto input)
        {

            var dimensionMeasurement = await _dimensionMeasurementManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Value, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<DimensionMeasurement, DimensionMeasurementDto>(dimensionMeasurement);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DimensionMeasurementExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _dimensionMeasurementRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ValueMin, input.ValueMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<DimensionMeasurement>, List<DimensionMeasurementExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "DimensionMeasurements.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new DimensionMeasurementExcelDownloadTokenCacheItem { Token = token },
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
