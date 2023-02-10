using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentRepository
    {
        Task<IQueryable<CompanyIdentityUserAssignmentWithNavigationProperties>> 
            GetQueryAbleForNavigationPropertiesAsync(Guid? userId);
    }
}
