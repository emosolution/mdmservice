using DMSpro.OMS.MdmService.PriceLists;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateWithNavigationPropertiesDto
    {
        public PriceUpdateDto PriceUpdate { get; set; }

        public PriceListDto PriceList { get; set; }

    }
}