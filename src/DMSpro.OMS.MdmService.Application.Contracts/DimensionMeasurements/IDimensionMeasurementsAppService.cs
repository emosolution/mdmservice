using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public partial interface IDimensionMeasurementsAppService : IApplicationService
    {
        Task<PagedResultDto<DimensionMeasurementDto>> GetListAsync(GetDimensionMeasurementsInput input);

        Task<DimensionMeasurementDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DimensionMeasurementDto> CreateAsync(DimensionMeasurementCreateDto input);

        Task<DimensionMeasurementDto> UpdateAsync(Guid id, DimensionMeasurementUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DimensionMeasurementExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}