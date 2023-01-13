using System;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.Shared.Protos.MdmService.Companies;
using Grpc.Core;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Companies;

public class CompaniesGRPCAppService : CompaniesProtoAppService.CompaniesProtoAppServiceBase
{
    readonly ICompaniesInternalAppService _companiesInternalAppService;

    public CompaniesGRPCAppService(ICompaniesInternalAppService companiesInternalAppService)
    {
        _companiesInternalAppService = companiesInternalAppService;
    }

    public override async Task<GetHOCompanyFromIdentityUserAndTenantResponse> GetHOCompanyFromIdentityUserAndTenant(
        GetHOCompanyFromIdentityUserAndTenantRequest request, ServerCallContext context)
    {
        Guid? tenantId = null;
        if (!string.IsNullOrEmpty(request.TenantId))
        {
            tenantId = new Guid(request.TenantId);
        }
        CompanyWithTenantDto companyDto =
            await _companiesInternalAppService.GetHOCompanyFromIdentityUserAndTenant(
                new Guid(request.IdentityUserId), tenantId);
        var response = new GetHOCompanyFromIdentityUserAndTenantResponse();
        if (companyDto == null)
        {
            return response;
        }
        response.Company = new OMS.Shared.Protos.MdmService.Companies.Company()
        {
            Id = companyDto.Id.ToString(),
            TenantId = companyDto.TenantId.ToString(),
            Code = companyDto.Code,
            Name = companyDto.Name,
        };
        return response;
    }
}
