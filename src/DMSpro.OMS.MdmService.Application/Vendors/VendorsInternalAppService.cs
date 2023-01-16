using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorsInternalAppService : ApplicationService, IVendorsInternalAppService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorsInternalAppService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public virtual async Task<VendorWithTenantDto> GetWithTenantIdAsynce(Guid id)
        {
            try
            {
                Vendor vendor = await _vendorRepository.GetAsync(id);
                return ObjectMapper.Map<Vendor, VendorWithTenantDto>(vendor);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}