using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Vendors;

namespace DMSpro.OMS.MdmService.Vendors;

public class VendorsGRPCAppService : VendorsProtoAppService.VendorsProtoAppServiceBase
{
    readonly IVendorsInternalAppService _vendorsInternalAppService;

    public VendorsGRPCAppService(IVendorsInternalAppService vendorsInternalAppService)
    {
        _vendorsInternalAppService = vendorsInternalAppService;
    }

    public override async Task<GetVendorResponse> GetVendor(GetVendorRequest request, ServerCallContext context)
    {
        Guid vendorId = new (request.VendorId);
        VendorWithTenantDto vendorDto = await _vendorsInternalAppService.GetWithTenantIdAsynce(vendorId);
        var response = new GetVendorResponse();
        response.Vendor = new OMS.Shared.Protos.MdmService.Vendors.Vendor()
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