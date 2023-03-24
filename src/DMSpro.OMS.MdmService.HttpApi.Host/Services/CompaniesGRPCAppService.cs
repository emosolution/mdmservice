using System;
using System.Threading.Tasks;
using DMSpro.OMS.Shared.Protos.MdmService.Companies;
using Grpc.Core;
using DMSpro.OMS.MdmService.Helpers;
using Volo.Abp.MultiTenancy;
using System.Linq;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.Companies;

public class CompaniesGRPCAppService : CompaniesProtoAppService.CompaniesProtoAppServiceBase
{
    private readonly ICompaniesInternalAppService _companiesInternalAppService;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyIdentityUserAssignmentRepository _userAssignmentRepository;
    private readonly ICompanyIdentityUserAssignmentsInternalAppService _userAssignmentsInternalAppService;
    private readonly ICurrentTenant _currentTenant;

    public CompaniesGRPCAppService(ICompaniesInternalAppService companiesInternalAppService,
        ICompanyRepository companyRepository,
        ICompanyIdentityUserAssignmentRepository userAssignmentRepository,
        ICompanyIdentityUserAssignmentsInternalAppService userAssignmentsInternalAppService,
        ICurrentTenant currentTenant)
    {
        _companiesInternalAppService = companiesInternalAppService;
        _companyRepository = companyRepository;
        _userAssignmentRepository = userAssignmentRepository;
        _userAssignmentsInternalAppService = userAssignmentsInternalAppService;
        _currentTenant = currentTenant;
    }

    public override async Task<CompanyResponse> GetHOCompanyWithIdentityUser(
        GetHOCompanyWithIdentityUserRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            CompanyWithTenantDto companyDto =
                await _companiesInternalAppService.GetHOCompanyFromIdentityUserAsync(
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
            await _companiesInternalAppService.CheckCompanyBelongToIdentityUserAsync(
                new Guid(request.CompanyId), new Guid(request.IdentityUserId), tenantId);
            var response = new CompanyResponse();
            if (companyDto == null)
            {
                return response;
            }

            if (request.CheckCurrentlySelected == true)
            {
                var assignment = await _userAssignmentRepository.GetListAsync(
                    x => x.IdentityUserId.ToString() == request.IdentityUserId &&
                    x.CompanyId.ToString() == request.CompanyId && x.CurrentlySelected == true);
                if (assignment.Count != 1)
                {
                    return response;
                }
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

    public override async Task<GetListCompaniesResponse> GetListCompanies(
        GetListCompaniesRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            var companies = await _companyRepository.GetListAsync(
                x => request.CompanyIds.Contains(x.Id.ToString()) &&
                x.TenantId == tenantId);
            var response = new GetListCompaniesResponse();
            if (companies.Count < 1)
            {
                return response;
            }
            response.Companies.Add(
                companies.Select(
                    x => new DMSpro.OMS.Shared.Protos.MdmService.Companies.Company()
                    {
                        Id = x.Id.ToString(),
                        TenantId = request.TenantId,
                        Code = x.Code,
                        Name = x.Name,
                        Active = x.Active,
                        HO = x.IsHO,
                    }));
            return response;
        }
    }

    public override async Task<CompanyResponse> GetCurrentlySelectedCompany(
        GetCurrentlySelectedCompanyRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        Guid? identityUserId = string.IsNullOrEmpty(request.IdentityUserId) ? null : new(request.IdentityUserId);
        using (_currentTenant.Change(tenantId))
        {
            var dto =
                await _userAssignmentsInternalAppService.GetCurrentlySelectedCompanyAsync(identityUserId);
            CompanyResponse response = new()
            {
                Company = new()
                {
                    Id = dto.Id.ToString(),
                    TenantId = request.TenantId,
                    Code = dto.Code,
                    Name = dto.Name,
                    Active = dto.Active,
                    HO = dto.IsHO,
                },
            };
            return response;
        }
    }
}
