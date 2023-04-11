using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}