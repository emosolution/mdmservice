using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroups;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListWithNavigationProperties
    {
        public CustomerGroupList CustomerGroupList { get; set; }

        public Customer Customer { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
        

        
    }
}