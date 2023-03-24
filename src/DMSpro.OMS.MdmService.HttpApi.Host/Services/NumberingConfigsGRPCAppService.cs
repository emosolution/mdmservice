using System.Threading.Tasks;
using Grpc.Core;
using Volo.Abp.MultiTenancy;
using DMSpro.OMS.Shared.Protos.MdmService.NumberingConfigs;
using System;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails;

public class NumberingConfigsGRPCAppService : NumberingConfigsProtoAppService.NumberingConfigsProtoAppServiceBase
{
    private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;
    private readonly ICurrentTenant _currentTenant;

    public NumberingConfigsGRPCAppService(
        INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
        ICurrentTenant currentTenant)
    {
        _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;
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
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
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
                await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
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