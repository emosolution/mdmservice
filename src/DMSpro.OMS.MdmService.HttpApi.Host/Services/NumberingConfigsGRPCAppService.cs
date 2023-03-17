using System.Threading.Tasks;
using Grpc.Core;
using Volo.Abp.MultiTenancy;
using DMSpro.OMS.Shared.Protos.MdmService.NumberingConfigs;
using Volo.Saas.Tenants;
using System;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails;

public class NumberingConfigsGRPCAppService : NumberingConfigsProtoAppService.NumberingConfigsProtoAppServiceBase
{
    private readonly INumberingConfigDetailsAppService _numberingConfigDetailsAppService;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICurrentTenant _currentTenant;

    public NumberingConfigsGRPCAppService(
        INumberingConfigDetailsAppService numberingConfigDetailsAppService,
        ICompanyRepository companyRepository,
        ICurrentTenant currentTenant)
    {
        _numberingConfigDetailsAppService = numberingConfigDetailsAppService;
        _companyRepository = companyRepository;
        _currentTenant = currentTenant;
    }

    public override async Task<GetSuggestedNumberingConfigResponse>
        GetSuggestedNumberingConfig(
            GetSuggestedNumberingConfigRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        Guid companyId = Guid.Parse(request.CompanyId);
        GetSuggestedNumberingConfigResponse response = new();
        using (_currentTenant.Change(tenantId))
        {
            var dto =
                await _numberingConfigDetailsAppService.GetSuggestedNumberingConfigAsync(
                    request.ObjectType, companyId);
            response.NumberingConfig =
                new DMSpro.OMS.Shared.Protos.MdmService.NumberingConfigs.NumberingConfig()
                {
                    TenantId = request.TenantId,
                    Prefix = dto.Prefix,
                    PaddingZeroNumber = dto.PaddingZeroNumber,
                    Suffix = dto.Suffix,
                    Active = dto.Active,
                    CurrentNumber = dto.CurrentNumber,
                    NumberingConfigId = dto.Id.ToString(),
                    CompanyId = request.CompanyId,
                };
            return response;
        }
    }

    public override async Task<SaveNumberingConfigResponse>
        SaveNumberingConfig(
            SaveNumberingConfigRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        Guid companyId = Guid.Parse(request.CompanyId);
        SaveNumberingConfigResponse response = new();
        using (_currentTenant.Change(tenantId))
        {
            var dto =
                await _numberingConfigDetailsAppService.SaveNumberingConfigAsync(
                    request.ObjectType, companyId, request.CurrentNumber);
            response.NumberingConfig =
                new DMSpro.OMS.Shared.Protos.MdmService.NumberingConfigs.NumberingConfig()
                {
                    TenantId = request.TenantId,
                    Prefix = dto.Prefix,
                    PaddingZeroNumber = dto.PaddingZeroNumber,
                    Suffix = dto.Suffix,
                    Active = dto.Active,
                    CurrentNumber = dto.CurrentNumber,
                    NumberingConfigId = dto.Id.ToString(),
                    CompanyId = request.CompanyId,
                };
            return response;
        }
    }

}