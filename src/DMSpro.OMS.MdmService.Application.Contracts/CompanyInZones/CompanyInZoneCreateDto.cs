using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneCreateDto
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsBase { get; set; } = false;
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CompanyId { get; set; }
    }
}