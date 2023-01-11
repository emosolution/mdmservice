using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Companies
{
    public interface ICompanyCustomRepository : IRepository<Company, Guid>
    {
        Task<Company> FindHOCompanyOfTenant(Guid tenantId);
        Task<Company> FindHOCompanyOfIdentityUser(Guid identityUser, Guid tenantId);
    }
}
    
