using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}