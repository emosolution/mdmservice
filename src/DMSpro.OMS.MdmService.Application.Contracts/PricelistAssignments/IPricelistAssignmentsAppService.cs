using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial interface IPricelistAssignmentsAppService
    {
        Task<PricelistAssignmentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input);

        Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input);
    }
}