using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public partial interface IWorkingPositionsAppService : IApplicationService
    {
        Task<WorkingPositionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WorkingPositionDto> CreateAsync(WorkingPositionCreateDto input);

        Task<WorkingPositionDto> UpdateAsync(Guid id, WorkingPositionUpdateDto input);
    }
}