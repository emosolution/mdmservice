using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Companies;
using System;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public interface ICompanyIdentityUserAssignmentsInternalAppService
    {
        Task<CompanyDto> GetCurrentlySelectedCompanyAsync(
            Guid? identityUserId = null, DateTime? checkTime = null);
    }
}