using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int AttrNo { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevel { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}