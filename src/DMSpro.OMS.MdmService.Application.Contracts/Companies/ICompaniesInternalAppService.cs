using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Companies
{
    public interface ICompaniesInternalAppService : IApplicationService
    {
        Task<CompanyDto> FindHOCompanyOfIdentityUser(Guid identityUserId, Guid tenantId);
    }
}
