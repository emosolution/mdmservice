using DMSpro.OMS.MdmService.Customers;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageWithNavigationProperties
    {
        public CustomerImage CustomerImage { get; set; }

        public Customer Customer { get; set; }
        

        
    }
}