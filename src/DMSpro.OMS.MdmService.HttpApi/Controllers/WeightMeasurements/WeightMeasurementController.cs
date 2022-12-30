using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.WeightMeasurements;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.WeightMeasurements
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("WeightMeasurement")]
    [Route("api/mdm-service/weight-measurements")]
    public class WeightMeasurementController : AbpController, IWeightMeasurementsAppService
    {
        private readonly IWeightMeasurementsAppService _weightMeasurementsAppService;

        public WeightMeasurementController(IWeightMeasurementsAppService weightMeasurementsAppService)
        {
            _weightMeasurementsAppService = weightMeasurementsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WeightMeasurementDto>> GetListAsync(GetWeightMeasurementsInput input)
        {
            return _weightMeasurementsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WeightMeasurementDto> GetAsync(Guid id)
        {
            return _weightMeasurementsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<WeightMeasurementDto> CreateAsync(WeightMeasurementCreateDto input)
        {
            return _weightMeasurementsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WeightMeasurementDto> UpdateAsync(Guid id, WeightMeasurementUpdateDto input)
        {
            return _weightMeasurementsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _weightMeasurementsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(WeightMeasurementExcelDownloadDto input)
        {
            return _weightMeasurementsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _weightMeasurementsAppService.GetDownloadTokenAsync();
        }
    }
}