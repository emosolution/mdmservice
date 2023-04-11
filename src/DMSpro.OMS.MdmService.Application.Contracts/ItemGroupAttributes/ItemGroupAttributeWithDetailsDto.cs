using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemGroups;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributeWithDetailsDto : ItemGroupAttributeDto, IHasConcurrencyStamp
    {
        public ItemGroupDto ItemGroup { get; set; }
        public ItemAttributeValueDto Attr0 { get; set; }
        public ItemAttributeValueDto Attr1 { get; set; }
        public ItemAttributeValueDto Attr2 { get; set; }
        public ItemAttributeValueDto Attr3 { get; set; }
        public ItemAttributeValueDto Attr4 { get; set; }
        public ItemAttributeValueDto Attr5 { get; set; }
        public ItemAttributeValueDto Attr6 { get; set; }
        public ItemAttributeValueDto Attr7 { get; set; }
        public ItemAttributeValueDto Attr8 { get; set; }
        public ItemAttributeValueDto Attr9 { get; set; }
        public ItemAttributeValueDto Attr10 { get; set; }
        public ItemAttributeValueDto Attr11 { get; set; }
        public ItemAttributeValueDto Attr12 { get; set; }
        public ItemAttributeValueDto Attr13 { get; set; }
        public ItemAttributeValueDto Attr14 { get; set; }
        public ItemAttributeValueDto Attr15 { get; set; }
        public ItemAttributeValueDto Attr16 { get; set; }
        public ItemAttributeValueDto Attr17 { get; set; }
        public ItemAttributeValueDto Attr18 { get; set; }
        public ItemAttributeValueDto Attr19 { get; set; }

        public ItemGroupAttributeWithDetailsDto()
        {
        }
    }
}