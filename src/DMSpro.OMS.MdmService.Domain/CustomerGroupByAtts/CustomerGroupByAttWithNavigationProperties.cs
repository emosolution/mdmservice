using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CusAttributeValues;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttWithNavigationProperties
    {
        public CustomerGroupByAtt CustomerGroupByAtt { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
        public CusAttributeValue CusAttributeValue { get; set; }
        

        
    }
}