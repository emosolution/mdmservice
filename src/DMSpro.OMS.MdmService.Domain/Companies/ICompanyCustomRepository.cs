using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial interface ICompanyRepository 
    {
        Task<Company> GetHOCompanyOfTenantAsync(Guid? tenantId);
        Task<Company> GetHOCompanyFromIdentityUserAsync(Guid identityUser, Guid? tenantId);
    }
}
    
