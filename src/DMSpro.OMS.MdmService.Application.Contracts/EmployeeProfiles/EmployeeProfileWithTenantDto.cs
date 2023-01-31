using System;
using System.Collections.Generic;
using System.Text;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileWithTenantDto : EmployeeProfileDto
    {
        public Guid? TenantId { get; set; }
    }
}
