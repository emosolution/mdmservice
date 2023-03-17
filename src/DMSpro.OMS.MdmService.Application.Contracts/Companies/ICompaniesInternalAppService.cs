using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Companies
{
    public interface ICompaniesInternalAppService : IApplicationService
    {
        Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAsync(Guid identityUserId, Guid? tenantId);

        Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUserAsync(Guid companyId, Guid identityUserId, Guid? tenantId);

        Task<CompanyDto> CheckActiveAsync(Guid id, DateTime? checkingDate, bool throwErrorOnInactive = false);
    }
}
