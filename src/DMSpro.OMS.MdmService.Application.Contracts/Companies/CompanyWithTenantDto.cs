using System;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyWithTenantDto : CompanyDto
    {
        public Guid TenantId { get; set; }
    }
}