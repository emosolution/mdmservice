using DMSpro.OMS.MdmService.ItemMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageWithNavigationPropertiesDto
    {
        public ItemImageDto ItemImage { get; set; }

        public ItemMasterDto ItemMaster { get; set; }

    }
}