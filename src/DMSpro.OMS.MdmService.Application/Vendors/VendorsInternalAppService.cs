using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public partial class VendorsInternalAppService : ApplicationService, IVendorsInternalAppService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorsInternalAppService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public virtual async Task<VendorWithTenantIdDto> GetWithTenantIdAsynce(Guid id)
        {
            Vendor vendor = await _vendorRepository.GetAsync(id);
            return ObjectMapper.Map<Vendor, VendorWithTenantIdDto>(vendor);
        }
    }
}