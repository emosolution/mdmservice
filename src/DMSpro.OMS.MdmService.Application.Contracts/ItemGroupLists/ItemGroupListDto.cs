using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Rate { get; set; }
        public decimal Price { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UomId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}