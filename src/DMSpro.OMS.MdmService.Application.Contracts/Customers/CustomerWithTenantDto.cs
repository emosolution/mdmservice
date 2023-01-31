using System;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerWithTenantDto : CustomerDto
    {
        public Guid? TenantId { get; set; }
    }
}
