using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Companies
{
    public interface ICompanyCustomRepository : IRepository<Company, Guid>
    {
        Task<Company> GetHOCompanyOfTenant(Guid? tenantId);
        Task<Company> GetHOCompanyFromIdentityUserAndTenant(Guid identityUser, Guid? tenantId);
    }
}
    
