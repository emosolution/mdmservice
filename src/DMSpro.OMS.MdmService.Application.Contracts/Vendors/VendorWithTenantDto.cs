using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorWithTenantDto : VendorDto
    { 
        public Guid? TenantId { get; set; }
    }
}