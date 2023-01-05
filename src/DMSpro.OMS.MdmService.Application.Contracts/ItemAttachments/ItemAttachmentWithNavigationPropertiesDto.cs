using DMSpro.OMS.MdmService.Items;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentWithNavigationPropertiesDto
    {
        public ItemAttachmentDto ItemAttachment { get; set; }

        public ItemDto Item { get; set; }

    }
}