using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.CustomerGroups;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentWithNavigationProperties
    {
        public PricelistAssignment PricelistAssignment { get; set; }

        public PriceList PriceList { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
        

        
    }
}