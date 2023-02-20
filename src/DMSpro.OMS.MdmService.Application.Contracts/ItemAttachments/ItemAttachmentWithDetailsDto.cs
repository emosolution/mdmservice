using System;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public class ItemAttachmentWithDetailsDto
	{
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public ItemDto Item { get; set; }
        public ItemAttachmentWithDetailsDto()
		{
		}
	}
}

