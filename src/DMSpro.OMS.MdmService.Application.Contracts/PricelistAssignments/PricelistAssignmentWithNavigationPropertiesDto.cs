using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.CustomerGroups;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentWithNavigationPropertiesDto
    {
        public PricelistAssignmentDto PricelistAssignment { get; set; }

        public PriceListDto PriceList { get; set; }
        public CustomerGroupDto CustomerGroup { get; set; }

    }
}