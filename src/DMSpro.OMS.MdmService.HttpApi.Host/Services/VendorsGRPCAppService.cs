using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.MdmService.Protos.Vendors;

namespace DMSpro.OMS.MdmService.Vendors;

public class VendorsGRPCAppService : VendorsProtoAppService.VendorsProtoAppServiceBase
{
    readonly IVendorsInternalAppService _vendorsInternalAppService;

    public VendorsGRPCAppService(IVendorsInternalAppService vendorsInternalAppService)
    {
        _vendorsInternalAppService = vendorsInternalAppService;
    }

    public override async Task<GetAsyncVendorResponse> GetAsync(GetAsyncVendorRequest request, ServerCallContext context)
    {
        Guid vendorId = new (request.VendorId);
        VendorWithTenantIdDto vendorDto = await _vendorsInternalAppService.GetWithTenantIdAsynce(vendorId);
        var response = new GetAsyncVendorResponse();
        response.Vendor = new Protos.Vendors.Vendor()
        {
            Id = vendorDto.Id.ToString(),
            CompanyId = vendorDto.CompanyId.ToString(),
            TenantId = vendorDto.TenantId.ToString(),
            Code = vendorDto.Code,
            Name = vendorDto.Name,
            ShortName = vendorDto.ShortName,
        };
        return response;
    }
}