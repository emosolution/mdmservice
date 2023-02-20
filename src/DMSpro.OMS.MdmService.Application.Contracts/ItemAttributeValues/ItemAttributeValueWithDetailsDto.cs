using System;
using DMSpro.OMS.MdmService.ItemAttributes;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
	public class ItemAttributeValueWithDetailsDto
	{
        public string AttrValName { get; set; }
        public Guid ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public ItemAttributeValueDto Parent { get; set; }
        public ItemAttributeDto ItemAttribute { get; set; }

        public ItemAttributeValueWithDetailsDto()
		{
		}
	}
}

