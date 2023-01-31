using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Vendors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Companies
{
    public interface ICompaniesInternalAppService : IApplicationService
    {
        //Task<CompanyWithTenantDto> GetWithTenantIdAsynce(Guid id);

        Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAndTenant(Guid identityUserId, Guid? tenantId);

        Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUserAndTenant(Guid companyId, Guid identityUserId, Guid? tenantId);
    }
}
