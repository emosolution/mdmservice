using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class SSHistoryInZoneCreateDto
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}