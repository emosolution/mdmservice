using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZoneUpdateDto : IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}