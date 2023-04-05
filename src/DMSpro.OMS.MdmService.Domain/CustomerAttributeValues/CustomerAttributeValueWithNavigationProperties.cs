using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueWithNavigationProperties
    {
        public CustomerAttributeValue CustomerAttributeValue { get; set; }

        public CustomerAttribute CustomerAttribute { get; set; }
        public CustomerAttributeValue CustomerAttributeValue1 { get; set; }
        

        
    }
}