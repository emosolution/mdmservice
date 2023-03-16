using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial interface ICompanyRepository 
    {
        Task<Company> GetHOCompanyOfTenantAsync(Guid? tenantId);
        Task<Company> GetHOCompanyFromIdentityUserAsync(Guid identityUser, Guid? tenantId);
        Task<Company> CheckActiveAsync(Guid id, bool throwErrorOnInactive = false);
        Task<Company> CheckActiveWithDateAsync(Guid id, DateTime checkingDate, bool throwErrorOnInactive = false);
    }
}
    
