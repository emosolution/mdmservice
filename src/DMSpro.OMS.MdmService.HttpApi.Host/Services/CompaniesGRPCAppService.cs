using System;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Protos.MdmService.Companies;
using Grpc.Core;
using DMSpro.OMS.MdmService.Helpers;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.Companies;

public class CompaniesGRPCAppService : CompaniesProtoAppService.CompaniesProtoAppServiceBase
{
    private readonly ICompaniesInternalAppService _companiesInternalAppService;
    private readonly ICurrentTenant _currentTenant;

    public CompaniesGRPCAppService(ICompaniesInternalAppService companiesInternalAppService,
        ICurrentTenant currentTenant)
    {
        _companiesInternalAppService = companiesInternalAppService;
        _currentTenant = currentTenant;
    }


    public override async Task<CompanyResponse> GetHOCompanyWithIdentityUser(
        GetHOCompanyWithIdentityUserRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
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
    }


    public override async Task<CompanyResponse> GetCompanyWithIdentityUser(
        GetCompanyWithIdentityUserRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
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
}
