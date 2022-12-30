using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailWithNavigationPropertiesDto
    {
        public PriceUpdateDetailDto PriceUpdateDetail { get; set; }

        public PriceUpdateDto PriceUpdate { get; set; }
        public PriceListDetailDto PriceListDetail { get; set; }

    }
}