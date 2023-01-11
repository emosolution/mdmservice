using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using DMSpro.OMS.Shared.GRPC.MdmService.Vendors;

namespace DMSpro.OMS.MdmService.Vendors;

public class VendorsGRPCAppService : ApplicationService, IVendorsGRPCService
{
    readonly IVendorsAppService _vendorsAppService;

    public VendorsGRPCAppService(IVendorsAppService vendorsAppService)
    {
        _vendorsAppService = vendorsAppService;
    }

    public async Task<VendorGRPCDto> GetAsync(Guid vendorId)
    {
        VendorDto vendorDto = await _vendorsAppService.GetAsync(vendorId);
        return ObjectMapper.Map<VendorDto, VendorGRPCDto>(vendorDto);
    }
}