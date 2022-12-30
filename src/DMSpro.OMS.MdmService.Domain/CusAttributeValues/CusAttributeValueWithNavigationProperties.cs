using DMSpro.OMS.MdmService.CustomerAttributes;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueWithNavigationProperties
    {
        public CusAttributeValue CusAttributeValue { get; set; }

        public CustomerAttribute CustomerAttribute { get; set; }
        public CusAttributeValue CusAttributeValue1 { get; set; }
        

        
    }
}