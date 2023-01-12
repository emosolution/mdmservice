using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorWithTenantIdDto : VendorDto
    { 
        public Guid? TenantId { get; set; }
    }
}