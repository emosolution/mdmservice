using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.VATs;
using System.Collections.Generic;
using System.Linq;

namespace DMSpro.OMS.MdmService.VATs;

public class VATsGRPCAppService : VATsProtoAppService.VATsProtoAppServiceBase
{
    readonly IVATRepository _repository;

    public VATsGRPCAppService(IVATRepository repository)
    {
        _repository = repository;
    }

    public override async Task<ListVATResponse> GetListVAT(GetListVATRequest request, ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        List<DMSpro.OMS.Shared.Protos.MdmService.VATs.VAT> VATs = new();
        IQueryable<VAT> queryable = await _repository.GetQueryableAsync();
        var query = from vat in queryable
                    where request.VATIds.Contains(vat.Id.ToString()) &&
                        vat.TenantId == tenantId
                    select vat;
        List<VAT> entities = query.ToList<VAT>();
        foreach (VAT entity in entities)
        {
            VATs.Add(new DMSpro.OMS.Shared.Protos.MdmService.VATs.VAT()
            {
                VATId = entity.Id.ToString(),
                TenantId = request.TenantId,
                Code = entity.Code,
                Name = entity.Name,
                Rate = entity.Rate,
            });
        }
        ListVATResponse response = new();
        response.VATs.Add(VATs);
        return response;
    }
}