using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Vendors;
using DMSpro.OMS.MdmService.Helpers;
using Castle.Components.DictionaryAdapter.Xml;

namespace DMSpro.OMS.MdmService.Vendors;

public class VendorsGRPCAppService : VendorsProtoAppService.VendorsProtoAppServiceBase
{
    readonly IVendorsInternalAppService _vendorsInternalAppService;

    public VendorsGRPCAppService(IVendorsInternalAppService vendorsInternalAppService)
    {
        _vendorsInternalAppService = vendorsInternalAppService;
    }

    public override async Task<VendorResponse> GetVendor(GetVendorRequest request, ServerCallContext context)
    {
        Guid vendorId = new (request.VendorId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        VendorWithTenantDto vendorDto = await _vendorsInternalAppService.GetWithTenantIdAsynce(vendorId);
        var response = new VendorResponse();
        if (vendorDto == null)
        {
            return response;
        }
        if (tenantId != vendorDto.TenantId) 
        {
            return response;
        }
        response.Vendor = new OMS.Shared.Protos.MdmService.Vendors.Vendor()
        {
            Id = vendorDto.Id.ToString(),
            CompanyId = vendorDto.CompanyId.ToString(),
            TenantId = request.TenantId,
            Code = vendorDto.Code,
            Name = vendorDto.Name,
            ShortName = vendorDto.ShortName,
            Active = MDMHelpers.CheckActive(vendorDto.Active, vendorDto.CreationTime, vendorDto.EndDate),
        };
        return response;
    }

    public override async Task<VendorResponse> GetVendorWithCompany(
        GetVendorWithCompanyRequest request, ServerCallContext context)
    {
        GetVendorRequest vendorRequest = new()
        {
            TenantId = request.TenantId,
            VendorId = request.VendorId,
        };
        var response = new VendorResponse();
        VendorResponse vendorResponse = await GetVendor(vendorRequest, context);
        if (vendorResponse.Vendor == null)
        {
            return response;
        }
        if (vendorResponse.Vendor.CompanyId.CompareTo(request.CompanyId) != 0)
        {
            return response;
        }
        return response;
    }
}