using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneUpdateDto : IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        [StringLength(ItemGroupInZoneConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid SellingZoneId { get; set; }
        public Guid ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}