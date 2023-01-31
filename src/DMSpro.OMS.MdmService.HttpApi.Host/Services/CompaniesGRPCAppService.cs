using System;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Protos.MdmService.Companies;
using Grpc.Core;
using DMSpro.OMS.MdmService.Helpers;

namespace DMSpro.OMS.MdmService.Companies;

public class CompaniesGRPCAppService : CompaniesProtoAppService.CompaniesProtoAppServiceBase
{
    readonly ICompaniesInternalAppService _companiesInternalAppService;

    public CompaniesGRPCAppService(ICompaniesInternalAppService companiesInternalAppService)
    {
        _companiesInternalAppService = companiesInternalAppService;
    }

    public override async Task<CompanyResponse> GetHOCompanyWithIdentityUser(
        GetHOCompanyWithIdentityUserRequest request, ServerCallContext context)
    {
        Guid? tenantId = null;
        if (!string.IsNullOrEmpty(request.TenantId))
        {
            tenantId = new Guid(request.TenantId);
        }
        CompanyWithTenantDto companyDto =
            await _companiesInternalAppService.GetHOCompanyFromIdentityUser(
                new Guid(request.IdentityUserId), tenantId);
        var response = new CompanyResponse();
        if (companyDto == null)
        {
            return response;
        }
        response.Company = new OMS.Shared.Protos.MdmService.Companies.Company()
        {
            Id = companyDto.Id.ToString(),
            TenantId = companyDto.TenantId == null ? "" : companyDto.TenantId.ToString(),
            Code = companyDto.Code,
            Name = companyDto.Name,
            Active = false,
            HO = companyDto.IsHO,
        };
        return response;
    }

    public override async Task<CompanyResponse> GetCompanyWithIdentityUser(
        GetCompanyWithIdentityUserRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId)? null : Guid.Parse(request.TenantId);
        CompanyWithTenantDto companyDto =
            await _companiesInternalAppService.CheckCompanyBelongToIdentityUser(
                new Guid(request.CompanyId), new Guid(request.IdentityUserId), tenantId);
        var response = new CompanyResponse();
        if (companyDto == null)
        {
            return response;
        }

        response.Company = new OMS.Shared.Protos.MdmService.Companies.Company()
        {
            Id = companyDto.Id.ToString(),
            TenantId = request.TenantId,
            Code = companyDto.Code,
            Name = companyDto.Name,
            Active = MDMHelpers.CheckActive(companyDto.Active, companyDto.EffectiveDate, companyDto.EndDate),
            HO = companyDto.IsHO
        };
        return response;
    }
}
