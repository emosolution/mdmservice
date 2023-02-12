using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentRepository
    {
        Task<List<CompanyIdentityUserAssignment>> GetByIdAsync(List<Guid> ids);
    }
}