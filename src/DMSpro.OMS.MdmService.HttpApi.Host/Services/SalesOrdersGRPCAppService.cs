using System.Threading.Tasks;
using Grpc.Core;
using Volo.Abp.MultiTenancy;
using System;
using DMSpro.OMS.Shared.Protos.MdmService.SalesOrders;

namespace DMSpro.OMS.MdmService.SalesOrders;

public class SalesOrdersGRPCAppService : SalesOrdersProtoAppService.SalesOrdersProtoAppServiceBase
{
    private readonly ISalesOrdersAppService _salesOrdersAppService;
    private readonly ICurrentTenant _currentTenant;

    public SalesOrdersGRPCAppService(
        ISalesOrdersAppService salesOrdersAppService,
        ICurrentTenant currentTenant)
    {
        _salesOrdersAppService = salesOrdersAppService;
        _currentTenant = currentTenant;
    }

    public override async Task<GetInfoSOResponse>
        GetInfoSO(GetInfoSORequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        Guid companyId = Guid.Parse(request.CompanyId);
        DateTime postingDate = DateTime.Parse(request.PostingDate);
        DateTime? lastUpdateDate =
            string.IsNullOrEmpty(request.LastUpdateDate)
                ? null : DateTime.Parse(request.LastUpdateDate);
        using (_currentTenant.Change(tenantId))
        {
            var result =
                await _salesOrdersAppService.GetInfoSOAsync(companyId, postingDate, lastUpdateDate);
            GetInfoSOResponse response = new()
            {
                TenantId = request.TenantId,
                CompanyId = request.CompanyId,
                JsonString = result,
                UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };
            return response;
        }
    }
}