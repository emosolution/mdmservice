using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributeWithDetailsDto : CustomerGroupAttributeDto, IHasConcurrencyStamp
    {   
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
        public CustomerGroupAttributeWithDetailsDto()
        {
        }
    }
}