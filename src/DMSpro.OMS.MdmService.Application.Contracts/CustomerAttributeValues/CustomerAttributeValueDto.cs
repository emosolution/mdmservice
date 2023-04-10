using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}