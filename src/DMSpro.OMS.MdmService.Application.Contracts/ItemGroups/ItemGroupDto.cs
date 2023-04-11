using DMSpro.OMS.MdmService.ItemGroups;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public GroupStatus Status { get; set; }
        public bool Selectable { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}