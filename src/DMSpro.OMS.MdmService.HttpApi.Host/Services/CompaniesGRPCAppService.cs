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
    private readonly ICompanyIdentityUserAssignmentsInternalAppService _userAssignmentsInternalAppService;
    private readonly ICurrentTenant _currentTenant;

    public CompaniesGRPCAppService(
        ICompanyIdentityUserAssignmentsInternalAppService userAssignmentsInternalAppService,
        ICurrentTenant currentTenant)
    {
        _userAssignmentsInternalAppService = userAssignmentsInternalAppService;
        _currentTenant = currentTenant;
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
