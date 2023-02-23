using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Vendors;
using DMSpro.OMS.MdmService.Helpers;
using System.Linq;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.Vendors;

public class VendorsGRPCAppService : VendorsProtoAppService.VendorsProtoAppServiceBase
{
    private readonly IVendorRepository _repository;
    private readonly ICurrentTenant _currentTenant;


    public VendorsGRPCAppService(IVendorRepository repository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _currentTenant = currentTenant;
    }

    public override async Task<VendorResponse> GetVendor(GetVendorRequest request, ServerCallContext context)
    {
        Guid id = new(request.VendorId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            IQueryable<Vendor> queryable = await _repository.GetQueryableAsync();
            var query = from item in queryable
                        where item.Id == id && item.TenantId == tenantId
                        select item;
            Vendor vendor = query.FirstOrDefault();
            var response = new VendorResponse();
            if (vendor == null)
            {
                return response;
            }

            response.Vendor = new OMS.Shared.Protos.MdmService.Vendors.Vendor()
            {
                Id = vendor.Id.ToString(),
                CompanyId = vendor.CompanyId.ToString(),
                TenantId = request.TenantId,
                Code = vendor.Code,
                Name = vendor.Name,
                ShortName = vendor.ShortName,
                Active = MDMHelpers.CheckActive(vendor.Active, vendor.CreationTime, vendor.EndDate),
            };
            return response;
        }
    }


    public override async Task<VendorResponse> GetVendorWithCompany(
        GetVendorWithCompanyRequest request, ServerCallContext context)
    {
        GetVendorRequest vendorRequest = new()
        {
            TenantId = request.TenantId,
            VendorId = request.VendorId,
        };
        VendorResponse vendorResponse = await GetVendor(vendorRequest, context);
        if (vendorResponse.Vendor == null)
        {
            return new VendorResponse(); ;
        }
        if (vendorResponse.Vendor.CompanyId.CompareTo(request.CompanyId) != 0)
        {
            return new VendorResponse(); ;
        }
        return vendorResponse;
    }

}