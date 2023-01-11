using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using DMSpro.OMS.Shared.GRPC.MdmService.Companies;

namespace DMSpro.OMS.MdmService.Companies;

public class CompaniesGRPCAppService : ApplicationService, ICompaniesGRPCService
{
    readonly ICompaniesInternalAppService _companiesInternalAppService;

    public CompaniesGRPCAppService(ICompaniesInternalAppService companiesInternalAppService)
    {
        _companiesInternalAppService = companiesInternalAppService;
    }

    public async Task<CompanyGRPCDto> FindHOCompanyOfIdentityUser(Guid identityUserId, Guid tenantId)
    {
        CompanyDto companyDto = await _companiesInternalAppService.FindHOCompanyOfIdentityUser(identityUserId, tenantId);
        return ObjectMapper.Map<CompanyDto, CompanyGRPCDto>(companyDto);
    }
}