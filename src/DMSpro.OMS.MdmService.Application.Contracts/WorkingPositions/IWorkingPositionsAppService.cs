using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public interface IWorkingPositionsAppService : IApplicationService
    {
        Task<PagedResultDto<WorkingPositionDto>> GetListAsync(GetWorkingPositionsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<WorkingPositionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input);

        Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkingPositionExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}