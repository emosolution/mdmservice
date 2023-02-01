using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.VATs;

namespace DMSpro.OMS.MdmService.VATs;

public class VATsGRPCAppService : VATsProtoAppService.VATsProtoAppServiceBase
{
    readonly IVATRepository _repository;

    public VATsGRPCAppService(IVATRepository repository)
    {
        _repository = repository;
    }

    public override async Task<VATResponse> GetVAT(GetVATRequest request, ServerCallContext context)
    {
        Guid id = new (request.VATId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        var entity = await _repository.GetAsync(id);
        var response = new VATResponse();
        if (entity == null)
        {
            return response;
        }
        if (tenantId != entity.TenantId) 
        {
            return response;
        }
        response.VAT = new OMS.Shared.Protos.MdmService.VATs.VAT()
        {
            Id = entity.Id.ToString(),
            TenantId = request.TenantId,
            Code = entity.Code,
            Name = entity.Name,
            Rate = entity.Rate,
        };
        return response;
    }
}