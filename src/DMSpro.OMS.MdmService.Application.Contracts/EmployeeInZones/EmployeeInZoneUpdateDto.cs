using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneUpdateDto : IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}