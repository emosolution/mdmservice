using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AttrNo { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }
        public bool IsSellingCategory { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}