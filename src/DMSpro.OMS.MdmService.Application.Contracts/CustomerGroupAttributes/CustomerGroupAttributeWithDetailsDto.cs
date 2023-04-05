using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributeWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid? Attr0Id { get; set; }
        public Guid? Attr1Id { get; set; }
        public Guid? Attr2Id { get; set; }
        public Guid? Attr3Id { get; set; }
        public Guid? Attr4Id { get; set; }
        public Guid? Attr5Id { get; set; }
        public Guid? Attr6Id { get; set; }
        public Guid? Attr7Id { get; set; }
        public Guid? Attr8Id { get; set; }
        public Guid? Attr9Id { get; set; }
        public Guid? Attr10Id { get; set; }
        public Guid? Attr11Id { get; set; }
        public Guid? Attr12Id { get; set; }
        public Guid? Attr13Id { get; set; }
        public Guid? Attr14Id { get; set; }
        public Guid? Attr15Id { get; set; }
        public Guid? Attr16Id { get; set; }
        public Guid? Attr17Id { get; set; }
        public Guid? Attr18Id { get; set; }
        public Guid? Attr19Id { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public CustomerAttributeValueDto Attr0 { get; set; }
        public CustomerAttributeValueDto Attr1 { get; set; }
        public CustomerAttributeValueDto Attr2 { get; set; }
        public CustomerAttributeValueDto Attr3 { get; set; }
        public CustomerAttributeValueDto Attr4 { get; set; }
        public CustomerAttributeValueDto Attr5 { get; set; }
        public CustomerAttributeValueDto Attr6 { get; set; }
        public CustomerAttributeValueDto Attr7 { get; set; }
        public CustomerAttributeValueDto Attr8 { get; set; }
        public CustomerAttributeValueDto Attr9 { get; set; }
        public CustomerAttributeValueDto Attr10 { get; set; }
        public CustomerAttributeValueDto Attr11 { get; set; }
        public CustomerAttributeValueDto Attr12 { get; set; }
        public CustomerAttributeValueDto Attr13 { get; set; }
        public CustomerAttributeValueDto Attr14 { get; set; }
        public CustomerAttributeValueDto Attr15 { get; set; }
        public CustomerAttributeValueDto Attr16 { get; set; }
        public CustomerAttributeValueDto Attr17 { get; set; }
        public CustomerAttributeValueDto Attr18 { get; set; }
        public CustomerAttributeValueDto Attr19 { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerGroupAttributeWithDetailsDto()
        {

        }
    }
}