using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemProfileDto 
    {
        public ItemDto Item { get; set; }
        public List<ItemAttachmentDto> Attachments { get; set; }
        public List<ItemImageDto> Images { get; set; }
    }
}
