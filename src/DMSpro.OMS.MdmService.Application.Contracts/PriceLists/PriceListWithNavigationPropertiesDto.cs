using DMSpro.OMS.MdmService.PriceLists;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListWithNavigationPropertiesDto
    {
        public PriceListDto PriceList { get; set; }

        public PriceListDto PriceList1 { get; set; }

    }
}