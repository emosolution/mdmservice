using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial interface ICompanyRepository 
    {
        Task<Company> GetHOCompanyOfTenant(Guid? tenantId);
        Task<Company> GetHOCompanyFromIdentityUser(Guid identityUser, Guid? tenantId);
        Task<bool> CheckActive(Guid id, bool throwErrorOnInactive);
    }
}
    
