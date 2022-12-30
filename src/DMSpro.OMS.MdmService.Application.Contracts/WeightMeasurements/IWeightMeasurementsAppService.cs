using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public interface IWeightMeasurementsAppService : IApplicationService
    {
        Task<PagedResultDto<WeightMeasurementDto>> GetListAsync(GetWeightMeasurementsInput input);

        Task<WeightMeasurementDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WeightMeasurementDto> CreateAsync(WeightMeasurementCreateDto input);

        Task<WeightMeasurementDto> UpdateAsync(Guid id, WeightMeasurementUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WeightMeasurementExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}