using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public Guid SellingZoneId { get; set; }
        public Guid ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}