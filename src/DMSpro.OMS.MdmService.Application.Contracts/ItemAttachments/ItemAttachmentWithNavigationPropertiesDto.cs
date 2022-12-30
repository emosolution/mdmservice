using DMSpro.OMS.MdmService.ItemMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentWithNavigationPropertiesDto
    {
        public ItemAttachmentDto ItemAttachment { get; set; }

        public ItemMasterDto ItemMaster { get; set; }

    }
}