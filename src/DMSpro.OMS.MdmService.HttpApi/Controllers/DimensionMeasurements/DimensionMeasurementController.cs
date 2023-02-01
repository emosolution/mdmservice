using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.DimensionMeasurements
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("DimensionMeasurement")]
    [Route("api/mdm-service/dimension-measurements")]
    public partial class DimensionMeasurementController : AbpController, IDimensionMeasurementsAppService
    {
        private readonly IDimensionMeasurementsAppService _dimensionMeasurementsAppService;

        public DimensionMeasurementController(IDimensionMeasurementsAppService dimensionMeasurementsAppService)
        {
            _dimensionMeasurementsAppService = dimensionMeasurementsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<DimensionMeasurementDto>> GetListAsync(GetDimensionMeasurementsInput input)
        {
            return _dimensionMeasurementsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DimensionMeasurementDto> GetAsync(Guid id)
        {
            return _dimensionMeasurementsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<DimensionMeasurementDto> CreateAsync(DimensionMeasurementCreateDto input)
        {
            return _dimensionMeasurementsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DimensionMeasurementDto> UpdateAsync(Guid id, DimensionMeasurementUpdateDto input)
        {
            return _dimensionMeasurementsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _dimensionMeasurementsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DimensionMeasurementExcelDownloadDto input)
        {
            return _dimensionMeasurementsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _dimensionMeasurementsAppService.GetDownloadTokenAsync();
        }
    }
}