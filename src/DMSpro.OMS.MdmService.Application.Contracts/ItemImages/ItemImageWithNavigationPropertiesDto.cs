using DMSpro.OMS.MdmService.Items;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageWithNavigationPropertiesDto
    {
        public ItemImageDto ItemImage { get; set; }

        public ItemDto Item { get; set; }

    }
}