using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.Items;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailWithNavigationPropertiesDto
    {
        public PriceListDetailDto PriceListDetail { get; set; }

        public PriceListDto PriceList { get; set; }
        public UOMDto UOM { get; set; }
        public ItemDto Item { get; set; }

    }
}