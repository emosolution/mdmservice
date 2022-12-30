using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneCreateDto
    {
        public DateTime EffectiveDate { get; set; }
        public Guid? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}