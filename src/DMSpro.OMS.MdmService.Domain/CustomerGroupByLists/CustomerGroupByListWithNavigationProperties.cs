using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListWithNavigationProperties
    {
        public CustomerGroupByList CustomerGroupByList { get; set; }

        public CustomerGroup CustomerGroup { get; set; }
        public Customer Customer { get; set; }
        

        
    }
}