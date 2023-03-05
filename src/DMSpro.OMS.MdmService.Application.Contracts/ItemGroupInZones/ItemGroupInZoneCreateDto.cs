using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneCreateDto
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; } = true;
        [StringLength(ItemGroupInZoneConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid SellingZoneId { get; set; }
        public Guid ItemGroupId { get; set; }
    }
}