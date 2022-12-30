using DMSpro.OMS.MdmService.ItemMasters;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentWithNavigationProperties
    {
        public ItemAttachment ItemAttachment { get; set; }

        public ItemMaster ItemMaster { get; set; }
        

        
    }
}