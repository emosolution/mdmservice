using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial interface IPricelistAssignmentsAppService
    {
        Task<DateTime> ReleaseAsync(Guid id);
    }
}