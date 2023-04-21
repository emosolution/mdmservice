using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public partial interface ISalesOrgEmpAssignmentsAppService : IApplicationService
    {
        Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input);

        Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input);
    }
}