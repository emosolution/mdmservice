using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public string ConcurrencyStamp { get; set; }

        public CustomerAttributeDto CustomerAttribute { get; set; } 
        
        public CustomerAttributeValueWithDetailsDto()
        {

        }
    }
}